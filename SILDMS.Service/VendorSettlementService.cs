using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public class VendorSettlementService: IVendorSettlementService
    {
        private readonly IVendorSettlementDataService _clientFinalBillPrepareData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public VendorSettlementService(IVendorSettlementDataService clientFinalBillPrepareData, ILocalizationService localizationService)
        {
            _clientFinalBillPrepareData = clientFinalBillPrepareData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _clientFinalBillPrepareData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<OBS_VendorSettlement> vendorReqList)
        {
            vendorReqList = _clientFinalBillPrepareData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveVendorSettlement(OBS_VendorSettlement billRecv)
        {
            return _clientFinalBillPrepareData.SaveVendorSettlement(billRecv);
        }
    }
}
