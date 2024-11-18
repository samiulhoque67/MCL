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
        
        ValidationResult IQuotationToClientService.AvailableClientDetailInfoService(string ClientID, out List<OBS_ClientDetails> ClientDetails)
        {
            ClientDetails = _iQuotationToClientDataService.AvailableClientDetailInfoDataService(ClientID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        public ValidationResult GetTermsConditionsListService(string VendorCSAprvID, out List<OBS_TermsItem> VendorTermTermList)
        {
            VendorTermTermList = _iQuotationToClientDataService.GetTermsConditionsListServiceData(VendorCSAprvID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IQuotationToClientService.GetClientReqDataInfoService(string ClientID, string ClientReqID, out List<ClientReqData> GetClientReqDetails)
        {
            GetClientReqDetails = _iQuotationToClientDataService.GetClientReqDataInfoDataService(ClientID, ClientReqID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            return _iQuotationToClientDataService.SaveQuotToClientServiceData(UserID, MasterData, DetailData, AllTermsDtl, out _errorNumber);

            throw new NotImplementedException();
        }

        
    }
}
