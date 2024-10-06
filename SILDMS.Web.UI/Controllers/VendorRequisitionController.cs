using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public async Task<dynamic> SaveVendorRequisition(OBS_VendorReq vendorReq, List<OBS_VendorReqItem> vendorReqItem, List<OBS_VendorReqTerms> vendorReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise)
        {
            vendorReq.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveVendorRequisition(vendorReq, vendorReqItem, vendorReqTerm, vendorReqItemWise);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetVendorReqSearchList()
        {
            var vendorReqSearchList = new List<OBS_VendorReq>();
            await Task.Run(() => _clientInfoService.GetVendorReqSearchList(out vendorReqSearchList));
            var result = Json(new { vendorReqSearchList, msg = "vendorReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
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
    }
}