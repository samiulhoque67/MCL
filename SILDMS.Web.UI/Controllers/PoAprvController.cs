using SILDMS.Model;
using SILDMS.Service.PoAprv;
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
    public class PoAprvController : Controller
    {
        // GET: PoAprv

        private readonly IPoAprvService poAprvService;
        private ValidationResult _respStatus;
        private readonly ILocalizationService _localizationService;
        private readonly string UserID;

        public PoAprvController(IPoAprvService _poAprvService)
        {
            poAprvService = _poAprvService;
            _respStatus = new ValidationResult();
            UserID = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetPOAprvDashBordData()
        {
            List<OBS_VendorCSRecmItem> result = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => poAprvService.GetPOAprvDashBordData(UserID, out result));
            //var TotalPendingParking = pd.Count();
            //var OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count();
            //var WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count();

            //return Json(new { TotalPendingParking = pd.Count(), OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count(), WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count() }, JsonRequestBehavior.AllowGet);
            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetPoAprvClientInfo(string ServiceCategoryID)
        {
            var CSClientList = new List<OBS_ClientReq>();
            await Task.Run(() => poAprvService.GetPoAprvClientInfo(ServiceCategoryID, out CSClientList));
            var result = Json(new { CSClientList, msg = "CSClientList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }


        public async Task<dynamic> GetVendorPOAprvQuotationItem(string VendorID, string ClientID, string ServiceCategoryID, string PORecmID)
        {
            var VenCSItemList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => poAprvService.GetVendorPOAprvQuotationItem(VendorID, ClientID, ServiceCategoryID, PORecmID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = poAprvService.SaveVendorPOAprvInfo(vendorCS, vendorCSItem, vendorCSTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetPOAprvInfoTermList(string PORecmID)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => poAprvService.GetPOAprvInfoTermList(PORecmID, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

    }
}