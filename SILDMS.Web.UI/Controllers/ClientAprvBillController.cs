using SILDMS.Model;
using SILDMS.Service.ClientAprvBill;
using SILDMS.Service.ClientBillRcmd;
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
    public class ClientAprvBillController : Controller
    {
        // GET: ClientAprvBill
        readonly IClientAprvBillService _clientAprvBillService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ClientAprvBillController(IClientAprvBillService clientAprvBillService, ILocalizationService localizationService)
        {
            _clientAprvBillService = clientAprvBillService;
            _localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<VendorBillRecvd>();
            await Task.Run(() => _clientAprvBillService.GetShowClientReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorFinalBill(VendorBillRecvd BillRecv)
        {
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientAprvBillService.SaveClientFinalBill(BillRecv);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveVendorFinalBillRcvd(VendorBillRecvd BillRecv)
        {
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientAprvBillService.SaveVendorFinalBillRcvd(BillRecv);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetQutnSearchList()
        {
            var PoSearchList = new List<VendorBillRecvd>();
            await Task.Run(() => _clientAprvBillService.GetQutnSearchList(out PoSearchList));
            var result = Json(new { PoSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}