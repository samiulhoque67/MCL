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
    public class VendorQuotationController : Controller
    {
        readonly IVendorQuotationService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorQuotationController(IVendorQuotationService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /VendorQuotation/Index


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
        public async Task<dynamic> GetVendorReqItemListForVenQutn(string VendorID, string VendorReqID)
        {
            var VendorReqItemList = new List<OBS_VendorReqItem>();
            await Task.Run(() => _clientInfoService.GetVendorReqItemListForVenQutn(VendorID, VendorReqID, out VendorReqItemList));
            var result = Json(new { VendorReqItemList, msg = "VendorReqItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<OBS_VendorQutn>();
            await Task.Run(() => _clientInfoService.GetShowVendorReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorQuotation(OBS_VendorQutn vendorQutn,
     List<OBS_VendorQutnItem> vendorQutnItem, List<OBS_VendorQutnTerms> vendorQutnTerm)
        {
            vendorQutn.SetBy = UserID;
            string status = string.Empty;

            if (string.IsNullOrEmpty(vendorQutn.VendorQutnID))
            {
                vendorQutn.Action = "add";
                status = _clientInfoService.SaveVendorQuotation(vendorQutn, vendorQutnItem, vendorQutnTerm);

                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    vendorQutn.VendorQutnID = statusarr[1]; // ← fix: use VendorQutnID not VendorReqID
                    status = statusarr[0];
                }
            }
            else
            {
                vendorQutn.Action = "edit";
                status = _clientInfoService.SaveVendorQuotation(vendorQutn, vendorQutnItem, vendorQutnTerm);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    vendorQutn.VendorQutnID = statusarr[1];
                    status = statusarr[0];
                }
            }

            string VendorQutnID = vendorQutn.VendorQutnID;
            return Json(new { status, VendorQutnID }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetVendorQutnSearchList()
        {
            var vendorQutnSearchList = new List<OBS_VendorQutn>();
            await Task.Run(() => _clientInfoService.GetVendorQutnSearchList(out vendorQutnSearchList));
            var result = Json(new { vendorQutnSearchList, msg = "vendorQutnSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorQutnItemList(string VendorQutnID)
        {
            var VendorQutnItemList = new List<OBS_VendorQutnItem>();
            await Task.Run(() => _clientInfoService.GetVendorQutnItemList(VendorQutnID, out VendorQutnItemList));
            var result = Json(new { VendorQutnItemList, msg = "VendorQutnItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorQutnTermList(string VendorQutnID)
        {
            var VendorQutnTermList = new List<OBS_VendorQutnTerms>();
            await Task.Run(() => _clientInfoService.GetVendorQutnTermList(VendorQutnID, out VendorQutnTermList));
            var result = Json(new { VendorQutnTermList, msg = "VendorQutnTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> DeleteVendorQutnItemAndTerm(string VendorQutnItemID, string VendorQutnTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteVendorQutnItemAndTerm(VendorQutnItemID, VendorQutnTermID);
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

        public async Task<dynamic> GetVendorQutnTermAgainstFormList(string TermsID)
        {
            var VendorQutnTermList = new List<OBS_VendorQutnTerms>();
            await Task.Run(() => _clientInfoService.GetVendorQutnTermAgainstFormList(TermsID, out VendorQutnTermList));
            var result = Json(new { VendorQutnTermList, msg = "VendorQutnTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        [HttpPost]
        public ActionResult SaveDocument(string serverIP, string ftpPort, string ftpUserName, string ftpPassword, string serverUrl, string documentID, string ext, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { Message = "No file uploaded." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                serverIP = ConfigurationManager.AppSettings["serverIP"];
                ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

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


        [HttpPost]
        public ActionResult UpdateDocumentID(string VendorQutnID, string DocumentID)
        {
            string status = _clientInfoService.UpdateDocumentID(VendorQutnID, DocumentID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
        //view pdf/
        [HttpGet]
        public ActionResult ViewDocument(string serverIP, string ftpPort, string ftpUserName, string ftpPassword, string serverUrl, string DocID, string ext)
        {
            try
            {
                // Always take from config (ignore frontend values)
                serverIP = ConfigurationManager.AppSettings["serverIP"];
                ftpPort = ConfigurationManager.AppSettings["ftpPort"];
                ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
                ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

                if (string.IsNullOrEmpty(DocID))
                    return new HttpStatusCodeResult(400, "Invalid document ID.");

                // Ensure extension starts with "."
                if (!ext.StartsWith(".")) ext = "." + ext;

                string ftpUrl = $"ftp://{serverIP}:{ftpPort}/{serverUrl}/{DocID}{ext}";

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                using (Stream responseStream = ftpResponse.GetResponseStream())
                {
                    // 🔴 File not found or empty
                    if (responseStream == null)
                        return new HttpStatusCodeResult(404, "Document not found.");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileData = memoryStream.ToArray();

                        if (fileData == null || fileData.Length == 0)
                            return new HttpStatusCodeResult(404, "Document is empty or not found.");

                        return File(fileData, "application/pdf");
                    }
                }
            }
            catch (WebException webEx)
            {
                var response = webEx.Response as FtpWebResponse;

                if (response != null)
                {
                    // 🔴 Handle FTP-specific errors
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return new HttpStatusCodeResult(404, "Document not found on server.");
                    }

                    if (response.StatusCode == FtpStatusCode.NotLoggedIn)
                    {
                        return new HttpStatusCodeResult(401, "FTP authentication failed.");
                    }

                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailableOrBusy)
                    {
                        return new HttpStatusCodeResult(404, "Document currently unavailable.");
                    }
                }

                return new HttpStatusCodeResult(500, "FTP connection error.");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500, "Unexpected error occurred while retrieving document.");
            }
        }

    }
}