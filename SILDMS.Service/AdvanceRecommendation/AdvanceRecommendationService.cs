using SILDMS.DataAccess.AdvanceApproval;
using SILDMS.DataAccess.AdvanceRecommendation;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Model;
using SILDMS.Service.AdvDemandVendor;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceRecommendation
{
    public class AdvanceRecommendationService : IAdvanceRecommendationService
    {
        private readonly IAdvanceRecommendationData _advanceRecommendationData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceRecommendationService(IAdvanceRecommendationData advanceRecommendationData, ILocalizationService localizationService)
        {
            _advanceRecommendationData = advanceRecommendationData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IAdvanceRecommendationService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advanceRecommendationData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }


        ValidationResult IAdvanceRecommendationService.AvailableClientDetailInfoService(string ClientID, string VendrAdvncDemnID, out List<POinfo> ClientDetails)
        {
            ClientDetails = _advanceRecommendationData.AvailableClientDetailInfoDataService(ClientID, VendrAdvncDemnID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData)
        {
            return _advanceRecommendationData.SaveQuotToClientServiceData(UserID, MasterData, out _errorNumber);

            throw new NotImplementedException();
        }
    }
}
