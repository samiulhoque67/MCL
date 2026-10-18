using SILDMS.Model;
using SILDMS.Service.VendorBillPayment;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorBillPaymentController : Controller
    {
        private readonly IVendorBillPaymentService vendorBillPaymentService;
        private readonly string UserID;

        public VendorBillPaymentController(IVendorBillPaymentService _vendorBillPaymentService)
        {
            vendorBillPaymentService = _vendorBillPaymentService;
            UserID = SILAuthorization.GetUserID();
        }

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetVendorBillPaymentList()
        {
            var list = new List<OBS_VendorBillPayment>();
            await Task.Run(() => vendorBillPaymentService.GetVendorBillPaymentList(out list));
            var result = Json(new { VendorBillPaymentList = list, msg = "VendorBillPaymentList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorBillPaymentItem(string VendorFinalBillID)
        {
            var list = new List<OBS_VendorBillPaymentItem>();
            await Task.Run(() => vendorBillPaymentService.GetVendorBillPaymentItem(VendorFinalBillID, out list));
            var result = Json(new { VendorBillPaymentItemList = list, msg = "VendorBillPaymentItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorBillPaymentInfo(OBS_VendorBillPayment payment)
        {
            payment.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = vendorBillPaymentService.SaveVendorBillPaymentInfo(payment));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}