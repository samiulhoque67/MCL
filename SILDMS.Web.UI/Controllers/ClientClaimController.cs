using SILDMS.Model;
using SILDMS.Service.ClientClaim;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class ClientClaimController : Controller
    {
        private readonly IClientClaimService clientClaimService;
        private readonly string UserID;

        public ClientClaimController(IClientClaimService _clientClaimService)
        {
            clientClaimService = _clientClaimService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetClientClaimClientInfo()
        {
            var list = new List<OBS_ClientClaim>();
            await Task.Run(() => clientClaimService.GetClientClaimClientInfo(out list));
            var result = Json(new { ClientClaimList = list, msg = "ClientClaimList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientClaimItem(string WOInfoID)
        {
            var list = new List<OBS_ClientClaimItem>();
            await Task.Run(() => clientClaimService.GetClientClaimItem(WOInfoID, out list));
            var result = Json(new { ClientClaimItemList = list, msg = "ClientClaimItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientClaimTermList(string WOInfoID)
        {
            var list = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => clientClaimService.GetClientClaimTermList(WOInfoID, out list));
            var result = Json(new { ClientClaimTermList = list, msg = "ClientClaimTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveClientClaimInfo(OBS_ClientClaim clientClaim)
        {
            clientClaim.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = clientClaimService.SaveClientClaimInfo(clientClaim));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}