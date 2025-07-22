using SILDMS.Model;
using SILDMS.Service.AdvanceClaimAprvClient;
using SILDMS.Service.ClaimReceived;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System.EnterpriseServices;

namespace SILDMS.Web.UI.Controllers
{
    public class ClaimReceivedController : Controller
    {
        readonly IClaimReceivedService _claimReceivedService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;
        public ClaimReceivedController(IClaimReceivedService repository, ILocalizationService localizationService)
        {
            this._claimReceivedService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }



        // GET: ClaimReceived
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _claimReceivedService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AllSavedAdvanceClaim(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _claimReceivedService.AllSavedAdvanceClaimService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }



        [HttpPost]
        public async Task<dynamic> WoQtforAdvanClaim(string ClientID, string WOInfoID, string AdvancClaimAprvID)
        {
            var WODetails = new List<AdvanClaimWo>();  // Renamed to ClientDetails
            await Task.Run(() => _claimReceivedService.WoQtforAdvanClaimService(ClientID, WOInfoID,  AdvancClaimAprvID, out WODetails));
            var result = Json(new { WODetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<dynamic> AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimAprvID, string AdvancClaimRcvdID)
        {
            var WODetails = new List<AdvanClaimWo>();  // Renamed to ClientDetails
            await Task.Run(() => _claimReceivedService.AllSavedAdvanceClaimDetails(ClientID, WOInfoID, AdvancClaimAprvID, AdvancClaimRcvdID, out WODetails));
            var result = Json(new { WODetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<AdvanceClaimMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo)
        {
            string AdvancClaimRcvdID = string.Empty;//, message = string.Empty;

            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _claimReceivedService.SaveQuotToClientService(UserID, MasterData, TransactionMode, ParticularNo, MoneyReceiptNo);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    AdvancClaimRcvdID = statusarr[1];
                    /*clientReq.ClientReqID = statusarr[1];*/
                    status = statusarr[0];
                }

                return Json(new { status, AdvancClaimRcvdID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }



        }
    }
}