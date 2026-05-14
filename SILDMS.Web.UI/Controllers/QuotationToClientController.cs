using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Service.QuotationToClient;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

        [SILAuthorize]
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
        public async Task<dynamic> AvailableClientDetailInfo(string ClientID, string ClientReqID, string ReqType = null)
        {
            var ClientDetails = new List<OBS_ClientDetails>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.AvailableClientDetailInfoService(ClientID, ClientReqID, ReqType, out ClientDetails));
            var result = Json(new { ClientDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<dynamic> AvailableClientAprvInfo(string ClientID, string ClientReqID, string ReqType = null)
        {
            var ClientDetails = new List<OBS_ClientDetails>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.AvailableClientAprvInfo(ClientID, ClientReqID, ReqType, out ClientDetails));
            var result = Json(new { ClientDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<dynamic> GetClientReqDataInfo(string ClientID, string ClientReqID, string ReqType)
        {
            var GetClientReqDetails = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.GetClientReqDataInfoService(ClientID, ClientReqID, ReqType, out GetClientReqDetails));
            var result = Json(new { GetClientReqDetails, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }
        [HttpPost]
        public async Task<dynamic> GetClientReqDataItemPopup(/*string ClientID, string ClientReqID,*/ string VendorCSAprvID,string ServiceItemID)
        {
            var GetClientReqDataItemPopup = new List<ClientReqData>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.GetClientReqDataItemPopupService(VendorCSAprvID, ServiceItemID, out GetClientReqDataItemPopup));
            var result = Json(new { GetClientReqDataItemPopup, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }


        [HttpPost]
        public async Task<dynamic> GetVendorTermList(string VendorCSAprvID, string ClientReqID, string ReqType)
        {
            var VendorTermTermList = new List<OBS_TermsItem>();  // Renamed to ClientDetails
            await Task.Run(() => _quotationToClientService.GetTermsConditionsListService(VendorCSAprvID, ClientReqID, ReqType, out VendorTermTermList));
            var result = Json(new { VendorTermTermList, msg = "loaded in the table." }, JsonRequestBehavior.AllowGet);  // Renamed here too
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> SaveQuotToClient(
     string action,
     List<OBS_QutntoClientMaster> MasterData,
     List<ClientReqData> DetailData,
     List<OBS_TermsItem> AllTermsDtl,
     string ReqType = null)
        {
            if (MasterData == null || !MasterData.Any() || DetailData == null || !DetailData.Any())
                return Json(new { status = "Error", message = "MasterData or DetailData is empty." },
                            JsonRequestBehavior.AllowGet);

            try
            {
                string rawStatus = _quotationToClientService
                                       .SaveQuotToClientService(UserID, action, MasterData,
                                                                DetailData, AllTermsDtl, ReqType);

                // Always split on the FIRST comma only – the ID or error message
                // may itself contain commas.
                int firstComma = rawStatus.IndexOf(',');

                string status = firstComma >= 0 ? rawStatus.Substring(0, firstComma).Trim()
                                                     : rawStatus.Trim();
                string clientQutnID = firstComma >= 0 ? rawStatus.Substring(firstComma + 1).Trim()
                                                      : string.Empty;

                return Json(new { status, ClientQutnID = clientQutnID },
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message },
                            JsonRequestBehavior.AllowGet);
            }
        }



        //edit and view
    }
}