using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorSettlement
{
    public interface IVendorSettlementService
    {
        void GetVendorSettlementList(out List<OBS_VendorSettlement> settlementList);
        void GetVendorSettlementBillItem(string vendorFinalBillID, out List<OBS_VendorBillPaymentItem> itemList);
        void GetVendorSettlementHistory(string vendorBillPaymentID, out List<OBS_VendorSettlementHistoryRow> historyList);
        void GetVendorSettlementDocPath(string vendorSettlementID, string docType, out string docName, out string docPath);
        string SaveVendorSettlementInfo(OBS_VendorSettlement settlement);
    }
}
