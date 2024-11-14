using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Service.QuotationToClient;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class QuotationToClientController : Controller
    {
        readonly IQuotationToClientService _quotationToClientService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public QuotationToClientController(IQuotationToClientService repository, ILocalizationService localizationService)
        {
            this._quotationToClientService = repository;
            this._localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }

        // GET: QuotationToClient
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<dynamic> AllAvailableClients(int page, int itemsPerPage, string sortBy, bool reverse, string search, string type)
        {
            var AllAvailableClientsList = new List<OBS_ClientInfo>();
            await Task.Run(() => _quotationToClientService.AllAvailableCSVendorApprovalService(UserID, page, itemsPerPage, sortBy, reverse, search, type, out AllAvailableClientsList));
            var result = Json(new { AllAvailableClientsList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AvailableClientDetailInfo(string ClientID)
        {
            var ClientDetails = new List<OBS_ClientDetails>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.AvailableClientDetailInfoService(ClientID, out ClientDetails));
            var result = Json(new { ClientDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<dynamic> GetVendorTermList(string VendorCSAprvID)
        {
            var VendorTermTermList = new List<OBS_TermsItem>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.GetTermsConditionsListService(VendorCSAprvID, out VendorTermTermList));
            var result = Json(new { VendorTermTermList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<dynamic> GetClientReqDataInfo(string ClientID, string ClientReqID)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.GetClientReqDataInfoService(ClientID, ClientReqID, out GetClientReqDetails));
            var result = Json(new { GetClientReqDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            if (MasterData == null || !MasterData.Any() || DetailData == null || !DetailData.Any())
            {
                return Json(new { status = "Error", message = "MasterList is empty or null." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string status = _quotationToClientService.SaveQuotToClientService(UserID, MasterData, DetailData, AllTermsDtl);
                return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //edit and view
    }
}