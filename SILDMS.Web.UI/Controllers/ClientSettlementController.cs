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
    public class ClientSettlementController : Controller
    {
        // GET: VendorFinalBillReceived
        readonly IClientSettlementService _clientFinalBillPrepareService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ClientSettlementController(IClientSettlementService clientFinalBillPrepareService, ILocalizationService localizationService)
        {
            _clientFinalBillPrepareService = clientFinalBillPrepareService;
            _localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<OBS_ClientSettlement>();
            await Task.Run(() => _clientFinalBillPrepareService.GetShowClientReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        [HttpPost]
        public async Task<dynamic> SaveClientSettlement(OBS_ClientSettlement BillRecv)
        {
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientFinalBillPrepareService.SaveClientSettlement(BillRecv);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetQutnSearchList()
        {
            var PoSearchList = new List<VendorBillRecvd>();
            await Task.Run(() => _clientFinalBillPrepareService.GetQutnSearchList(out PoSearchList));
            var result = Json(new { PoSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}