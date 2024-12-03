using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccessInterface;

namespace SILDMS.Service
{
    public class VendorFinalBilPaymentService : IVendorFinalBilPaymentService
    {
        private readonly IVendorFinalBilPaymentDataService _aprvFinalBillRcvdData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public VendorFinalBilPaymentService(IVendorFinalBilPaymentDataService aprvFinalBillRcvdData, ILocalizationService localizationService)
        {
            _aprvFinalBillRcvdData = aprvFinalBillRcvdData;
            _localizationService = localizationService;
        }

        public ValidationResult GetPOSearchList(out List<OBS_VendorFinalBilPayment> poSearchList)
        {
            poSearchList = _aprvFinalBillRcvdData.GetPOSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowVendorReqList(out List<OBS_VendorFinalBilPayment> vendorReqList)
        {
            vendorReqList = _aprvFinalBillRcvdData.GetShowVendorReqList();
            return ValidationResult.Success;
        }

        public string SaveVendorFinalBill(OBS_VendorFinalBilPayment billRecv)
        {
            return _aprvFinalBillRcvdData.SaveVendorFinalBill(billRecv);
        }
    }
}