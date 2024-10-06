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
        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<OBS_VendorQutn>();
            await Task.Run(() => _clientInfoService.GetShowVendorReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorQuotation(OBS_VendorQutn clientReq, List<OBS_VendorQutnItem> clientReqItem, List<OBS_VendorQutnTerms> clientReqTerm)
        {
            clientReq.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveVendorQuotation(clientReq, clientReqItem, clientReqTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetVendorQutnSearchList()
        {
            var clientReqSearchList = new List<OBS_VendorQutn>();
            await Task.Run(() => _clientInfoService.GetVendorQutnSearchList(out clientReqSearchList));
            var result = Json(new { clientReqSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
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

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
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
    }
}