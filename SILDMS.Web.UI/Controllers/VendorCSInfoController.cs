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
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorCSInfoController : Controller
    {
        readonly IVendorCSInfoService _vendorCSInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorCSInfoController(IVendorCSInfoService repository, ILocalizationService localizationService)
        {
            this._vendorCSInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /VendorCSInfo/Index
        public ActionResult Index()
        {
            return View();
        }
        public async Task<dynamic> GetVendorCSInfoDashBordData()
        {
            List<OBS_VendorCSRecmItem> result = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSInfoDashBordData(UserID, out result));
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

        public async Task<dynamic> GetVendorCSVendorsUsingClient(string ClientID)
        {
            var CSVendorList = new List<OBS_VendorCSRecm>();
            await Task.Run(() => _vendorCSInfoService.OBS_GetVendorCSVendorsUsingClient(ClientID, out CSVendorList));
            var result = Json(new { CSVendorList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorQutnItemID)
        {
            var VenCSItemList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSInfoService.OBS_GetVendorCSQuotationItem(VendorID, ClientID, VendorQutnItemID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "VenCSItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSInfoTermList(string VendorQutnID)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSInfoTermList(VendorQutnID, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorCSInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorCSInfoService.SaveVendorCSInfo(vendorCS, vendorCSItem, vendorCSTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID)
        {
            string status = string.Empty;
            status = _vendorCSInfoService.DeleteVendorCSInfoItemAndTerm(VendorCSInfoItemID, VendorCSInfoTermID);
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

        public async Task<dynamic> GetVendorCSInfoTermAgainstFormList(string TermsID)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => _vendorCSInfoService.GetVendorCSInfoTermAgainstFormList(TermsID, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetReqWiseVendorList(string VendorCSInfoID)
        {
            var ReqWiseVendorList = new List<OBS_VendorCSRecmVendors>();
            await Task.Run(() => _vendorCSInfoService.GetReqWiseVendorList(VendorCSInfoID, out ReqWiseVendorList));
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
            var ReqWiseMaterialList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSInfoService.GetMaterialByRequisition(VendorRequisitionNumber, out ReqWiseMaterialList));
            return Json(new { ReqWiseMaterialList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public async Task<dynamic> GetVendorByMaterial(string VendorReqID, string ServiceItemID)
        {
            var MatWiseVendorList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSInfoService.GetVendorByMaterialService(VendorReqID, ServiceItemID, out MatWiseVendorList));
            return Json(new { MatWiseVendorList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> SearchCS()
        {
            var SearchCSList = new List<Invitation>();
            await Task.Run(() => _vendorCSInfoService.SearchCSService(UserID, out SearchCSList));
            return Json(new { SearchCSList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        public async Task<dynamic> CSVendor(string CSNumber)
        {
            var VendorCSList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSInfoService.CSVendorService(UserID, CSNumber, out VendorCSList));
            return Json(new { VendorCSList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }


        public async Task<dynamic> CSVendorTerms(string CSNumber)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => _vendorCSInfoService.CSVendorTerms(CSNumber, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }




    }
}