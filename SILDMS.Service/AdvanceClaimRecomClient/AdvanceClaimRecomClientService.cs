using SILDMS.DataAccess.AdvanceClaim;
using SILDMS.Model;
using SILDMS.Service.AdvanceClaim;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.AdvanceClaimRecomClient;

namespace SILDMS.Service.AdvanceClaimRecomClient
{
    public class AdvanceClaimRecomClientService : IAdvanceClaimRecomClientService
    {
        private readonly IAdvanceClaimRecomClientData _advanceClaimRecomClientData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceClaimRecomClientService(IAdvanceClaimRecomClientData advanceClaimRecomClientData, ILocalizationService localizationService)
        {
            _advanceClaimRecomClientData = advanceClaimRecomClientData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IAdvanceClaimRecomClientService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceClaimRecomClientData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IAdvanceClaimRecomClientService.WoQtforAdvanClaimService(string ClientID, string WOInfoID, string AdvancClaimID, out List<AdvanClaimWo> WODetails)
        {
            WODetails = _advanceClaimRecomClientData.WoQtforAdvanClaimDataService(ClientID, WOInfoID, AdvancClaimID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData, string Operation)
        {
            return _advanceClaimRecomClientData.SaveQuotToClientServiceData(UserID, MasterData, Operation, out _errorNumber);

            throw new NotImplementedException();
        }

    }
}
