using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_CombinedSettlement
    {
        public string WOInfoID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string ClientReqNo { get; set; }
        public string WONo { get; set; }

        // Client side
        public string WOAmount { get; set; }
        public string CollectionAmount { get; set; }
        public string ClientBillCount { get; set; }
        public string ClientCollectedCount { get; set; }
        public string ClientSettlementStatus { get; set; }

        // Vendor side
        public string PoCount { get; set; }
        public string PoAmount { get; set; }
        public string PayableAmount { get; set; }
        public string VendorBillCount { get; set; }
        public string VendorPaidCount { get; set; }
        public string VendorSettlementStatus { get; set; }

        // BothSettled | ClientSettled | VendorSettled | BothPending | ClientPendingNoVendor
        public string OverallCode { get; set; }
        public string OverallStatus { get; set; }
    }

    // One Client Final Bill with its collection and settlement totals
    public class OBS_CombinedClientBill
    {
        public string ClientFinalBillID { get; set; }
        public string BillDate { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }

        public string ClientBillCollectionID { get; set; }   // "" when not collected yet
        public string AITAmount { get; set; }
        public string VATPaidBy { get; set; }
        public string VATAmount { get; set; }
        public string CollectionAmount { get; set; }
        public string MoneyReceiptNo { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentRefNo { get; set; }
        public string ReceiveDate { get; set; }

        public string AITReceived { get; set; }
        public string AITDue { get; set; }
        public string MushakReceived { get; set; }
        public string MushakDue { get; set; }
        public string EntryCount { get; set; }
        public string SettlementStatus { get; set; }
    }

    // One Vendor Final Bill with its payment and settlement totals
    public class OBS_CombinedVendorBill
    {
        public string VendorFinalBillID { get; set; }
        public string POAprvID { get; set; }
        public string PoNo { get; set; }
        public string VendorName { get; set; }
        public string POAmount { get; set; }
        public string BillDate { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }

        public string VendorBillPaymentID { get; set; }      // "" when not paid yet
        public string TDSPerc { get; set; }
        public string TDSAmount { get; set; }
        public string VDSPaidBy { get; set; }
        public string VDSAmount { get; set; }
        public string PayableAmount { get; set; }
        public string MoneyReceiptNo { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentRefNo { get; set; }
        public string PaymentDate { get; set; }

        public string TDSReceived { get; set; }
        public string TDSDue { get; set; }
        public string MushakReceived { get; set; }
        public string VDSDue { get; set; }
        public string EntryCount { get; set; }
        public string SettlementStatus { get; set; }
    }
}
