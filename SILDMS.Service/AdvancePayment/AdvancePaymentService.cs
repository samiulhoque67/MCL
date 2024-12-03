using SILDMS.DataAccess.AdvanceRecommendation;
using SILDMS.Model;
using SILDMS.Service.AdvanceRecommendation;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.AdvancePayment;

namespace SILDMS.Service.AdvancePayment
{
    public class AdvancePaymentService : IAdvancePaymentService
    {
        private readonly IAdvancePaymentDataService _advancePaymentDataService;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvancePaymentService(IAdvancePaymentDataService advancePaymentDataService, ILocalizationService localizationService)
        {
            _advancePaymentDataService = advancePaymentDataService;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }

        ValidationResult IAdvancePaymentService.AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList)
        {
            AllAvailableClientsList = _advancePaymentDataService.AllAvailableCSVendorApprovalDataService(UserId, page, itemsPerPage, sortBy, reverse, search, type, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }


        ValidationResult IAdvancePaymentService.AvailableClientDetailInfoService(string ClientID, string VendrAdvncDemnID, out List<POinfo> ClientDetails)
        {
            ClientDetails = _advancePaymentDataService.AvailableClientDetailInfoDataService(ClientID, VendrAdvncDemnID, out _errorNumber);

            return _errorNumber.Length > 0
                ? new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber))
                : ValidationResult.Success;
        }

        public string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo)
        {
            return _advancePaymentDataService.SaveQuotToClientServiceData(UserID, MasterData, TransactionMode, ParticularNo, MoneyReceiptNo, out _errorNumber);

            throw new NotImplementedException();
        }
    }
}
