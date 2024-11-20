using SILDMS.Service.QuotationRecommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System.Web.Mvc;
using SILDMS.Service.QuotationApproval;
using SILDMS.Model;
using System.Threading.Tasks;

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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationApprovalService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> GetClientReqDataInfo(string ClientID, string ClientReqID)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationApprovalService.GetClientReqDataInfoService(ClientID, ClientReqID,  out GetClientReqDetails));
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
            if (MasterData == null || !MasterData.Any() || DetailData == null || !DetailData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _quotationApprovalService.SaveQuotToClientService(UserID, MasterData, DetailData, AllTermsDtl);
                return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}