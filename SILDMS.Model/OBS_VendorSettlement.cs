using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorSettlement
    {
        public string VendorBillPaymentID { get; set; }
        public string VendorFinalBillID { get; set; }
        public string POAprvID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        public string VendorID { get; set; }

        // Context
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string ClientReqNo { get; set; }
        public string VendorName { get; set; }
        public string PoNo { get; set; }
        public string BillDate { get; set; }

        // Bill figures
        public string BillItemsAmountWithoutVat { get; set; }
        public string BillVatAmt { get; set; }
        public string VatPerc { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }

        // From the payment page
        public string MoneyReceiptNo { get; set; }
        public string PaymentDate { get; set; }
        public string TDSPerc { get; set; }
        public string TDSAmount { get; set; }
        public string VDSPaidBy { get; set; }
        public string PayableAmount { get; set; }

        // Running totals
        public string TDSAmountTotal { get; set; }
        public string TDSReceived { get; set; }
        public string TDSDue { get; set; }
        public string VDSAmountTotal { get; set; }
        public string MushakReceived { get; set; }
        public string VDSDue { get; set; }
        public string EntryCount { get; set; }
        public string SettlementStatus { get; set; }   // Pending | Partially Settled | Settled

        // Save payload
        public string TDSChallanReceiveAmt { get; set; }
        public string TDSChallanNo { get; set; }
        public string TDSChallanDate { get; set; }
        public string TDSDocName { get; set; }
        public string TDSDocPath { get; set; }
        public string MushakReceiveAmt { get; set; }
        public string MushakNo { get; set; }
        public string MushakDate { get; set; }
        public string MushakDocName { get; set; }
        public string MushakDocPath { get; set; }
        public string SetBy { get; set; }
    }

    public class OBS_VendorSettlementHistoryRow
    {
        public string VendorSettlementID { get; set; }
        public string VendorBillPaymentID { get; set; }
        public string TDSChallanReceiveAmt { get; set; }
        public string TDSDueAmt { get; set; }
        public string TDSChallanNo { get; set; }
        public string TDSChallanDate { get; set; }
        public string TDSDocName { get; set; }
        public string HasTDSDoc { get; set; }
        public string MushakReceiveAmt { get; set; }
        public string VDSDueAmt { get; set; }
        public string MushakNo { get; set; }
        public string MushakDate { get; set; }
        public string MushakDocName { get; set; }
        public string HasMushakDoc { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
    }
}
