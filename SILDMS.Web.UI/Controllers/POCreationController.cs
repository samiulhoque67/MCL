using SILDMS.Model;
using SILDMS.Service.DashboardV2;
using SILDMS.Service.PoCreation;
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
    public class POCreationController : Controller
    {

        private readonly IPOCreationService pOCreationService;
        private ValidationResult _respStatus;
        private readonly ILocalizationService _localizationService;
        private readonly string UserID;

        public POCreationController(IPOCreationService _pOCreationService)
        {
            pOCreationService = _pOCreationService;
            _respStatus = new ValidationResult();
            UserID = SILAuthorization.GetUserID();
        }

        // GET: POCreation
        public ActionResult Index()
        {
            return View();
        }
        public async Task<dynamic> GetPOCreationDashBordData()
        {
            List<OBS_VendorCSRecmItem> result = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => pOCreationService.GetPOCreationDashBordData(UserID, out result));
            //var TotalPendingParking = pd.Count();
            //var OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count();
            //var WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count();

            //return Json(new { TotalPendingParking = pd.Count(), OverBenchmark = pd.Where(o => Convert.ToInt32(o.BenchMark) < 0).Count(), WithinBenchMark = pd.Where(o => Convert.ToInt32(o.BenchMark) >= 0).Count() }, JsonRequestBehavior.AllowGet);
            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetPoCreationClientInfo(string ServiceCategoryID)
        {
            var CSClientList = new List<OBS_ClientReq>();
            await Task.Run(() => pOCreationService.GetPoCreationClientInfo(ServiceCategoryID, out CSClientList));
            var result = Json(new { CSClientList, msg = "CSClientList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> OBS_GetPOVendorsUsingClient(string ClientID,string VendorCSAprvID, string ServiceCategoryID)
        {
            var CSVendorList = new List<OBS_VendorCSRecm>();
            await Task.Run(() => pOCreationService.OBS_GetPOVendorsUsingClient(ClientID, VendorCSAprvID, ServiceCategoryID, out CSVendorList));
            var result = Json(new { CSVendorList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        } 
        
        public async Task<dynamic> GetVendorPOQuotationItem(string VendorID, string ClientID, string ServiceCategoryID,string VendorCSAprvID)
        {
            var VenCSItemList = new List<OBS_VendorCSRecmItem>();
            await Task.Run(() => pOCreationService.GetVendorPOQuotationItem(VendorID,ClientID, ServiceCategoryID, VendorCSAprvID, out VenCSItemList));
            var result = Json(new { VenCSItemList, msg = "CSVendorList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorPOInfoTermList(string VendorCSAprvID)
        {
            var VendorCSInfoTermList = new List<OBS_VendorCSRecmTerms>();
            await Task.Run(() => pOCreationService.GetVendorPOInfoTermList(VendorCSAprvID, out VendorCSInfoTermList));
            var result = Json(new { VendorCSInfoTermList, msg = "VendorCSInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorPOInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm, List<OBS_VendorCSRecmVendors> vendorCSItemWise)
        {
            vendorCS.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = pOCreationService.SaveVendorPOInfo(vendorCS, vendorCSItem, vendorCSTerm, vendorCSItemWise);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

    }
}