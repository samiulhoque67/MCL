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
    public class FinalSettlementService: IFinalSettlementService
    {
        private readonly IFinalSettlementDataService _FinalSettlementData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public FinalSettlementService(IFinalSettlementDataService FinalSettlementData, ILocalizationService localizationService)
        {
            _FinalSettlementData = FinalSettlementData;
            _localizationService = localizationService;
        }

        public ValidationResult GetfinalSettlementSearchList(out List<OBS_FinalSettlement> poSearchList)
        {
            poSearchList = _FinalSettlementData.GetfinalSettlementSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowfinalSettlementList(out List<OBS_FinalSettlement> vendorReqList)
        {
            vendorReqList = _FinalSettlementData.GetShowfinalSettlementList();
            return ValidationResult.Success;
        }

        public string SavefinalSettlement(OBS_FinalSettlement billRecv)
        {
            return _FinalSettlementData.SavefinalSettlement(billRecv);
        }
    }
}
