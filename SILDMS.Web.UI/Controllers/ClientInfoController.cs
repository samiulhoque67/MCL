using SILDMS.Model;
using SILDMS.Model.DocScanningModule;
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
    public class ClientInfoController : Controller
    {
        readonly IClientInfoService _clientInfoService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public ClientInfoController(IClientInfoService repository, ILocalizationService localizationService)
        {
            this._clientInfoService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        // GET: /ClientInfo/Index
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
                ClientCategoryID = x.ServicesCategoryID,
                ClientCategoryName = x.ServicesCategoryName
            }).OrderByDescending(ob => ob.ClientCategoryID);

            return Json(new { Message = "", result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> SaveClientInfoMst(OBS_ClientInfo _modelClientInfoMst, string ClientAddressID)
        {
            _modelClientInfoMst.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveClientInfoMst(_modelClientInfoMst, ClientAddressID);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> SaveClientAddress(OBS_ClientAddressInfo _modelClientAddress)
        {
            _modelClientAddress.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            string ClientAddressID = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveClientAddress(_modelClientAddress);

            if (status != string.Empty)
            {
                string[] statusarr = status.Split(',');
                ClientAddressID = statusarr[1];
                status = statusarr[0];
            }
            return Json(new { status, ClientAddressID }, JsonRequestBehavior.AllowGet);

            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [SILLogAttribute]
        public async Task<dynamic> DeleteClientAddress(OBS_ClientAddressInfo _modelClientAddress)
        {
            _modelClientAddress.Action = "delete";
            string status = string.Empty;//, message = string.Empty;
            status = _clientInfoService.SaveClientAddress(_modelClientAddress);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
            //return Json(new { ResponseCode = status, message }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetClientInfoSearchList()
        {
            var ClientInfoSearchList = new List<OBS_ClientInfo>();
            await Task.Run(() => _clientInfoService.GetClientInfoSearchList(out ClientInfoSearchList));
            var result = Json(new { ClientInfoSearchList, msg = "ClientInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> GetClientAddressList(string ClientID)
        {
            var ClientAddressList = new List<OBS_ClientAddressInfo>();
            await Task.Run(() => _clientInfoService.GetClientAddressList(ClientID, out ClientAddressList));
            var result = Json(new { ClientAddressList, msg = "ClientInfoSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}