using SILDMS.DataAccess.ClientFinalBillPrepare;
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
    public class ClientSettlementService: IClientSettlementService
    {
        private readonly IClientSettlementDataService _clientFinalBillPrepareData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public ClientSettlementService(IClientSettlementDataService clientFinalBillPrepareData, ILocalizationService localizationService)
        {
            _clientFinalBillPrepareData = clientFinalBillPrepareData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _clientFinalBillPrepareData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<OBS_ClientSettlement> vendorReqList)
        {
            vendorReqList = _clientFinalBillPrepareData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveClientSettlement(OBS_ClientSettlement billRecv)
        {
            return _clientFinalBillPrepareData.SaveClientSettlement(billRecv);
        }
    }
}
