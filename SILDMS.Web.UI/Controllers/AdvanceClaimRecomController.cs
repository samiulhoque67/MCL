using SILDMS.Service.AdvanceClaim;
using SILDMS.Service.AdvanceClaimRecomClient;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using SILDMS.Model;
using System.Threading.Tasks;


namespace SILDMS.Web.UI.Controllers
{
    public class AdvanceClaimRecomController : Controller
    {
        readonly IAdvanceClaimRecomClientService _advanceClaimRecomClientService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;
        public AdvanceClaimRecomController(IAdvanceClaimRecomClientService repository, ILocalizationService localizationService)
        {
            this._advanceClaimRecomClientService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }


        // GET: AdvanceClaimRecom
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _advanceClaimRecomClientService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AllSavedAdvanceClaim(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _advanceClaimRecomClientService.AllSavedAdvanceClaimService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> WoQtforAdvanClaim(string ClientID, string WOInfoID, string AdvancClaimID)
        {
            var WODetails = new List<AdvanClaimWo>();  // Renamed to ClientDetails
            await Task.Run(() => _advanceClaimRecomClientService.WoQtforAdvanClaimService(ClientID, WOInfoID, AdvancClaimID, out WODetails));
            var result = Json(new { WODetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<dynamic> AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimRecmID)
        {
            var WODetails = new List<AdvanClaimWo>();  // Renamed to ClientDetails
            await Task.Run(() => _advanceClaimRecomClientService.AllSavedAdvanceClaimDetails(ClientID, WOInfoID, AdvancClaimRecmID, out WODetails));
            var result = Json(new { WODetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<AdvanceClaimMaster> MasterData, string Operation)
        {
           
            string AdvancClaimRecmID = string.Empty;//, message = string.Empty;

            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _advanceClaimRecomClientService.SaveQuotToClientService(UserID, MasterData, Operation);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    AdvancClaimRecmID = statusarr[1];
                    /*clientReq.ClientReqID = statusarr[1];*/
                    status = statusarr[0];
                }

                return Json(new { status, AdvancClaimRecmID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}