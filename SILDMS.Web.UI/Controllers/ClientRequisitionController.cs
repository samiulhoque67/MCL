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
    public class ClientRequisitionController : Controller
    {
        readonly IClientRequisitionService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ClientRequisitionController(IClientRequisitionService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /ClientRequisition/Index
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
            var ClientInfoList = new List<OBS_ClientAndAddressInfo>();
            await Task.Run(() => _clientInfoService.GetClientInfoList(out ClientInfoList));
            var result = Json(new { ClientInfoList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm)
        {
            clientReq.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveClientRequisition(clientReq, clientReqItem, clientReqTerm);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetClientReqSearchList()
        {
            var clientReqSearchList = new List<OBS_ClientReq>();
            await Task.Run(() => _clientInfoService.GetClientReqSearchList(out clientReqSearchList));
            var result = Json(new { clientReqSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientReqItemList(string ClientReqID)
        {
            var ClientReqItemList = new List<OBS_ClientReqItem>();
            await Task.Run(() => _clientInfoService.GetClientReqItemList(ClientReqID, out ClientReqItemList));
            var result = Json(new { ClientReqItemList, msg = "ClientReqItemList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientReqTermList(string ClientReqID)
        {
            var ClientReqTermList = new List<OBS_ClientReqTerms>();
            await Task.Run(() => _clientInfoService.GetClientReqTermList(ClientReqID, out ClientReqTermList));
            var result = Json(new { ClientReqTermList, msg = "ClientReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        //[HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID)
        {
            string status = string.Empty;
            status = _clientInfoService.DeleteClientReqItemAndTerm(ClientReqItemID, ClientReqTermID);
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

        public async Task<dynamic> GetClientReqTermAgainstFormList(string TermsID)
        {
            var ClientReqTermList = new List<OBS_ClientReqTerms>();
            await Task.Run(() => _clientInfoService.GetClientReqTermAgainstFormList(TermsID, out ClientReqTermList));
            var result = Json(new { ClientReqTermList, msg = "ClientReqTermList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }       
    }
}