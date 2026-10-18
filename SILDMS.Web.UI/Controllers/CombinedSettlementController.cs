using SILDMS.Model;
using SILDMS.Service.CombinedSettlement;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    // Read-only. Attachment links on the page point at the existing
    // ClientSettlement / VendorSettlement DownloadSettlementDocument actions, so the
    // FTP download logic lives in exactly one place per side.
    public class CombinedSettlementController : Controller
    {
        private readonly ICombinedSettlementService combinedSettlementService;

        public CombinedSettlementController(ICombinedSettlementService _combinedSettlementService)
        {
            combinedSettlementService = _combinedSettlementService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetCombinedSettlementList()
        {
            var list = new List<OBS_CombinedSettlement>();
            await Task.Run(() => combinedSettlementService.GetCombinedSettlementList(out list));
            var result = Json(new { CombinedSettlementList = list, msg = "CombinedSettlementList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetCombinedClientBills(string WOInfoID)
        {
            var list = new List<OBS_CombinedClientBill>();
            await Task.Run(() => combinedSettlementService.GetCombinedClientBills(WOInfoID, out list));
            var result = Json(new { CombinedClientBills = list, msg = "CombinedClientBills are loaded." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetCombinedClientEntries(string WOInfoID)
        {
            var list = new List<OBS_ClientSettlementHistoryRow>();
            await Task.Run(() => combinedSettlementService.GetCombinedClientEntries(WOInfoID, out list));
            var result = Json(new { CombinedClientEntries = list, msg = "CombinedClientEntries are loaded." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetCombinedVendorBills(string ClientReqID)
        {
            var list = new List<OBS_CombinedVendorBill>();
            await Task.Run(() => combinedSettlementService.GetCombinedVendorBills(ClientReqID, out list));
            var result = Json(new { CombinedVendorBills = list, msg = "CombinedVendorBills are loaded." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetCombinedVendorEntries(string ClientReqID)
        {
            var list = new List<OBS_VendorSettlementHistoryRow>();
            await Task.Run(() => combinedSettlementService.GetCombinedVendorEntries(ClientReqID, out list));
            var result = Json(new { CombinedVendorEntries = list, msg = "CombinedVendorEntries are loaded." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}