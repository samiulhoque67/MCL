using SILDMS.Model;
using SILDMS.Service.ClientSettlement;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class ClientSettlementController : Controller
    {
        // AIT challans and Mushaks are stored under App_Data, which IIS never serves
        // directly — every download has to come through DownloadSettlementDocument.
        private const string DocFolder = "~/App_Data/ClientSettlementDocs";
        private const int MaxFileBytes = 5 * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

        private readonly IClientSettlementService clientSettlementService;
        private readonly string UserID;

        public ClientSettlementController(IClientSettlementService _clientSettlementService)
        {
            clientSettlementService = _clientSettlementService;
            UserID = SILAuthorization.GetUserID();
        }

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetClientSettlementList()
        {
            var list = new List<OBS_ClientSettlement>();
            await Task.Run(() => clientSettlementService.GetClientSettlementList(out list));
            var result = Json(new { ClientSettlementList = list, msg = "ClientSettlementList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientSettlementBillItem(string ClientFinalBillID)
        {
            var list = new List<OBS_ClientBillCollectionItem>();
            await Task.Run(() => clientSettlementService.GetClientSettlementBillItem(ClientFinalBillID, out list));
            var result = Json(new { ClientSettlementBillItemList = list, msg = "ClientSettlementBillItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientSettlementHistory(string ClientBillCollectionID)
        {
            var list = new List<OBS_ClientSettlementHistoryRow>();
            await Task.Run(() => clientSettlementService.GetClientSettlementHistory(ClientBillCollectionID, out list));
            var result = Json(new { ClientSettlementHistory = list, msg = "ClientSettlementHistory are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        // Posted as multipart/form-data: the text fields bind onto OBS_ClientSettlement,
        // the files arrive as Request.Files["AITDocument"] / ["MushakDocument"].
        [HttpPost]
        public async Task<dynamic> SaveClientSettlementInfo(OBS_ClientSettlement settlement)
        {
            if (string.IsNullOrEmpty(UserID))
                return Json(new { status = "E440", msg = "Session expired. Please sign in again." });

            settlement.SetBy = UserID;

            // Only keep a document for a side that actually has an amount in this entry.
            decimal aitRecv = ParseDecimal(settlement.AITChallanReceiveAmt);
            decimal mushakRecv = ParseDecimal(settlement.MushakReceiveAmt);

            string aitPath = null, mushakPath = null, aitName = null, mushakName = null, err;

            if (aitRecv > 0)
            {
                aitPath = SaveUpload(Request.Files["AITDocument"], out aitName, out err);
                if (err != null) return Json(new { status = "E430", msg = err });
            }
            if (mushakRecv > 0)
            {
                mushakPath = SaveUpload(Request.Files["MushakDocument"], out mushakName, out err);
                if (err != null)
                {
                    DeleteStored(aitPath);
                    return Json(new { status = "E430", msg = err });
                }
            }

            settlement.AITDocName = aitName;
            settlement.AITDocPath = aitPath;
            settlement.MushakDocName = mushakName;
            settlement.MushakDocPath = mushakPath;

            string status = null;
            try
            {
                await Task.Run(() => status = clientSettlementService.SaveClientSettlementInfo(settlement));
            }
            catch
            {
                DeleteStored(aitPath);
                DeleteStored(mushakPath);
                throw;
            }

            // Anything other than a success code means nothing was saved — don't leave orphan files behind.
            if (status == null || !status.StartsWith("S"))
            {
                DeleteStored(aitPath);
                DeleteStored(mushakPath);
            }

            return Json(new { status });
        }

        public ActionResult DownloadSettlementDocument(string ClientSettlementID, string DocType)
        {
            if (string.IsNullOrEmpty(UserID)) return new HttpUnauthorizedResult();
            if (DocType != "AIT" && DocType != "Mushak") return HttpNotFound();

            string name, path;
            clientSettlementService.GetClientSettlementDocPath(ClientSettlementID, DocType, out name, out path);
            if (string.IsNullOrEmpty(path)) return HttpNotFound();

            try
            {
                var serverIP = ConfigurationManager.AppSettings["serverIP"];
                var ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                var ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                var ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

                // path is stored as e.g. "MCL/ClientSettlement/{guid}.pdf"
                string ftpUrl = $"ftp://{serverIP}:{ftpPort}/{path}";

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                using (Stream responseStream = ftpResponse.GetResponseStream())
                {
                    if (responseStream == null)
                        return HttpNotFound();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileData = memoryStream.ToArray();

                        string downloadName = string.IsNullOrEmpty(name)
                            ? Path.GetFileName(path)
                            : name;

                        string contentType = MimeMapping.GetMimeMapping(downloadName);
                        return File(fileData, contentType, downloadName);
                    }
                }
            }
            catch (WebException)
            {
                // File not found on FTP or connection problem
                return HttpNotFound();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500, "Error retrieving document from FTP server.");
            }
        }

        // ── helpers ──────────────────────────────────────────────────

        private string SaveUpload(HttpPostedFileBase file, out string originalName, out string error)
        {
            originalName = null;
            error = null;

            if (file == null || file.ContentLength == 0)
                return null;   // documents are optional

            string ext = (Path.GetExtension(file.FileName) ?? string.Empty).ToLowerInvariant();
            if (!AllowedExtensions.Contains(ext))
            {
                error = "Only PDF, JPG or PNG documents are allowed.";
                return null;
            }
            if (file.ContentLength > MaxFileBytes)
            {
                error = "A document may not be larger than 5 MB.";
                return null;
            }

            try
            {
                var serverIP = ConfigurationManager.AppSettings["serverIP"];
                var ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                var ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                var ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

                // Unique name on the FTP server (never reuse the client's file name)
                string documentID = Guid.NewGuid().ToString("N");
                string storedExt = ext.TrimStart('.');          // "pdf", "jpg", ...
                string serverUrl = "MCL/ClientSettlement";           // folder on the FTP server – change if you prefer another path

                string ftpUrl = $"ftp://{serverIP}:{ftpPort}/{serverUrl}/{documentID}.{storedExt}";

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                byte[] fileData;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }

                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(fileData, 0, fileData.Length);
                }

                originalName = Path.GetFileName(file.FileName);

                // Return a path that the rest of the application can use later
                // (same style as the original DocFolder + storedName)
                return serverUrl + "/" + documentID + "." + storedExt;
            }
            catch (WebException webEx)
            {
                var response = webEx.Response as FtpWebResponse;
                error = "Error uploading file to FTP server. " +
                        (response?.StatusDescription ?? webEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                error = "Error uploading file. " + (ex.InnerException?.Message ?? ex.Message);
                return null;
            }
        }

        private void DeleteStored(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath)) return;
            try
            {
                string physical = Server.MapPath(virtualPath);
                if (System.IO.File.Exists(physical)) System.IO.File.Delete(physical);
            }
            catch { /* best-effort cleanup */ }
        }

        private static decimal ParseDecimal(string s)
        {
            decimal d;
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d) ? d : 0m;
        }
    }
}