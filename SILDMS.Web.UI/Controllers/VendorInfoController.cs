using Microsoft.Owin.BuilderProperties;
using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorInfoController : Controller
    {
        readonly IVendorInfoService _vendorInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorInfoController(IVendorInfoService repository, ILocalizationService localizationService)
        {
            this._vendorInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /VendorInfo/Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<dynamic> SaveVendorwithMat(string VendorCode,string VendorName, string ContactPerson, string ContactNumber, string Email,
            string VendorTinNo, string VendorBinNo,string VAddress, string BankName, string AccountName, string AccountNumber, string RoutingNumber, List<OBS_ServicesCategory> ServiceItemInfo, int VendorStatus, string TempVendorID =  null)

        {
            string Status = string.Empty, message = string.Empty;

            Status = _vendorInfoService.SaveVendorwithMatService(UserID, VendorCode, VendorName, ContactPerson, ContactNumber, Email,
            VendorTinNo,  VendorBinNo,  VAddress, BankName, AccountName, AccountNumber, RoutingNumber, ServiceItemInfo, VendorStatus, TempVendorID, out Status);
            if (Status == "Success" || Status != null || Status != "")
            {
                message = "Data Saved Successfully";
                return Json(new { Status, message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                message = "Error Found";
                return Json(new { Status, message }, JsonRequestBehavior.AllowGet);

            }
        }
        public async Task<dynamic> GetAllListedVendors(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var ListedVendorsList = new List<OBS_VendorInfo>();
            await Task.Run(() => _vendorInfoService.GetAllListedVendorsService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out ListedVendorsList));
            var result = Json(new { ListedVendorsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Authorize]
        public async Task<dynamic> GetAllMaterial()
        {
            string MaterialCategory = null;
            var MaterialList = new List<OBS_ServicesCategory>();
            await Task.Run(() => _vendorInfoService.GetAllMaterialService(out MaterialList));
            return Json(new { MaterialList, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

    }
}