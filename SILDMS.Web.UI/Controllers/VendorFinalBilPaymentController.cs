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
    public class VendorFinalBilPaymentController : Controller
    {
        // GET: VendorFinalBilPayment
        readonly IVendorFinalBilPaymentService _aprvFinalBillRcvdService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorFinalBilPaymentController(IVendorFinalBilPaymentService aprvFinalBillRcvdService, ILocalizationService localizationService)
        {
            _aprvFinalBillRcvdService = aprvFinalBillRcvdService;
            _localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<OBS_VendorFinalBilPayment>();
            await Task.Run(() => _aprvFinalBillRcvdService.GetShowVendorReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorFinalBill(OBS_VendorFinalBilPayment BillRecv)
        {
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _aprvFinalBillRcvdService.SaveVendorFinalBill(BillRecv);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetPOSearchList()
        {
            var PoSearchList = new List<OBS_VendorFinalBilPayment>();
            await Task.Run(() => _aprvFinalBillRcvdService.GetPOSearchList(out PoSearchList));
            var result = Json(new { PoSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}