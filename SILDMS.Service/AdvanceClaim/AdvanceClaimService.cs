using SILDMS.DataAccess.AdvanceClaim;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Model;
using SILDMS.Service.AdvDemandVendor;
using SILDMS.Service.QuotationApproval;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceClaim
{
    public class AdvanceClaimService : IAdvanceClaimService
    {

        private readonly IAdvanceClaimData _advanceClaimData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceClaimService(IAdvanceClaimData advanceClaimData, ILocalizationService localizationService)
        {
            _advanceClaimData = advanceClaimData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IAdvanceClaimService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceClaimData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        ValidationResult IAdvanceClaimService.AllSavedAdvanceClaimService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceClaimData.AllSavedAdvanceClaimDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IAdvanceClaimService.WoQtforAdvanClaimService(string ClientID, string WOInfoID, string WONo, out List<AdvanClaimWo> WODetails)
         {
            WODetails = _advanceClaimData.WoQtforAdvanClaimDataService(ClientID, WOInfoID, WONo, out _errorNumber);

             return _errorNumber.Length > 0
                 ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                 : ValidationResult.Success;
         }
        ValidationResult IAdvanceClaimService.AllSavedAdvanceClaimDetailsService(string ClientID, string WOInfoID, string WONo, string AdvancClaimID, out List<AdvanClaimWo> WODetails)
         {
            WODetails = _advanceClaimData.AllSavedAdvanceClaimDetailsDataService(ClientID, WOInfoID, WONo, AdvancClaimID, out _errorNumber);

             return _errorNumber.Length > 0
                 ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                 : ValidationResult.Success;
         }

        public string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData)
        {
            return _advanceClaimData.SaveQuotToClientServiceData(UserID, MasterData, out _errorNumber);

            throw new NotImplementedException();
        }

    }
}
