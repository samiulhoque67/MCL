using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IFinalSettlementDataService
    {
        List<OBS_FinalSettlement> GetfinalSettlementSearchList();
        List<OBS_FinalSettlement> GetShowfinalSettlementList();
        string SavefinalSettlement(OBS_FinalSettlement billRecv);
    }
}
