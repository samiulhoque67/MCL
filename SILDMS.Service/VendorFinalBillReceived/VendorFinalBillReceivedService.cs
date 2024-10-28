using SILDMS.DataAccess;
using SILDMS.DataAccess.VendorFinalBillReceived;
using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorFinalBillReceived
{
    public class VendorFinalBillReceivedService : IVendorFinalBillReceivedService
    {
        private readonly IVendorFinalBillReceivedData _vendorFinalBillReceivedData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";



        public VendorFinalBillReceivedService(IVendorFinalBillReceivedData vendorFinalBillReceivedData, ILocalizationService localizationService)
        {
            _vendorFinalBillReceivedData = vendorFinalBillReceivedData;
            _localizationService = localizationService;
        }

        public ValidationResult GetPOSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _vendorFinalBillReceivedData.GetPOSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowVendorReqList(out List<OBS_VendorQutn> vendorReqList)
        {
            vendorReqList = _vendorFinalBillReceivedData.GetShowVendorReqList();
            return ValidationResult.Success;
        }

        public string SaveVendorFinalBill(VendorBillRecvd billRecv)
        {
            return _vendorFinalBillReceivedData.SaveVendorFinalBill(billRecv);
        }
    }
}
