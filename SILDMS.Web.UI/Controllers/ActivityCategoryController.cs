using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;

namespace SILDMS.Web.UI.Controllers
{
    public class ActivityCategoryController : Controller
    {
        
        readonly IServicesCategoryService _servicesCategoryService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ActivityCategoryController(IServicesCategoryService repository, ILocalizationService localizationService)
        {
            this._servicesCategoryService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        [SILAuthorize]
        [OutputCache(Duration = 2000, VaryByParam = "none", Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<dynamic> GetServicesCategory(string servicesCategoryId)
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _servicesCategoryService.GetServicesCategory("", UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServicesCategoryID = x.ServicesCategoryID,
                ServicesCategoryCode = x.ServicesCategoryCode,
                ServicesCategoryName = x.ServicesCategoryName,
                //LevelSL = x.LevelSL,
                SetOn = x.SetOn,
                SetBy = x.SetBy,
                ModifiedOn = x.ModifiedOn,
                MdifiedBy = x.ModifiedBy,
                Status = x.Status.ToString()
            }).OrderByDescending(ob => ob.ServicesCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> AddServicesCategory(OBS_ServicesCategory objServicesCategory)
        {
            //if (ModelState.IsValid)
            //{
                action = "add";
                objServicesCategory.SetBy = UserID;
                objServicesCategory.ModifiedBy = objServicesCategory.SetBy;
                respStatus = await Task.Run(() => _servicesCategoryService.AddServicesCategory(objServicesCategory, action, out outStatus));
                // Error handling.   
                return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    respStatus = new ValidationResult("E404", _localizationService.GetResource("E404"));
            //}
            return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> EditServicesCategory(OBS_ServicesCategory obServicesCategory)
        {
           
                action = "edit";
                obServicesCategory.SetBy = UserID;
                obServicesCategory.ModifiedBy = obServicesCategory.SetBy;
                respStatus = await Task.Run(() => _servicesCategoryService.AddServicesCategory(obServicesCategory, action, out outStatus));
                return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
           
        }


        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> DeleteServicesCategory(OBS_ServicesCategory menu)
        {
            action = "delete";
            respStatus = await Task.Run(() => _servicesCategoryService.AddServicesCategory(menu, action, out outStatus));
            return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
        }

    }
}