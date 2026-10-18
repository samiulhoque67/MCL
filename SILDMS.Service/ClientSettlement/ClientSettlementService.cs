using SILDMS.DataAccess.ClientSettlement;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientSettlement
{
    public class ClientSettlementService : IClientSettlementService
    {
        private readonly IClientSettlementData clientSettlementData;

        public ClientSettlementService(IClientSettlementData _clientSettlementData)
        {
            clientSettlementData = _clientSettlementData;
        }

        public void GetClientSettlementList(out List<OBS_ClientSettlement> settlementList)
        {
            settlementList = clientSettlementData.GetClientSettlementList(out string errorNumber);
        }

        public void GetClientSettlementBillItem(string clientFinalBillID, out List<OBS_ClientBillCollectionItem> itemList)
        {
            itemList = clientSettlementData.GetClientSettlementBillItem(clientFinalBillID, out string errorNumber);
        }

        public void GetClientSettlementHistory(string clientBillCollectionID, out List<OBS_ClientSettlementHistoryRow> historyList)
        {
            historyList = clientSettlementData.GetClientSettlementHistory(clientBillCollectionID, out string errorNumber);
        }

        public void GetClientSettlementDocPath(string clientSettlementID, string docType, out string docName, out string docPath)
            => clientSettlementData.GetClientSettlementDocPath(clientSettlementID, docType, out docName, out docPath);

        public string SaveClientSettlementInfo(OBS_ClientSettlement settlement)
            => clientSettlementData.SaveClientSettlementInfo(settlement);
    
}
}
