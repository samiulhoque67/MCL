using SILDMS.DataAccess.VendorFinalBillReceived;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.RcmdFinalBillRcvd;

namespace SILDMS.Service.RcmdFinalBillRcvd
{
    public class RcmdFinalBillRcvdService : IRcmdFinalBillRcvdService
    {
        private readonly IRcmdFinalBillRcvdData _rcmdFinalBillRcvdData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";



        public RcmdFinalBillRcvdService(IRcmdFinalBillRcvdData rcmdFinalBillRcvdData, ILocalizationService localizationService)
        {
            _rcmdFinalBillRcvdData = rcmdFinalBillRcvdData;
            _localizationService = localizationService;
        }

        public ValidationResult GetPOSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _rcmdFinalBillRcvdData.GetPOSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowVendorReqList(out List<VendorBillRecvd> vendorReqList)
        {
            vendorReqList = _rcmdFinalBillRcvdData.GetShowVendorReqList();
            return ValidationResult.Success;
        }

        public string SaveVendorFinalBill(VendorBillRecvd billRecv)
        {
            return _rcmdFinalBillRcvdData.SaveVendorFinalBill(billRecv);
        }
    }
}
