using SILDMS.Service.AdvanceApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SILDMS.Utillity;
using SILDMS.Web.UI.Areas.SecurityModule.Models;

using SILDMS.Utillity.Localization;
using SILDMS.Model;
using System.Threading.Tasks;

namespace SILDMS.Web.UI.Controllers
{
    public class AdvanceApprovalController : Controller
    {

        readonly IAdvanceApprovalService _advanceApprovalService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;


        public AdvanceApprovalController(IAdvanceApprovalService repository, ILocalizationService localizationService)
        {
            this._advanceApprovalService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }


        // GET: AdvanceApproval
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<POinfo>();
            await Task.Run(() => _advanceApprovalService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }


        [HttpPost]
        public async Task<dynamic> AvailableClientDetailInfo(string ClientID, string VendrAdvncDemnID)
        {
            var ClientDetails = new List<POinfo>();  // Renamed to ClientDetails
            await Task.Run(() => _advanceApprovalService.AvailableClientDetailInfoService(ClientID, VendrAdvncDemnID, out ClientDetails));
            var result = Json(new { ClientDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<AdvanceDemandMaster> MasterData)
        {
            string VendorAdvancAprvID = string.Empty;//, message = string.Empty;

            if (MasterData == null || !MasterData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _advanceApprovalService.SaveQuotToClientService(UserID, MasterData);
                if (status != string.Empty)
                {
                    string[] statusarr = status.Split(',');
                    VendorAdvancAprvID = statusarr[1];
                    /*clientReq.ClientReqID = statusarr[1];*/
                    status = statusarr[0];
                }

                return Json(new { status, VendorAdvancAprvID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}