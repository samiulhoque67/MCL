using SILDMS.DataAccess.AdvanceApproval;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Model;
using SILDMS.Service.AdvanceRecommendation;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceApproval
{
    public class AdvanceApprovalService : IAdvanceApprovalService
    {
        private readonly IAdvanceApprovalData _advanceApprovalData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceApprovalService(IAdvanceApprovalData advanceApprovalData, ILocalizationService localizationService)
        {
            _advanceApprovalData = advanceApprovalData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }


        ValidationResult IAdvanceApprovalService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceApprovalData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }


        ValidationResult IAdvanceApprovalService.AvailableClientDetailInfoService(string ClientID, string VendrAdvncDemnID, out List<POinfo> ClientDetails)
        {
            ClientDetails = _advanceApprovalData.AvailableClientDetailInfoDataService(ClientID, VendrAdvncDemnID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData)
        {
            return _advanceApprovalData.SaveQuotToClientServiceData(UserID, MasterData, out _errorNumber);

            throw new NotImplementedException();
        }


    }
    }
