using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.CombinedSettlement
{
    public interface ICombinedSettlementService
    {

        void GetCombinedSettlementList(out List<OBS_CombinedSettlement> list);
        void GetCombinedClientBills(string woInfoID, out List<OBS_CombinedClientBill> list);
        void GetCombinedClientEntries(string woInfoID, out List<OBS_ClientSettlementHistoryRow> list);
        void GetCombinedVendorBills(string clientReqID, out List<OBS_CombinedVendorBill> list);
        void GetCombinedVendorEntries(string clientReqID, out List<OBS_VendorSettlementHistoryRow> list);
    }
}
