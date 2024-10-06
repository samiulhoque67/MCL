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

        [HttpGet]
        //[Authorize]
        public async Task<dynamic> GetServicesCategory()
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _vendorInfoService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                VendorCategoryID = x.ServicesCategoryID,
                VendorCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.VendorCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> SaveVendorInfoMst(OBS_VendorInfo _modelVendorInfoMst)
        {
            _modelVendorInfoMst.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorInfoService.SaveVendorInfoMst(_modelVendorInfoMst);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveVendorAddress(OBS_VendorAddressInfo _modelVendorAddress)
        {
            _modelVendorAddress.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorInfoService.SaveVendorAddress(_modelVendorAddress);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> DeleteVendorAddress(OBS_VendorAddressInfo _modelVendorAddress)
        {
            _modelVendorAddress.Action = "delete";
            string status = string.Empty;//, message = string.Empty;
            status = _vendorInfoService.SaveVendorAddress(_modelVendorAddress);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetVendorInfoSearchList()
        {
            var VendorInfoSearchList = new List<OBS_VendorInfo>();
            await Task.Run(() => _vendorInfoService.GetVendorInfoSearchList(out VendorInfoSearchList));
            var result = Json(new { VendorInfoSearchList, msg = "VendorInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetVendorAddressList(string VendorID)
        {
            var VendorAddressList = new List<OBS_VendorAddressInfo>();
            await Task.Run(() => _vendorInfoService.GetVendorAddressList(VendorID, out VendorAddressList));
            var result = Json(new { VendorAddressList, msg = "VendorInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}