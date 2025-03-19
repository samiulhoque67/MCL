using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IFinalSettlementService
    {
        ValidationResult GetfinalSettlementSearchList(out List<OBS_FinalSettlement> poSearchList);
        ValidationResult GetShowfinalSettlementList(out List<OBS_FinalSettlement> vendorReqList);
        string SavefinalSettlement(OBS_FinalSettlement billRecv);
    }
}
