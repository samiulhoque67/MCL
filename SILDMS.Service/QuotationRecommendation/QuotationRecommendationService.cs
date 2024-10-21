using SILDMS.DataAccess.QuotationRecommendation;
using SILDMS.DataAccess.QuotationToClientService;
using SILDMS.Model;
using SILDMS.Service.QuotationToClient;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.QuotationRecommendation
{
    public class QuotationRecommendationService : IQuotationRecommendationService
    {
        private readonly IQuotationRecommendationData _quotationRecommendationData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;


        public QuotationRecommendationService(IQuotationRecommendationData quotationRecommendationData, ILocalizationService localizationService)
        {
            _quotationRecommendationData = quotationRecommendationData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IQuotationRecommendationService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _quotationRecommendationData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IQuotationRecommendationService.GetClientReqDataInfoService(string ClientID, out List<ClientReqData> GetClientReqDetails)
        {
            GetClientReqDetails = _quotationRecommendationData.GetClientReqDataInfoDataService(ClientID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            return _quotationRecommendationData.SaveQuotToClientServiceData(UserID, MasterData, DetailData, AllTermsDtl, out _errorNumber);

            throw new NotImplementedException();
        }

    }
}

