using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Service.VendorCSActualAprv;
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
        // GET: VendorCSActualAprv

        readonly IVendorCSActualAprvService _vendorCSActualAprvService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private readonly string UserName = string.Empty;
        private string action = string.Empty;

        public VendorCSAprvController(IVendorCSActualAprvService repository, ILocalizationService localizationService)
        {
            this._vendorCSActualAprvService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
            UserName = SILAuthorization.GetUserFullName();
        }
       
        public ActionResult AllCSRecDataforAcc()
        {
            return View();
        }

        public ActionResult AllCSVerifyDataforAud()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCSData()
        {
            return View();
        }


        public async Task<dynamic> GetVendorCSAprvDashBordData()
        {
            List<OBS_VendorCSAprvItem> result = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSAprvDashBordData(UserID, out result));
            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[Authorize]
        public async Task<dynamic> GetServicesCategory()
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _vendorCSActualAprvService.GetServicesCategory(UserID, out obServicesCategory));
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
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSClientInfo(ServiceCategoryID, out CSClientList));
            var result = Json(new { CSClientList, msg = "CSClientList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID)
        {
            var CSVendorList = new List<OBS_VendorCSAprv>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSVendorsUsingClient(ClientID, VendorCSRecmID, out CSVendorList));
            var result = Json(new { CSVendorList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorCSRecmItemID)
        {
            var VenCSItemList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSQuotationItem(VendorID, ClientID, VendorCSRecmItemID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "VenCSItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSAprvTermList(string VendorCSRecmID, string VendorID)
        {
            var VendorCSAprvTermList = new List<OBS_VendorCSAprvTerms>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSAprvTermList(VendorCSRecmID, VendorID, out VendorCSAprvTermList));
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
            status = _vendorCSActualAprvService.SaveVendorCSAprv(vendorCS, vendorCSItem, vendorCSTerm);

            TempData["VendorCSRecmInfo"] = vendorCS;
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveVendorCSRecAcc(OBS_VendorCSAprv vendorCS, List<OBS_VendorCSAprvItem> vendorCSItem, List<OBS_VendorCSAprvTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            vendorCS.RecommendedByName = SILAuthorization.GetUserFullName();
            vendorCS.RecommendedByDesignation = SILAuthorization.GetUserDesignation(); ;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorCSActualAprvService.SaveVendorCSRecAcc(vendorCS, vendorCSItem, vendorCSTerm);

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveVendorCSRecAudit(OBS_VendorCSAprv vendorCS, List<OBS_VendorCSAprvItem> vendorCSItem, List<OBS_VendorCSAprvTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            vendorCS.RecommendedByName = SILAuthorization.GetUserFullName();
            vendorCS.RecommendedByDesignation = SILAuthorization.GetUserDesignation(); ;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorCSActualAprvService.SaveVendorCSRecAudit(vendorCS, vendorCSItem, vendorCSTerm);

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveVendorCSInfo(OBS_VendorCSAprv vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            OBS_VendorCSReport objOBS_VendorCSReport = new OBS_VendorCSReport();

            vendorCS.SetBy = UserID;

            objOBS_VendorCSReport.ClientReqNo = vendorCS.ClientReqNo;
            objOBS_VendorCSReport.RequisitionDate = vendorCS.RequisitionDate;
            objOBS_VendorCSReport.RptQutnQty = vendorCS.RptQutnQty;
            objOBS_VendorCSReport.RptQutnUnit = vendorCS.RptQutnUnit;
            objOBS_VendorCSReport.ClientName = vendorCS.ClientName;
            objOBS_VendorCSReport.VenReqItem = vendorCS.VenReqItem;
            objOBS_VendorCSReport.Note = vendorCS.CSAprvNote;
            objOBS_VendorCSReport.CSRecmVendorName = vendorCS.CSRecmVendorName;
            objOBS_VendorCSReport.RecommendedByName = SILAuthorization.GetUserFullName();
            objOBS_VendorCSReport.RecommendedByDesignation = SILAuthorization.GetUserDesignation(); ;
            objOBS_VendorCSReport.CSPrepDate = vendorCS.CSRecDate;
            objOBS_VendorCSReport.VendorReqID = vendorCS.VendorReqID;
            objOBS_VendorCSReport.ServiceItemID = vendorCSItem[0].ServiceItemID;

            objOBS_VendorCSReport.PrepBy = vendorCS.PrepBy;
            objOBS_VendorCSReport.PrepDesig = vendorCS.PrepDesig;

            objOBS_VendorCSReport.RecomenBy = vendorCS.RecomenBy;
            objOBS_VendorCSReport.RecomenDesig = vendorCS.RecomenDesig;

            objOBS_VendorCSReport.RecmAccBy = vendorCS.RecmAccBy;
            objOBS_VendorCSReport.RecmAccDesig = vendorCS.RecmAccDesig;

            objOBS_VendorCSReport.VerifyBy = vendorCS.VerifyBy;
            objOBS_VendorCSReport.VerifyDesig = vendorCS.VerifyDesig;

            objOBS_VendorCSReport.ApprovedBy = vendorCS.ApprovedBy;
            objOBS_VendorCSReport.ApprovedDesig = vendorCS.ApprovedDesig;

            TempData["VendorCSprepInfo"] = objOBS_VendorCSReport;

            string status = "S201";//, message = string.Empty;

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID)
        {
            string status = string.Empty;
            status = _vendorCSActualAprvService.DeleteVendorCSAprvItemAndTerm(VendorCSAprvItemID, VendorCSAprvTermID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsConditionsList()
        {
            var termsConditionsList = new List<OBS_Terms>();
            await Task.Run(() => _vendorCSActualAprvService.GetTermsConditionsList(out termsConditionsList));
            var result = Json(new { termsConditionsList, msg = "Terms Conditions List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorCSAprvTermAgainstFormList(string TermsID)
        {
            var VendorCSAprvTermList = new List<OBS_VendorCSAprvTerms>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorCSAprvTermAgainstFormList(TermsID, out VendorCSAprvTermList));
            var result = Json(new { VendorCSAprvTermList, msg = "VendorCSAprvTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetReqWiseVendorList(string VendorCSAprvID)
        {
            var ReqWiseVendorList = new List<OBS_VendorCSAprvVendors>();
            await Task.Run(() => _vendorCSActualAprvService.GetReqWiseVendorList(VendorCSAprvID, out ReqWiseVendorList));
            var result = Json(new { ReqWiseVendorList, msg = "ReqWiseVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        [Authorize]
        public async Task<dynamic> GetAllRequisition()
        {
            var InvitationList = new List<Invitation>();
            await Task.Run(() => _vendorCSActualAprvService.GetAllRequisition(UserID, out InvitationList));
            return Json(new { InvitationList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetAllCSRecDataforAcc()
        {
            var InvitationList = new List<Invitation>();
            await Task.Run(() => _vendorCSActualAprvService.GetAllCSRecDataforAcc(UserID, out InvitationList));
            return Json(new { InvitationList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetAllCSRecDataforVerify()
        {
            var InvitationList = new List<Invitation>();
            await Task.Run(() => _vendorCSActualAprvService.GetAllCSRecDataforVerify(UserID, out InvitationList));
            return Json(new { InvitationList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetAllApprovedData()
        {
            var InvitationList = new List<Invitation>();
            await Task.Run(() => _vendorCSActualAprvService.GetAllApprovedData(UserID, out InvitationList));
            return Json(new { InvitationList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public async Task<dynamic> GetMaterialByRequisition(string VendorRequisitionNumber)
        {
            var ReqWiseMaterialList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSActualAprvService.GetMaterialByRequisition(VendorRequisitionNumber, out ReqWiseMaterialList));
            return Json(new { ReqWiseMaterialList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> GetVendorByMaterial(string VendorReqID, string ServiceItemID)
        {
            var MatWiseVendorList = new List<OBS_VendorCSAprvItem>();
            await Task.Run(() => _vendorCSActualAprvService.GetVendorByMaterialService(VendorReqID, ServiceItemID, out MatWiseVendorList));
            return Json(new { MatWiseVendorList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<dynamic> SearchCS()
        {
            var SearchCSList = new List<Invitation>();
            await Task.Run(() => _vendorCSActualAprvService.SearchCSService(UserID, out SearchCSList));
            return Json(new { SearchCSList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public async Task<dynamic> CSVendor(string CSNumber)
        {
            var VendorCSList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => _vendorCSActualAprvService.CSVendorService(UserID, CSNumber, out VendorCSList));
            return Json(new { VendorCSList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> CSVendorTerms(string CSNumber)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => _vendorCSActualAprvService.CSVendorTerms(CSNumber, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

    }
}