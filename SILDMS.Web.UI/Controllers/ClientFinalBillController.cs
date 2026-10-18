using SILDMS.Model;
using SILDMS.Service.ClientFinalBill;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class ClientFinalBillController : Controller
    {
        private readonly IClientFinalBillService clientFinalBillService;
        private readonly string UserID;

        public ClientFinalBillController(IClientFinalBillService _clientFinalBillService)
        {
            clientFinalBillService = _clientFinalBillService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetClientFinalBillClientInfo()
        {
            var list = new List<OBS_ClientFinalBill>();
            await Task.Run(() => clientFinalBillService.GetClientFinalBillClientInfo(out list));
            var result = Json(new { ClientFinalBillList = list, msg = "ClientFinalBillList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientFinalBillItem(string WOInfoID)
        {
            var list = new List<OBS_ClientFinalBillItem>();
            await Task.Run(() => clientFinalBillService.GetClientFinalBillItem(WOInfoID, out list));
            var result = Json(new { ClientFinalBillItemList = list, msg = "ClientFinalBillItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientAdvanceClaimHistory(string WOInfoID)
        {
            var list = new List<OBS_ClientAdvanceClaimHistoryRow>();
            await Task.Run(() => clientFinalBillService.GetClientAdvanceClaimHistory(WOInfoID, out list));
            var result = Json(new { ClientAdvanceClaimHistory = list, msg = "ClientAdvanceClaimHistory are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientFinalBillHistory(string WOInfoID)
        {
            var list = new List<OBS_ClientFinalBillHistoryRow>();
            await Task.Run(() => clientFinalBillService.GetClientFinalBillHistory(WOInfoID, out list));
            var result = Json(new { ClientFinalBillHistory = list, msg = "ClientFinalBillHistory are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientFinalBillTermList(string WOInfoID)
        {
            var list = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => clientFinalBillService.GetClientFinalBillTermList(WOInfoID, out list));
            var result = Json(new { ClientFinalBillTermList = list, msg = "ClientFinalBillTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveClientFinalBillInfo(OBS_ClientFinalBill clientFinalBill, List<OBS_ClientFinalBillItem> clientFinalBillItem)
        {
            clientFinalBill.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = clientFinalBillService.SaveClientFinalBillInfo(clientFinalBill, clientFinalBillItem));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}