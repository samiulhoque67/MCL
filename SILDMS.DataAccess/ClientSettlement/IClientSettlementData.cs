using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientSettlement
{
    public interface IClientSettlementData
    {
        List<OBS_ClientSettlement> GetClientSettlementList(out string errorNumber);
        List<OBS_ClientBillCollectionItem> GetClientSettlementBillItem(string clientFinalBillID, out string errorNumber);
        List<OBS_ClientSettlementHistoryRow> GetClientSettlementHistory(string clientBillCollectionID, out string errorNumber);
        void GetClientSettlementDocPath(string clientSettlementID, string docType, out string docName, out string docPath);
        string SaveClientSettlementInfo(OBS_ClientSettlement settlement);
    }
}
