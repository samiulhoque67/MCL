using SILDMS.DataAccess.VendorSettlement;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorSettlement
{
    public class VendorSettlementService : IVendorSettlementService
    {
        private readonly IVendorSettlementData vendorSettlementData;

        public VendorSettlementService(IVendorSettlementData _vendorSettlementData)
        {
            vendorSettlementData = _vendorSettlementData;
        }

        public void GetVendorSettlementList(out List<OBS_VendorSettlement> settlementList)
        {
            settlementList = vendorSettlementData.GetVendorSettlementList(out string errorNumber);
        }

        public void GetVendorSettlementBillItem(string vendorFinalBillID, out List<OBS_VendorBillPaymentItem> itemList)
        {
            itemList = vendorSettlementData.GetVendorSettlementBillItem(vendorFinalBillID, out string errorNumber);
        }

        public void GetVendorSettlementHistory(string vendorBillPaymentID, out List<OBS_VendorSettlementHistoryRow> historyList)
        {
            historyList = vendorSettlementData.GetVendorSettlementHistory(vendorBillPaymentID, out string errorNumber);
        }

        public void GetVendorSettlementDocPath(string vendorSettlementID, string docType, out string docName, out string docPath)
            => vendorSettlementData.GetVendorSettlementDocPath(vendorSettlementID, docType, out docName, out docPath);

        public string SaveVendorSettlementInfo(OBS_VendorSettlement settlement)
            => vendorSettlementData.SaveVendorSettlementInfo(settlement);
    }
}

