using SILDMS.Model;
using SILDMS.Service.VendorFinalBill;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorFinalBillController : Controller
    {
        private readonly IVendorFinalBillService vendorFinalBillService;
        private readonly string UserID;

        public VendorFinalBillController(IVendorFinalBillService _vendorFinalBillService)
        {
            vendorFinalBillService = _vendorFinalBillService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetVendorFinalBillClientInfo()
        {
            var list = new List<OBS_VendorFinalBill>();
            await Task.Run(() => vendorFinalBillService.GetVendorFinalBillClientInfo(out list));
            var result = Json(new { VendorFinalBillList = list, msg = "VendorFinalBillList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorFinalBillItem(string POAprvID)
        {
            var list = new List<OBS_VendorFinalBillItem>();
            await Task.Run(() => vendorFinalBillService.GetVendorFinalBillItem(POAprvID, out list));
            var result = Json(new { VendorFinalBillItemList = list, msg = "VendorFinalBillItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorAdvanceDemandHistory(string POAprvID)
        {
            var list = new List<OBS_VendorDemandHistoryRow>();
            await Task.Run(() => vendorFinalBillService.GetVendorAdvanceDemandHistory(POAprvID, out list));
            var result = Json(new { VendorAdvanceDemandHistory = list, msg = "VendorAdvanceDemandHistory are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorFinalBillHistory(string POAprvID)
        {
            var list = new List<OBS_VendorFinalBillHistoryRow>();
            await Task.Run(() => vendorFinalBillService.GetVendorFinalBillHistory(POAprvID, out list));
            var result = Json(new { VendorFinalBillHistory = list, msg = "VendorFinalBillHistory are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorFinalBillTermList(string POAprvID)
        {
            var list = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => vendorFinalBillService.GetVendorFinalBillTermList(POAprvID, out list));
            var result = Json(new { VendorFinalBillTermList = list, msg = "VendorFinalBillTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorFinalBillInfo(OBS_VendorFinalBill vendorFinalBill, List<OBS_VendorFinalBillItem> vendorFinalBillItem)
        {
            vendorFinalBill.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = vendorFinalBillService.SaveVendorFinalBillInfo(vendorFinalBill, vendorFinalBillItem));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}