using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientSettlement
{
    public interface IClientSettlementService
    {
        void GetClientSettlementList(out List<OBS_ClientSettlement> settlementList);
        void GetClientSettlementBillItem(string clientFinalBillID, out List<OBS_ClientBillCollectionItem> itemList);
        void GetClientSettlementHistory(string clientBillCollectionID, out List<OBS_ClientSettlementHistoryRow> historyList);
        void GetClientSettlementDocPath(string clientSettlementID, string docType, out string docName, out string docPath);
        string SaveClientSettlementInfo(OBS_ClientSettlement settlement);
    }
}
