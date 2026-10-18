namespace SILDMS.Model
{
    // Header row — used both for the list grid (PO context only) and
    // the save payload (adds date, remarks).
    public class OBS_VendorFinalBill
    {
        // Keys
        public string VendorFinalBillID { get; set; }
        public string POAprvID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        public string VendorID { get; set; }

        // Context (read-only, joined in from PO / Client / Vendor)
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string VendorName { get; set; }
        public string PoNo { get; set; }
        public string PoAprvDate { get; set; }
        public string BillCategory { get; set; }
        public string TotalPOAmount { get; set; }

        // List-row status — from the list proc, drives whether "Add Bill" shows
        public string TotalRemainingQty { get; set; }
        public string BillCount { get; set; }
        public string TotalBilledAmt { get; set; }

        // Bill totals (computed server-side on save; echoed back for history rows)
        public string BillItemsAmountWithoutVat { get; set; }  // sum of this bill's item amounts, before VAT
        public string BillItemsAmount { get; set; }             // same, with VAT
        public string GrandTotalAmt { get; set; }                 // = BillItemsAmount

        // Input fields
        public string BillDate { get; set; }
        public string Remarks { get; set; }
        public string SetBy { get; set; }
        public string SetOn { get; set; }
    }

    // Item row for the editable billing grid
    public class OBS_VendorFinalBillItem
    {
        public string POAprvItemID { get; set; }
        public string PoAprvID { get; set; }
        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }
        public string ServiceCategoryID { get; set; }
        public string ServiceCategoryName { get; set; }
        public string Description { get; set; }
        public string DeliveryLocation { get; set; }

        // Reference-only, from the PO item
        public string POQty { get; set; }
        public string POPrice { get; set; }
        public string POVatPerc { get; set; }
        public string AlreadyBilledQty { get; set; }
        public string RemainingQty { get; set; }

        // Editable — submitted on save
        public string BillQty { get; set; }
        public string BillPrice { get; set; }
        public string BillVatPerc { get; set; }

        // Computed client-side for display only; server recomputes on save
        public string BillAmt { get; set; }
        public string BillVatAmt { get; set; }
        public string BillTotalAmt { get; set; }
    }

    // Read-only row for both the Advance Demand history and the
    // Final Bill history sections on the page.
    public class OBS_VendorDemandHistoryRow
    {
        public string VendorDemandID { get; set; }
        public string POAprvID { get; set; }
        public string TotalPOAmount { get; set; }
        public string DemandAmount { get; set; }
        public string RemainingPOAmount { get; set; }
        public string DemandDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string IsFullPayment { get; set; }
        public string AdvanceAlreadyAdjusted { get; set; }   // sum already netted off across prior bills
        public string AdvanceAvailable { get; set; }          // still unadjusted, available for the next bill
    }

    public class OBS_VendorFinalBillHistoryRow
    {
        public string VendorFinalBillID { get; set; }
        public string POAprvID { get; set; }
        public string BillItemsAmountWithoutVat { get; set; }
        public string BillItemsAmount { get; set; }
        public string GrandTotalAmt { get; set; }
        public string AdvanceAdjustedAmt { get; set; }
        public string NetPayableAmt { get; set; }
        public string BillDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
    }
}