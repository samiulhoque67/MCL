using SILDMS.DataAccess.RcmdFinalBillRcvd;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.AprvFinalBillRcvd;

namespace SILDMS.Service.AprvFinalBillRcvd
{
    public class AprvFinalBillRcvdService : IAprvFinalBillRcvdService
    {
        private readonly IAprvFinalBillRcvdData _aprvFinalBillRcvdData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";



        public AprvFinalBillRcvdService(IAprvFinalBillRcvdData aprvFinalBillRcvdData, ILocalizationService localizationService)
        {
            _aprvFinalBillRcvdData = aprvFinalBillRcvdData;
            _localizationService = localizationService;
        }

        public ValidationResult GetPOSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _aprvFinalBillRcvdData.GetPOSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowVendorReqList(out List<VendorBillRecvd> vendorReqList)
        {
            vendorReqList = _aprvFinalBillRcvdData.GetShowVendorReqList();
            return ValidationResult.Success;
        }

        public string SaveVendorFinalBill(VendorBillRecvd billRecv)
        {
            return _aprvFinalBillRcvdData.SaveVendorFinalBill(billRecv);
        }
    }
}
