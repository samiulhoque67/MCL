using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SILDMS.Utillity;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using SILDMS.Utillity.Localization;
using SILDMS.Service.AdvanceRecommendation;
using SILDMS.Model;
using System.Threading.Tasks;


namespace SILDMS.Web.UI.Controllers
{
    public class AdvanceRecommendationController : Controller
    {
        readonly IAdvanceRecommendationService _advanceRecommendationService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;


        public AdvanceRecommendationController(IAdvanceRecommendationService repository, ILocalizationService localizationService)
        {
            this._advanceRecommendationService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        // GET: AdvanceRecommendation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<POinfo>();
            await Task.Run(() => _advanceRecommendationService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }


        [HttpPost]
        public async Task<dynamic> AvailableClientDetailInfo(string ClientID, string VendrAdvncDemnID)
        {
            var ClientDetails = new List<POinfo>();  // Renamed to ClientDetails
            await Task.Run(() => _advanceRecommendationService.AvailableClientDetailInfoService(ClientID, VendrAdvncDemnID, out ClientDetails));
            var result = Json(new { ClientDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<AdvanceDemandMaster> MasterData)
        {
            string VendorAdvancRecmID = string.Empty;//, message = string.Empty;

            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _advanceRecommendationService.SaveQuotToClientService(UserID, MasterData);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    VendorAdvancRecmID = statusarr[1];
                    /*clientReq.ClientReqID = statusarr[1];*/
                    status = statusarr[0];
                }

                return Json(new { status, VendorAdvancRecmID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}