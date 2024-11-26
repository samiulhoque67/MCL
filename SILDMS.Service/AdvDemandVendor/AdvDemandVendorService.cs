using SILDMS.DataAccess.AdvDemandVendor;
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

namespace SILDMS.Service.AdvDemandVendor
{
    public class AdvDemandVendorService : IAdvDemandVendorService
    {
        private readonly IAdvDemandVendorData _advDemandVendorData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvDemandVendorService(IAdvDemandVendorData advDemandVendorData, ILocalizationService localizationService)
        {
            _advDemandVendorData = advDemandVendorData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }



        ValidationResult IAdvDemandVendorService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advDemandVendorData.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        ValidationResult IAdvDemandVendorService.AvailableClientDetailInfoService(string POAprvID, out List<POinfo> ClientDetails)
        {
            ClientDetails = _advDemandVendorData.AvailableClientDetailInfoDataService(POAprvID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData)
        {
            return _advDemandVendorData.SaveQuotToClientServiceData(UserID, MasterData, out _errorNumber);

            throw new NotImplementedException();
        }

    }
}
