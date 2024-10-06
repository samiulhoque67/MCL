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
    public class ServiceItemInfoController : Controller
    {
        readonly IServiceItemInfoService _ServiceItemInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ServiceItemInfoController(IServiceItemInfoService repository, ILocalizationService localizationService)
        {
            this._ServiceItemInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /ServiceItemInfo/Index
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
            await Task.Run(() => _ServiceItemInfoService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServiceItemCategoryID = x.ServicesCategoryID,
                ServiceItemCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ServiceItemCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> SaveServiceItemInfo(OBS_ServiceItemInfo _modelServiceItemInfo)
        {
            _modelServiceItemInfo.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _ServiceItemInfoService.SaveServiceItemInfo(_modelServiceItemInfo);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> DeleteServiceItemInfo(OBS_ServiceItemInfo _modelServiceItemInfo)
        {
            _modelServiceItemInfo.Action = "delete";
            string status = string.Empty;//, message = string.Empty;
            status = _ServiceItemInfoService.SaveServiceItemInfo(_modelServiceItemInfo);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetServiceItemInfoSearchList(string ServiceItemCategoryID)
        {
            var ServiceItemInfoSearchList = new List<OBS_ServiceItemInfo>();
            await Task.Run(() => _ServiceItemInfoService.GetServiceItemInfoSearchList(ServiceItemCategoryID,out ServiceItemInfoSearchList));
            var result = Json(new { ServiceItemInfoSearchList, msg = "ServiceItemInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}