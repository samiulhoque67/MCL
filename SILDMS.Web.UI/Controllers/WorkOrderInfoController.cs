using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
//using SILDMS.Service.WorkOrderInfo;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class WorkOrderInfoController : Controller
    {
        readonly IWorkOrderInfoService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public WorkOrderInfoController(IWorkOrderInfoService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /WorkOrderInfo/Index
        public ActionResult Index()
        {
            return View();//
        }
        [HttpGet]
        //[Authorize]
        public async Task<dynamic> GetServicesCategory()
        {
            //UserID = SILAuthorization.GetUserID();
            List<OBS_ServicesCategory> obServicesCategory = null;
            await Task.Run(() => _clientInfoService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServiceCategoryID = x.ServicesCategoryID,
                ServiceCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ServiceCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetClientInfoList()
        {
            var ClientInfoList = new List<OBS_WOInfo>();
            await Task.Run(() => _clientInfoService.GetClientInfoList(out ClientInfoList));
            var result = Json(new { ClientInfoList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetWOInfoItemList(string ClientQutnAprvID)
        {
            var WOInfoItemList = new List<OBS_WOInfoItem>();
            await Task.Run(() => _clientInfoService.GetWOInfoItemList(ClientQutnAprvID, out WOInfoItemList));
            var result = Json(new { WOInfoItemList, msg = "WOInfoItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoTermList(string ClientQutnAprvID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoTermList(ClientQutnAprvID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveWorkOrderInfo(OBS_WOInfo woInfo, List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm)
        {
            woInfo.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveWorkOrderInfo(woInfo, woInfoItem, woInfoTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetWOInfoSearchList()
        {
            var woInfoSearchList = new List<OBS_WOInfo>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchList(out woInfoSearchList));
            var result = Json(new { woInfoSearchList, msg = "woInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoSearchItemList(string WOInfoID)
        {
            var WOInfoItemList = new List<OBS_WOInfoItem>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchItemList(WOInfoID, out WOInfoItemList));
            var result = Json(new { WOInfoItemList, msg = "WOInfoItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoSearchTermsList(string WOInfoID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoSearchTermsList(WOInfoID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteWOInfoItemAndTerm(WOInfoItemID, WOInfoTermID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsConditionsList()
        {
            var termsConditionsList = new List<OBS_Terms>();
            await Task.Run(() => _clientInfoService.GetTermsConditionsList(out termsConditionsList));
            var result = Json(new { termsConditionsList, msg = "Terms Conditions List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetWOInfoTermAgainstFormList(string TermsID)
        {
            var WOInfoTermList = new List<OBS_WOInfoTerms>();
            await Task.Run(() => _clientInfoService.GetWOInfoTermAgainstFormList(TermsID, out WOInfoTermList));
            var result = Json(new { WOInfoTermList, msg = "WOInfoTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}