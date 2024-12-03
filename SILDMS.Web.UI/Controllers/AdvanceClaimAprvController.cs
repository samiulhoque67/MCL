using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using SILDMS.Model;
using System.Threading.Tasks;
using SILDMS.Service.AdvanceClaimAprvClient;

namespace SILDMS.Web.UI.Controllers
{
    public class AdvanceClaimAprvController : Controller
    {
        readonly IAdvanceClaimAprvClientService _advanceClaimAprvClientService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;
        public AdvanceClaimAprvController(IAdvanceClaimAprvClientService repository, ILocalizationService localizationService)
        {
            this._advanceClaimAprvClientService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }


        // GET: AdvanceClaimAprv
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();
            await Task.Run(() => _advanceClaimAprvClientService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> WoQtforAdvanClaim(string ClientID, string WOInfoID, string AdvancClaimID, string AdvancClaimRecmID)
        {
            var WODetails = new List<AdvanClaimWo>();  // Renamed to ClientDetails
            await Task.Run(() => _advanceClaimAprvClientService.WoQtforAdvanClaimService(ClientID, WOInfoID, AdvancClaimID, AdvancClaimRecmID, out WODetails));
            var result = Json(new { WODetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }



        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<AdvanceClaimMaster> MasterData, string Operation)
        {
            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _advanceClaimAprvClientService.SaveQuotToClientService(UserID, MasterData, Operation);
                return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}