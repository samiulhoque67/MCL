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
    public class VendorCSAprvController : Controller
    {
        readonly IVendorCSAprvService _vendorCSInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private readonly string UserName = string.Empty;
        private string action = string.Empty;

        public VendorCSAprvController(IVendorCSAprvService repository, ILocalizationService localizationService)
        {
            this._vendorCSInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
            UserName = SILAuthorization.GetUserFullName();
        }
        // GET: /VendorCSAprv/Index
        public ActionResult Index()
        {
            return View();
        }
        public async Task<dynamic> GetVendorCSAprvDashBordData()
        {
            List<OBS_VendorCSAprvItem> result = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSAprvDashBordData(UserID, out result));
            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[Authorize]
        public async Task<dynamic> GetServicesCategory()
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _vendorCSInfoService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServiceCategoryID = x.ServicesCategoryID,
                ServiceCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ServiceCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetVendorCSClientInfo(string ServiceCategoryID)
        {
            var CSClientList = new List<OBS_ClientReq>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSClientInfo(ServiceCategoryID, out CSClientList));
            var result = Json(new { CSClientList, msg = "CSClientList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID)
        {
            var CSVendorList = new List<OBS_VendorCSAprv>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSVendorsUsingClient(ClientID, VendorCSRecmID, out CSVendorList));
            var result = Json(new { CSVendorList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorCSRecmItemID)
        {
            var VenCSItemList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSQuotationItem(VendorID, ClientID, VendorCSRecmItemID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "VenCSItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSAprvTermList(string VendorCSRecmID)
        {
            var VendorCSAprvTermList = new List<OBS_VendorCSAprvTerms>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSAprvTermList(VendorCSRecmID, out VendorCSAprvTermList));
            var result = Json(new { VendorCSAprvTermList, msg = "VendorCSAprvTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorCSAprv(OBS_VendorCSAprv vendorCS, List<OBS_VendorCSAprvItem> vendorCSItem, List<OBS_VendorCSAprvTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            vendorCS.RecommendedByName = SILAuthorization.GetUserFullName(); 
            vendorCS.RecommendedByDesignation = SILAuthorization.GetUserDesignation(); ;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorCSInfoService.SaveVendorCSAprv(vendorCS, vendorCSItem, vendorCSTerm);

            TempData["VendorCSRecmInfo"] = vendorCS;
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID)
        {
            string status = string.Empty;
            status = _vendorCSInfoService.DeleteVendorCSAprvItemAndTerm(VendorCSAprvItemID, VendorCSAprvTermID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsConditionsList()
        {
            var termsConditionsList = new List<OBS_Terms>();
            await Task.Run(() => _vendorCSInfoService.GetTermsConditionsList(out termsConditionsList));
            var result = Json(new { termsConditionsList, msg = "Terms Conditions List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSAprvTermAgainstFormList(string TermsID)
        {
            var VendorCSAprvTermList = new List<OBS_VendorCSAprvTerms>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSAprvTermAgainstFormList(TermsID, out VendorCSAprvTermList));
            var result = Json(new { VendorCSAprvTermList, msg = "VendorCSAprvTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetReqWiseVendorList(string VendorCSAprvID)
        {
            var ReqWiseVendorList = new List<OBS_VendorCSAprvVendors>();
            await Task.Run(() => _vendorCSInfoService.GetReqWiseVendorList(VendorCSAprvID, out ReqWiseVendorList));
            var result = Json(new { ReqWiseVendorList, msg = "ReqWiseVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        [Authorize]
        public async Task<dynamic> GetAllRequisition()
        {
            var InvitationList = new List<Invitation>();
            await Task.Run(() => _vendorCSInfoService.GetAllRequisition(UserID, out InvitationList));
            return Json(new { InvitationList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetMaterialByRequisition(string VendorRequisitionNumber)
        {
            var ReqWiseMaterialList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSInfoService.GetMaterialByRequisition(VendorRequisitionNumber, out ReqWiseMaterialList));
            return Json(new { ReqWiseMaterialList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetVendorByMaterial(string VendorReqID, string ServiceItemID)
        {
            var MatWiseVendorList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSInfoService.GetVendorByMaterialService(VendorReqID, ServiceItemID, out MatWiseVendorList));
            return Json(new { MatWiseVendorList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}