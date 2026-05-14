using SILDMS.Model;
using SILDMS.Service.QuotationApproval;
using SILDMS.Service.QuotationRecommendation;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class QuotationApprovalController : Controller
    {
        readonly IQuotationApprovalService _quotationApprovalService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;



        public QuotationApprovalController(IQuotationApprovalService repository, ILocalizationService localizationService)
        {
            this._quotationApprovalService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        // GET: QuotationApproval

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllClientQuotationApprovalData()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, string action)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationApprovalService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, action, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AllSavcdClientQuotationRecommendation(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, string action)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationApprovalService.AllSavcdClientQuotationRecommendationService(UserID, page, itemsPerPage, sortBy, reverse, search, type, action, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AllClientQuotationforApprvData(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, string action)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationApprovalService.AllClientQuotationforApprvData(UserID, page, itemsPerPage, sortBy, reverse, search, type, action, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> GetClientReqDataInfo(string ClientID, string ClientReqID)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationApprovalService.GetClientReqDataInfoService(ClientID, ClientReqID, out GetClientReqDetails));
            var result = Json(new { GetClientReqDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<dynamic> GetClientReqDataInfoAprv(string ClientID, string ClientReqID)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationApprovalService.GetClientReqDataInfoAprvService(ClientID, ClientReqID, out GetClientReqDetails));
            var result = Json(new { GetClientReqDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }



        [HttpPost]
        public async Task<dynamic> GetVendorTermList(string ClientQutnRecmID)
        {

            var VendorTermTermList = new List<OBS_TermsItem>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationApprovalService.GetVendorTermListService(ClientQutnRecmID, out VendorTermTermList));
            var result = Json(new { VendorTermTermList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            string ClientQutnAprvID = string.Empty;//, message = string.Empty;

            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _quotationApprovalService.SaveQuotToClientService(UserID, MasterData, DetailData, AllTermsDtl);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    ClientQutnAprvID = statusarr[1];
                    status = statusarr[0];
                }
                TempData["ClientQutnAprvID"] = ClientQutnAprvID;
                return Json(new { status, ClientQutnAprvID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> SaveQuotToClientReport(List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            string ClientQutnAprvID = string.Empty;//, message = string.Empty;

            try
            {
                ClientQutnAprvID = MasterData[0].ClientQutnAprvID;
                string status = "202";
                TempData["ClientQutnAprvID"] = ClientQutnAprvID;
                return Json(new { status, ClientQutnAprvID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}