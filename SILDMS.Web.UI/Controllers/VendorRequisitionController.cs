using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.Shared;
using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace SILDMS.Web.UI.Controllers
{
    public class VendorRequisitionController : Controller
    {
        readonly IVendorRequisitionService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorRequisitionController(IVendorRequisitionService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        // GET: /VendorRequisition/Index
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
        public async Task<dynamic> GetVendorInfoList()
        {
            var VendorInfoList = new List<OBS_VendorAndAddressInfo>();
            await Task.Run(() => _clientInfoService.GetVendorInfoList(out VendorInfoList));
            var result = Json(new { VendorInfoList, msg = "Vendor Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetClientReqInfoList()
        {
            var ClientReqInfoList = new List<OBS_ClientReq>();
            await Task.Run(() => _clientInfoService.GetClientReqInfoList(out ClientReqInfoList));
            var result = Json(new { ClientReqInfoList, msg = "Vendor Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientListForVendorRequisition()
        {
            var clientReqSearchList = new List<OBS_ClientReq>();
            await Task.Run(() => _clientInfoService.GetClientListForVendorRequisition(out clientReqSearchList));
            var result = Json(new { clientReqSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorRequisition(OBS_VendorReq vendorReq, List<OBS_VendorReqItem> vendorReqItem, List<OBS_VendorReqTerms> vendorReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise)
        {

            vendorReq.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveVendorRequisition(vendorReq, vendorReqItem, vendorReqTerm, vendorReqItemWise);

            if (status != string.Empty)
            {
                string[] statusarr = status.Split(',');
                vendorReq.VendorReqID = statusarr[1];
                status = statusarr[0];
            }
            TempData["VendorRequisition"] = vendorReq;
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }


        [SILAuthorize]
        public ActionResult RptRequisitionToVendorReport()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> RptRequisitionToVendorReport(string ReportType)
        {
            var tempdata = TempData["VendorRequisition"];

            ReportType = "PDF";
            if (TempData["VendorRequisition"] == null)
            {
                ViewBag.Title = "No valid data.";
                return View();
            }

            OBS_VendorReq objVendorReq = new OBS_VendorReq();
            objVendorReq = (OBS_VendorReq)TempData["VendorRequisition"];
            string VendorReqID = objVendorReq.VendorReqID;
            DataSet ds = new DataSet();
            await Task.Run(() => _clientInfoService.rptRequisitionToVendorReport(VendorReqID, "", out ds));

            // Load main report
            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports/rptVendorRequisition.rpt");
            reportDocument.Load(ReportPath);

            // Set main report data source
            DataTable dtVR = ds.Tables[0];
            reportDocument.SetDataSource(dtVR);
            reportDocument.Refresh();
            
            string reportName = "VendorRequisition";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);

            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }


        public async Task<dynamic> GetVendorReqSearchList()
        {
            var vendorReqSearchList = new List<OBS_VendorReq>();
            await Task.Run(() => _clientInfoService.GetVendorReqSearchList(out vendorReqSearchList));
            var result = Json(new { vendorReqSearchList, msg = "vendorReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorWiseItemList(string ServiceItemID)
        {
            var VendorInfoSearchList = new List<OBS_VendorInfo>();
            await Task.Run(() => _clientInfoService.GetVendorWiseItemList(ServiceItemID, out VendorInfoSearchList));
            var result = Json(new { VendorInfoSearchList, msg = "VendorInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorReqItemList(string VendorReqID)
        {
            var VendorReqItemList = new List<OBS_VendorReqItem>();
            await Task.Run(() => _clientInfoService.GetVendorReqItemList(VendorReqID, out VendorReqItemList));
            var result = Json(new { VendorReqItemList, msg = "VendorReqItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorReqTermList(string VendorReqID)
        {
            var VendorReqTermList = new List<OBS_VendorReqTerms>();
            await Task.Run(() => _clientInfoService.GetVendorReqTermList(VendorReqID, out VendorReqTermList));
            var result = Json(new { VendorReqTermList, msg = "VendorReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteVendorReqItemAndTerm(string VendorReqItemID, string VendorReqTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteVendorReqItemAndTerm(VendorReqItemID, VendorReqTermID);
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

        public async Task<dynamic> GetVendorReqTermAgainstFormList(string TermsID)
        {
            var VendorReqTermList = new List<OBS_VendorReqTerms>();
            await Task.Run(() => _clientInfoService.GetVendorReqTermAgainstFormList(TermsID, out VendorReqTermList));
            var result = Json(new { VendorReqTermList, msg = "VendorReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetReqWiseVendorList(string VendorReqID)
        {
            var ReqWiseVendorList = new List<OBS_VendorReqItemWise>();
            await Task.Run(() => _clientInfoService.GetReqWiseVendorList(VendorReqID, out ReqWiseVendorList));
            var result = Json(new { ReqWiseVendorList, msg = "ReqWiseVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[Authorize]
        //public async Task<dynamic> GetExistingVendor(string Mat_Code)
        //{
        //    var ExistingVendorlist = new List<GoogleSearch>();
        //    if (!string.IsNullOrEmpty(SILAuthorization.GetUserID()))
        //    {


        //        await Task.Run(() => _clientInfoService.GetExistingVendorService(Mat_Code, out ExistingVendorlist));

        //    }
        //    return Json(new { ExistingVendorlist, Msg = "" }, JsonRequestBehavior.AllowGet);
        //}





    }
}