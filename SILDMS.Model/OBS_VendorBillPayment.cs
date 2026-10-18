namespace SILDMS.Model
{
    // ── Vendor Final Bill Payment ────────────────────────────────────
    // One class serves the list row, the detail form and the save payload.
    public class OBS_VendorBillPayment
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

        // Bill figures (read-only; the save proc re-derives them itself)
        public string BillItemsAmountWithoutVat { get; set; }
        public string BillVatAmt { get; set; }
        public string VatPerc { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }
        public string NetPayableAmt { get; set; }

        // Payment fields
        public string TDSPaidBy { get; set; }
        public string TDSPerc { get; set; }
        public string TDSAmount { get; set; }
        public string VDSPaidBy { get; set; }          // "MediaCom" | "Vendor"
        public string VDSAmount { get; set; }
        public string PayableAmount { get; set; }

        // The 5 common payment fields
        public string MoneyReceiptNo { get; set; }
        public string PaymentMode { get; set; }        // Cheque | EFT | Cash
        public string PaymentRefNo { get; set; }
        public string PaymentDate { get; set; }
        public string Note { get; set; }

        public string HasSettlement { get; set; }      // "1" once any settlement entry exists
        public string SetBy { get; set; }
    }

    // Bill detail grid row — used by both the Payment and Settlement pages
    public class OBS_VendorBillPaymentItem
    {
        public string VendorFinalBillItemID { get; set; }
        public string ServiceItemName { get; set; }
        public string BillQty { get; set; }
        public string BillPrice { get; set; }
        public string BillAmt { get; set; }
        public string BillVatPerc { get; set; }
        public string BillVatAmt { get; set; }
        public string BillTotalAmt { get; set; }
    }

    // ── Vendor Settlement ────────────────────────────────────────────
    // List row + save payload (the controller fills the document name/path after upload).
    
}