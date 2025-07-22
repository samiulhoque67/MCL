using SILDMS.DataAccess.AdvanceClaimRecomClient;
using SILDMS.Model;
using SILDMS.Service.AdvanceClaimRecomClient;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.AdvanceClaimAprvClient;

namespace SILDMS.Service.AdvanceClaimAprvClient
{
    public class AdvanceClaimAprvClientService : IAdvanceClaimAprvClientService
    {
        private readonly IAdvanceClaimAprvClientData _advanceClaimAprvClientData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceClaimAprvClientService(IAdvanceClaimAprvClientData advanceClaimAprvClientData, ILocalizationService localizationService)
        {
            _advanceClaimAprvClientData = advanceClaimAprvClientData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IAdvanceClaimAprvClientService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceClaimAprvClientData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IAdvanceClaimAprvClientService.AllSavedAdvanceClaimService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceClaimAprvClientData.AllSavedAdvanceClaimDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }


        ValidationResult IAdvanceClaimAprvClientService.WoQtforAdvanClaimService(string ClientID, string WOInfoID, string AdvancClaimID, string AdvancClaimRecmID, out List<AdvanClaimWo> WODetails)
        {
            WODetails = _advanceClaimAprvClientData.WoQtforAdvanClaimDataService(ClientID, WOInfoID, AdvancClaimID, AdvancClaimRecmID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }
        
        ValidationResult IAdvanceClaimAprvClientService.AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimID, string AdvancClaimAprvID, out List<AdvanClaimWo> WODetails)
        {
            WODetails = _advanceClaimAprvClientData.AllSavedAdvanceClaimDetails(ClientID, WOInfoID, AdvancClaimID, AdvancClaimAprvID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData, string Operation)
        {
            return _advanceClaimAprvClientData.SaveQuotToClientServiceData(UserID, MasterData, Operation, out _errorNumber);

            throw new NotImplementedException();
        }


    }
}
