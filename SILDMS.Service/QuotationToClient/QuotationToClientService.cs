using SILDMS.DataAccess.QuotationToClientService;
using SILDMS.Model;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.QuotationToClient
{
    public class QuotationToClientService : IQuotationToClientService
    {

        private readonly IQuotationToClientDataService _iQuotationToClientDataService;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public QuotationToClientService(IQuotationToClientDataService quotationToClientDataService, ILocalizationService localizationService)
        {
            _iQuotationToClientDataService = quotationToClientDataService;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IQuotationToClientService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientInfo> AllAvailableClientsList)
        {
            AllAvailableClientsList = _iQuotationToClientDataService.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        
        ValidationResult IQuotationToClientService.AvailableClientDetailInfoService(string ClientID, string ClientReqID, string ReqType, out List<OBS_ClientDetails> ClientDetails)
        {
            ClientDetails = _iQuotationToClientDataService.AvailableClientDetailInfoDataService(ClientID, ClientReqID, ReqType, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        public ValidationResult GetTermsConditionsListService(string VendorCSAprvID, string ClientReqID, string ReqType, out List<OBS_TermsItem> VendorTermTermList)
        {
            VendorTermTermList = _iQuotationToClientDataService.GetTermsConditionsListServiceData(VendorCSAprvID, ClientReqID, ReqType, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IQuotationToClientService.GetClientReqDataInfoService(string ClientID, string ClientReqID, string ReqType, out List<ClientReqData> GetClientReqDetails)
        {
            GetClientReqDetails = _iQuotationToClientDataService.GetClientReqDataInfoDataService(ClientID, ClientReqID, ReqType, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, string action, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl, string ReqType)
        {
            return _iQuotationToClientDataService.SaveQuotToClientServiceData(UserID, action, MasterData, DetailData, AllTermsDtl, ReqType, out _errorNumber);

            throw new NotImplementedException();
        }

        
    }
}
