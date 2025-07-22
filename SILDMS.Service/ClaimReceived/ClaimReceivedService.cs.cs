using SILDMS.DataAccess.AdvanceClaimAprvClient;
using SILDMS.Model;
using SILDMS.Service.AdvanceClaimAprvClient;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.Claim_Received;

namespace SILDMS.Service.ClaimReceived
{
    public class ClaimReceivedService : IClaimReceivedService
    {
        private readonly IClaimReceivedDataService _claimReceivedDataService;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public ClaimReceivedService(IClaimReceivedDataService claimReceivedDataService, ILocalizationService localizationService)
        {
            _claimReceivedDataService = claimReceivedDataService;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IClaimReceivedService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _claimReceivedDataService.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IClaimReceivedService.AllSavedAdvanceClaimService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _claimReceivedDataService.AllSavedAdvanceClaimDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }


        ValidationResult IClaimReceivedService.WoQtforAdvanClaimService(string ClientID, string WOInfoID, string AdvancClaimAprvID, out List<AdvanClaimWo> WODetails)
        {
            WODetails = _claimReceivedDataService.WoQtforAdvanClaimDataService(ClientID, WOInfoID, AdvancClaimAprvID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        
        ValidationResult IClaimReceivedService.AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimAprvID, string AdvancClaimRcvdID, out List<AdvanClaimWo> WODetails)
        {
            WODetails = _claimReceivedDataService.AllSavedAdvanceClaimDetails(ClientID, WOInfoID, AdvancClaimAprvID, AdvancClaimRcvdID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo)
        {
            return _claimReceivedDataService.SaveQuotToClientServiceData(UserID, MasterData, TransactionMode, ParticularNo, MoneyReceiptNo, out _errorNumber);

            throw new NotImplementedException();
        }


    }
}
