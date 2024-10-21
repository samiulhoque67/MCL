using SILDMS.DataAccess.QuotationApproval;
using SILDMS.Model;
using SILDMS.Service.QuotationRecommendation;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.QuotationApproval
{
    public class QuotationApprovalService : IQuotationApprovalService
    {

        private readonly IQuotationApprovalDataService _quotationApprovalDataService;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;


        public QuotationApprovalService(IQuotationApprovalDataService quotationApprovalDataService, ILocalizationService localizationService)
        {
            _quotationApprovalDataService = quotationApprovalDataService;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }
        ValidationResult IQuotationApprovalService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _quotationApprovalDataService.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IQuotationApprovalService.GetClientReqDataInfoService(string ClientID, out List<ClientReqData> GetClientReqDetails)
        {
            GetClientReqDetails = _quotationApprovalDataService.GetClientReqDataInfoDataService(ClientID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl)
        {
            return _quotationApprovalDataService.SaveQuotToClientServiceData(UserID, MasterData, DetailData, AllTermsDtl, out _errorNumber);

            throw new NotImplementedException();
        }


    }
}
