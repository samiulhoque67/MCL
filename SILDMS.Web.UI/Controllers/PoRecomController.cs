using SILDMS.Model;
using SILDMS.Service.PoCreation;
using SILDMS.Service.PoRecom;
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
    public class PoRecomController : Controller
    {
        // GET: PoRecom

        private readonly IPoRecomService poRecomService;
        private ValidationResult _respStatus;
        private readonly ILocalizationService _localizationService;
        private readonly string UserID;

        public PoRecomController(IPoRecomService _poRecomService)
        {
            poRecomService = _poRecomService;
            _respStatus = new ValidationResult();
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetPORecomDashBordData()
        {
            List<OBS_VendorCSRecmItem> result = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => poRecomService.GetPORecomDashBordData(UserID, out result));
            //var TotalPendingParking = pd.Count();
            //var OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count();
            //var WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count();

            //return Json(new { TotalPendingParking = pd.Count(), OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count(), WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count() }, JsonRequestBehavior.AllowGet);
            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetPoRecomClientInfo(string ServiceCategoryID)
        {
            var CSClientList = new List<OBS_ClientReq>();
            await Task.Run(() => poRecomService.GetPoRecomClientInfo(ServiceCategoryID, out CSClientList));
            var result = Json(new { CSClientList, msg = "CSClientList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }


        public async Task<dynamic> GetVendorPORecomQuotationItem(string VendorID, string ClientID, string ServiceCategoryID, string POPreparationID)
        {
            var VenCSItemList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => poRecomService.GetVendorPORecomQuotationItem(VendorID, ClientID, ServiceCategoryID, POPreparationID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorPORecomInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            vendorCS.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = poRecomService.SaveVendorPORecomInfo(vendorCS, vendorCSItem, vendorCSTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetPORecomInfoTermList(string POPreparationID)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => poRecomService.GetPORecomInfoTermList(POPreparationID, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }


    }
}