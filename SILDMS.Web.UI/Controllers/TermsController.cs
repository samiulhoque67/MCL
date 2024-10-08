﻿using SILDMS.Model;
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
    public class TermsController : Controller
    {
        readonly ITermsService _termsService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public TermsController(ITermsService repository, ILocalizationService localizationService)
        {
            this._termsService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /Terms/Index
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
            await Task.Run(() => _termsService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                TermsCategoryID = x.ServicesCategoryID,
                TermsCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.TermsCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> SaveTermsMst(OBS_Terms _modelTermsMst, List<OBS_TermsItem> itemList)
        {
            _modelTermsMst.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _termsService.SaveTermsMst(_modelTermsMst);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveTermsItem(OBS_TermsItem _modelTermsItem)
        {
            _modelTermsItem.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _termsService.SaveTermsItem(_modelTermsItem);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> DeleteTermsItem(OBS_TermsItem _modelTermsItem)
        {
            _modelTermsItem.Action = "delete";
            string status = string.Empty;//, message = string.Empty;
            status = _termsService.SaveTermsItem(_modelTermsItem);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsSearchList()
        {
            var TermsSearchList = new List<OBS_Terms>();
            await Task.Run(() => _termsService.GetTermsSearchList(out TermsSearchList));
            var result = Json(new { TermsSearchList, msg = "TermsSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetTermsItemList(string TermsID)
        {
            var TermsItemList = new List<OBS_TermsItem>();
            await Task.Run(() => _termsService.GetTermsItemList(TermsID, out TermsItemList));
            var result = Json(new { TermsItemList, msg = "TermsSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}