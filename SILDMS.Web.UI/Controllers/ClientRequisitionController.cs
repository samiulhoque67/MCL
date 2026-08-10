
using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class ClientRequisitionController : Controller
    {
        readonly IClientRequisitionService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ClientRequisitionController(IClientRequisitionService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /ClientRequisition/Index


        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Authorize]
        public async Task<dynamic> GetServicesCategory()
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _clientInfoService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServiceCategoryID = x.ServicesCategoryID,
                ServiceCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ServiceCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetClientInfoList()
        {
            var ClientInfoList = new List<OBS_ClientAndAddressInfo>();
            await Task.Run(() => _clientInfoService.GetClientInfoList(out ClientInfoList));
            var result = Json(new { ClientInfoList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm)
        {
            clientReq.SetBy = UserID;
            string status = string.Empty;
            string ClientReqID = string.Empty;
            string duplicateSince = string.Empty;

            status = _clientInfoService.SaveClientRequisition(clientReq, clientReqItem, clientReqTerm);

            if (!string.IsNullOrEmpty(status))
            {
                string[] statusarr = status.Split(',');

                if (statusarr[0] == "E405")               // NEW: duplicate detected by the SP
                {
                    ClientReqID = statusarr.Length > 1 ? statusarr[1] : string.Empty; // existing req's ID
                    duplicateSince = statusarr.Length > 2 ? statusarr[2] : string.Empty;
                    status = "ERROR_Duplicate";
                }
                else if (status != "ERROR_Duplicate")
                {
                    ClientReqID = statusarr[1];
                    status = statusarr[0];
                }
            }

            return Json(new { status, ClientReqID, duplicateSince }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetClientReqSearchList()
        {
            var clientReqSearchList = new List<OBS_ClientReq>();
            await Task.Run(() => _clientInfoService.GetClientReqSearchList(out clientReqSearchList));
            var result = Json(new { clientReqSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientReqItemList(string ClientReqID, string ReqType)
        {
            var ClientReqItemList = new List<OBS_ClientReqItem>();
            await Task.Run(() => _clientInfoService.GetClientReqItemList(ClientReqID, ReqType, out ClientReqItemList));
            var result = Json(new { ClientReqItemList, msg = "ClientReqItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientReqTermList(string ClientReqID)
        {
            var ClientReqTermList = new List<OBS_ClientReqTerms>();
            await Task.Run(() => _clientInfoService.GetClientReqTermList(ClientReqID, out ClientReqTermList));
            var result = Json(new { ClientReqTermList, msg = "ClientReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteClientReqItemAndTerm(ClientReqItemID, ClientReqTermID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsConditionsList()
        {
            var termsConditionsList = new List<OBS_Terms>();
            await Task.Run(() => _clientInfoService.GetTermsConditionsList(out termsConditionsList));
            var result = Json(new { termsConditionsList, msg = "Terms Conditions List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientReqTermAgainstFormList(string TermsID)
        {
            var ClientReqTermList = new List<OBS_ClientReqTerms>();
            await Task.Run(() => _clientInfoService.GetClientReqTermAgainstFormList(TermsID, out ClientReqTermList));
            var result = Json(new { ClientReqTermList, msg = "ClientReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }


        [HttpPost]
        public ActionResult SaveDocument(string serverUrl, string documentID, string ext, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { Message = "No file uploaded." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var serverIP = ConfigurationManager.AppSettings["serverIP"];
                var ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                var ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                var ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];
                // Build FTP URL dynamically
                string ftpUrl = $"ftp://{serverIP}:{ftpPort}/{serverUrl}/{documentID}.{ext}";

                // Create an FTP request
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                // Read file data
                byte[] fileData;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }

                // Upload file data to the FTP server
                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(fileData, 0, fileData.Length);
                }

                return Json(new { Message = "File uploaded successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (WebException webEx)
            {
                var response = (FtpWebResponse)webEx.Response;
                return Json(new
                {
                    Message = "Error uploading file to FTP server.",
                    Status = response?.StatusDescription,
                    Exception = webEx.Message
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error uploading file.", Exception = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //view pdf/
        [HttpGet]
        public ActionResult ViewDocument(string serverUrl, string DocID, string ext)
        
        {
            try
            {
                var serverIP = ConfigurationManager.AppSettings["serverIP"];
                var ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                var ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                var ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

                // Ensure the extension starts with a dot
                if (!ext.StartsWith(".")) ext = "." + ext;

                // Construct the FTP URL correctly
                string ftpUrl = $"ftp://{serverIP}:{ftpPort}/{serverUrl}/{DocID}{ext}";

                // Create an FTP request to download the file
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                using (Stream responseStream = ftpResponse.GetResponseStream())
                {
                    if (responseStream == null)
                        return new HttpStatusCodeResult(404, "File not found on the FTP server.");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileData = memoryStream.ToArray();

                        return File(fileData, "application/pdf");
                    }
                }
            }
            catch (WebException webEx)
            {
                return new HttpStatusCodeResult(500, $"FTP error: {webEx.Message}");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, $"Error retrieving document: {ex.Message}");
            }
        }

        public async Task<dynamic> UpdateDocumentID(string ClientReqID, string DocumentID)
        {
            string status = string.Empty;
            await Task.Run(() =>
            {
                status = _clientInfoService.UpdateDocumentID(ClientReqID, DocumentID);
            });
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }


    }
}