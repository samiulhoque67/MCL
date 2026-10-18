using SILDMS.Model;
using SILDMS.Service.ClientBillCollection;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class ClientBillCollectionController : Controller
    {
        private readonly IClientBillCollectionService clientBillCollectionService;
        private readonly string UserID;

        public ClientBillCollectionController(IClientBillCollectionService _clientBillCollectionService)
        {
            clientBillCollectionService = _clientBillCollectionService;
            UserID = SILAuthorization.GetUserID();
        }

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetClientBillCollectionList()
        {
            var list = new List<OBS_ClientBillCollection>();
            await Task.Run(() => clientBillCollectionService.GetClientBillCollectionList(out list));
            var result = Json(new { ClientBillCollectionList = list, msg = "ClientBillCollectionList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientBillCollectionItem(string ClientFinalBillID)
        {
            var list = new List<OBS_ClientBillCollectionItem>();
            await Task.Run(() => clientBillCollectionService.GetClientBillCollectionItem(ClientFinalBillID, out list));
            var result = Json(new { ClientBillCollectionItemList = list, msg = "ClientBillCollectionItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveClientBillCollectionInfo(OBS_ClientBillCollection collection)
        {
            collection.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = clientBillCollectionService.SaveClientBillCollectionInfo(collection));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}