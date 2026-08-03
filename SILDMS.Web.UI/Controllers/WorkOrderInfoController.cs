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
    public class WorkOrderInfoController : Controller
    {
        readonly IWorkOrderInfoService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public WorkOrderInfoController(IWorkOrderInfoService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /WorkOrderInfo/Index

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();//
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
            var ClientInfoList = new List<OBS_WOInfo>();
            await Task.Run(() => _clientInfoService.GetClientInfoList(out ClientInfoList));
            var result = Json(new { ClientInfoList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetWOInfoItemList(string ClientQutnAprvID)
        {
            var WOInfoItemList = new List<OBS_WOInfoItem>();
            await Task.Run(() => _clientInfoService.GetWOInfoItemList(ClientQutnAprvID, out WOInfoItemList));
            var result = Json(new { WOInfoItemList, msg = "WOInfoItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoTermList(string ClientQutnAprvID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoTermList(ClientQutnAprvID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveWorkOrderInfo(OBS_WOInfo woInfo,
     List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm)
        {
            woInfo.SetBy = UserID;
            string status = string.Empty;
            status = _clientInfoService.SaveWorkOrderInfo(woInfo, woInfoItem, woInfoTerm);

            // Parse WOInfoID back out if SP returns it (S201,<id> or S202,<id>)
            string WOInfoID = woInfo.WOInfoID;
            if (!string.IsNullOrEmpty(status) && status.Contains(","))
            {
                var parts = status.Split(',');
                status = parts[0];
                WOInfoID = parts[1];
            }

            return Json(new { status, WOInfoID }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetWOInfoSearchList()
        {
            var woInfoSearchList = new List<OBS_WOInfo>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchList(out woInfoSearchList));
            var result = Json(new { woInfoSearchList, msg = "woInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoSearchItemList(string WOInfoID)
        {
            var WOInfoItemList = new List<OBS_WOInfoItem>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchItemList(WOInfoID, out WOInfoItemList));
            var result = Json(new { WOInfoItemList, msg = "WOInfoItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoSearchTermsList(string WOInfoID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchTermsList(WOInfoID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteWOInfoItemAndTerm(WOInfoItemID, WOInfoTermID);
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

        public async Task<dynamic> GetWOInfoTermAgainstFormList(string TermsID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoTermAgainstFormList(TermsID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
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

        public async Task<dynamic> UpdateDocumentID(string WOInfoID, string DocumentID)
        {
            string status = string.Empty;
            await Task.Run(() =>
            {
                status = _clientInfoService.UpdateDocumentID(WOInfoID, DocumentID);
            });
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}