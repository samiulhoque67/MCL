using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.CombinedSettlement
{
    public interface ICombinedSettlementData
    {
        List<OBS_CombinedSettlement> GetCombinedSettlementList(out string errorNumber);
        List<OBS_CombinedClientBill> GetCombinedClientBills(string woInfoID, out string errorNumber);
        List<OBS_ClientSettlementHistoryRow> GetCombinedClientEntries(string woInfoID, out string errorNumber);
        List<OBS_CombinedVendorBill> GetCombinedVendorBills(string clientReqID, out string errorNumber);
        List<OBS_VendorSettlementHistoryRow> GetCombinedVendorEntries(string clientReqID, out string errorNumber);
    }
}
