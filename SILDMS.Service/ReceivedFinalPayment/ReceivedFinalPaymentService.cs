using SILDMS.DataAccess.ClientAprvBill;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.ReceivedFinalPayment;

namespace SILDMS.Service.ReceivedFinalPayment
{
    public class ReceivedFinalPaymentService : IReceivedFinalPaymentService
    {
        private readonly IReceivedFinalPaymentData _receivedFinalPaymentData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public ReceivedFinalPaymentService(IReceivedFinalPaymentData receivedFinalPaymentData, ILocalizationService localizationService)
        {
            _receivedFinalPaymentData = receivedFinalPaymentData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _receivedFinalPaymentData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<VendorBillRecvd> vendorReqList)
        {
            vendorReqList = _receivedFinalPaymentData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveClientFinalBill(VendorBillRecvd billRecv)
        {
            return _receivedFinalPaymentData.SaveClientFinalBill(billRecv);
        }
        public string SaveVendorFinalBillRcvd(VendorBillRecvd billRecv)
        {
            return _receivedFinalPaymentData.SaveVendorFinalBillRcvd(billRecv);
        }
    }
}
