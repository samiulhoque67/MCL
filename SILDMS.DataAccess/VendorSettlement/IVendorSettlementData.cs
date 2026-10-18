using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorSettlement
{
    public interface IVendorSettlementData
    {
        List<OBS_VendorSettlement> GetVendorSettlementList(out string errorNumber);
        List<OBS_VendorBillPaymentItem> GetVendorSettlementBillItem(string vendorFinalBillID, out string errorNumber);
        List<OBS_VendorSettlementHistoryRow> GetVendorSettlementHistory(string vendorBillPaymentID, out string errorNumber);
        void GetVendorSettlementDocPath(string vendorSettlementID, string docType, out string docName, out string docPath);
        string SaveVendorSettlementInfo(OBS_VendorSettlement settlement);
    }
}
