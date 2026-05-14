using SILDMS.Model;
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
    public class QuotationRecommendationController : Controller
    {
        readonly IQuotationRecommendationService _quotationRecommendationService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;


        public QuotationRecommendationController(IQuotationRecommendationService repository, ILocalizationService localizationService)
        {
            this._quotationRecommendationService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        // GET: QuotationRecommendation

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [Authorize]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationRecommendationService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }



        [HttpPost]
        [Authorize]
        public async Task<dynamic> AllSavcdClientsQuotation(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _quotationRecommendationService.AllSavcdClientsQuotationService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }



        [HttpPost]
        [Authorize]
        public async Task<dynamic> GetVendorTermList(string ClientQuotationID)
        {
            var VendorTermTermList = new List<OBS_TermsItem>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationRecommendationService.GetVendorTermListService(ClientQuotationID, out VendorTermTermList));
            var result = Json(new { VendorTermTermList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<dynamic> GetClientReqDataInfo(string ClientID, string ClientReqID, string ClientQutnID)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationRecommendationService.GetClientReqDataInfoService(ClientID, ClientReqID, ClientQutnID, out GetClientReqDetails));
            var result = Json(new { GetClientReqDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveQuotToClient(List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            string ClientQutnRecmID = string.Empty;
            if (MasterData == null || !MasterData.Any() || DetailData == null || !DetailData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _quotationRecommendationService.SaveQuotToClientService(UserID, MasterData, DetailData, AllTermsDtl);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    ClientQutnRecmID = statusarr[1];
                    status = statusarr[0];
                }
                return Json(new { status = status, ClientQutnRecmID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}