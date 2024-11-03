using SILDMS.Model;
using SILDMS.Service;
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
    public class QuotationToClientRevisedController : Controller
    {
        readonly IQuotationToClientRevisedService _quotationToClientRevisedService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public QuotationToClientRevisedController(IQuotationToClientRevisedService repository, ILocalizationService localizationService)
        {
            this._quotationToClientRevisedService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /QuotationToClientRevised/Index
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
            await Task.Run(() => _quotationToClientRevisedService.GetServicesCategory(UserID, out obServicesCategory));
            var result = obServicesCategory.Select(x => new
            {
                ServiceCategoryID = x.ServicesCategoryID,
                ServiceCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ServiceCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }
        public async Task<dynamic> GetClientInfoList()
        {
            var ClientInfoList = new List<OBS_QuotationToClientRevised>();
            await Task.Run(() => _quotationToClientRevisedService.GetClientInfoList(out ClientInfoList));
            var result = Json(new { ClientInfoList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
        public async Task<dynamic> GetQuotationToClientRevisedItemList(string ClientQutnAprvID)
        {
            var QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedItem>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedItemList(ClientQutnAprvID, out QuotationToClientRevisedItemList));
            var result = Json(new { QuotationToClientRevisedItemList, msg = "QuotationToClientRevisedItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetQuotationToClientRevisedTermList(string ClientQutnAprvID)
        {
            var QuotationToClientRevisedTermList = new List<OBS_QuotationToClientRevisedTerms>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedTermList(ClientQutnAprvID, out QuotationToClientRevisedTermList));
            var result = Json(new { QuotationToClientRevisedTermList, msg = "QuotationToClientRevisedTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveQuotationToClientRevised(OBS_QuotationToClientRevised woInfo, List<OBS_QuotationToClientRevisedItem> woInfoItem, List<OBS_QuotationToClientRevisedTerms> woInfoTerm)
        {
            woInfo.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _quotationToClientRevisedService.SaveQuotationToClientRevised(woInfo, woInfoItem, woInfoTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetQuotationToClientRevisedSearchList()
        {
            var woInfoSearchList = new List<OBS_QuotationToClientRevised>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedSearchList(out woInfoSearchList));
            var result = Json(new { woInfoSearchList, msg = "woInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetQuotationToClientRevisedSearchItemList(string QuotationToClientRevisedID)
        {
            var QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedItem>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedSearchItemList(QuotationToClientRevisedID, out QuotationToClientRevisedItemList));
            var result = Json(new { QuotationToClientRevisedItemList, msg = "QuotationToClientRevisedItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetQuotationToClientRevisedSearchTermsList(string QuotationToClientRevisedID)
        {
            var QuotationToClientRevisedTermList = new List<OBS_QuotationToClientRevisedTerms>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedSearchTermsList(QuotationToClientRevisedID, out QuotationToClientRevisedTermList));
            var result = Json(new { QuotationToClientRevisedTermList, msg = "QuotationToClientRevisedTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteQuotationToClientRevisedItemAndTerm(string QuotationToClientRevisedItemID, string QuotationToClientRevisedTermID)
        {
            string status = string.Empty;
            status = _quotationToClientRevisedService.DeleteQuotationToClientRevisedItemAndTerm(QuotationToClientRevisedItemID, QuotationToClientRevisedTermID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetTermsConditionsList()
        {
            var termsConditionsList = new List<OBS_Terms>();
            await Task.Run(() => _quotationToClientRevisedService.GetTermsConditionsList(out termsConditionsList));
            var result = Json(new { termsConditionsList, msg = "Terms Conditions List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetQuotationToClientRevisedTermAgainstFormList(string TermsID)
        {
            var QuotationToClientRevisedTermList = new List<OBS_QuotationToClientRevisedTerms>();
            await Task.Run(() => _quotationToClientRevisedService.GetQuotationToClientRevisedTermAgainstFormList(TermsID, out QuotationToClientRevisedTermList));
            var result = Json(new { QuotationToClientRevisedTermList, msg = "QuotationToClientRevisedTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}