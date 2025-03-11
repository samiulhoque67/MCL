using SILDMS.Model;
using SILDMS.Service;
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
    public class FinalSettlementController : Controller
    {
        // GET: FinalSettlement
        readonly IFinalSettlementService _finalSettlementService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public FinalSettlementController(IFinalSettlementService finalSettlementService, ILocalizationService localizationService)
        {
            _finalSettlementService = finalSettlementService;
            _localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetShowfinalSettlementList()
        {
            var finalSettlementList = new List<OBS_FinalSettlement>();
            await Task.Run(() => _finalSettlementService.GetShowfinalSettlementList(out finalSettlementList));
            var result = Json(new { finalSettlementList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        [HttpPost]
        public async Task<dynamic> SaveFinalSettlement(OBS_FinalSettlement BillRecv)
        {
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _finalSettlementService.SavefinalSettlement(BillRecv);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetfinalSettlementSearchList()
        {
            var finalSettlementSearchList = new List<OBS_FinalSettlement>();
            await Task.Run(() => _finalSettlementService.GetfinalSettlementSearchList(out finalSettlementSearchList));
            var result = Json(new { finalSettlementSearchList, msg = "finalSettlementSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}