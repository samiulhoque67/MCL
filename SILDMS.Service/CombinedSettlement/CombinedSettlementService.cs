using SILDMS.DataAccess.CombinedSettlement;
using SILDMS.Model;
using System.Collections.Generic;

namespace SILDMS.Service.CombinedSettlement
{
    
    public class CombinedSettlementService : ICombinedSettlementService
    {
        private readonly ICombinedSettlementData combinedSettlementData;

        public CombinedSettlementService(ICombinedSettlementData _combinedSettlementData)
        {
            combinedSettlementData = _combinedSettlementData;
        }

        public void GetCombinedSettlementList(out List<OBS_CombinedSettlement> list)
        {
            list = combinedSettlementData.GetCombinedSettlementList(out string errorNumber);
        }

        public void GetCombinedClientBills(string woInfoID, out List<OBS_CombinedClientBill> list)
        {
            list = combinedSettlementData.GetCombinedClientBills(woInfoID, out string errorNumber);
        }

        public void GetCombinedClientEntries(string woInfoID, out List<OBS_ClientSettlementHistoryRow> list)
        {
            list = combinedSettlementData.GetCombinedClientEntries(woInfoID, out string errorNumber);
        }

        public void GetCombinedVendorBills(string clientReqID, out List<OBS_CombinedVendorBill> list)
        {
            list = combinedSettlementData.GetCombinedVendorBills(clientReqID, out string errorNumber);
        }

        public void GetCombinedVendorEntries(string clientReqID, out List<OBS_VendorSettlementHistoryRow> list)
        {
            list = combinedSettlementData.GetCombinedVendorEntries(clientReqID, out string errorNumber);
        }
    }
}