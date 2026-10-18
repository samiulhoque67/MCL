using SILDMS.Model;
using SILDMS.Service.VendorDemand;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorDemandController : Controller
    {
        private readonly IVendorDemandService vendorDemandService;
        private readonly string UserID;

        public VendorDemandController(IVendorDemandService _vendorDemandService)
        {
            vendorDemandService = _vendorDemandService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetVendorDemandClientInfo()
        {
            var list = new List<OBS_VendorDemand>();
            await Task.Run(() => vendorDemandService.GetVendorDemandClientInfo(out list));
            var result = Json(new { VendorDemandList = list, msg = "VendorDemandList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorDemandItem(string POAprvID)
        {
            var list = new List<OBS_VendorDemandItem>();
            await Task.Run(() => vendorDemandService.GetVendorDemandItem(POAprvID, out list));
            var result = Json(new { VendorDemandItemList = list, msg = "VendorDemandItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorDemandTermList(string POAprvID)
        {
            var list = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => vendorDemandService.GetVendorDemandTermList(POAprvID, out list));
            var result = Json(new { VendorDemandTermList = list, msg = "VendorDemandTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorDemandInfo(OBS_VendorDemand vendorDemand)
        {
            vendorDemand.SetBy = UserID;
            string status = null;
            await Task.Run(() => status = vendorDemandService.SaveVendorDemandInfo(vendorDemand));
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
    }
}