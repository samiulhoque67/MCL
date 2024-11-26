using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IClientSettlementDataService
    {
        List<VendorBillRecvd> GetQutnSearchList();
        List<OBS_ClientSettlement> GetShowClientReqList();
        string SaveClientSettlement(OBS_ClientSettlement billRecv);
    }
}

