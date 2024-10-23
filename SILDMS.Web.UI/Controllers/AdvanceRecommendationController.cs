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
            var AllAvailableClientsList = new List<AdvanceDemandMaster>();
            await Task.Run(() => _advanceRecommendationService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

    }
}