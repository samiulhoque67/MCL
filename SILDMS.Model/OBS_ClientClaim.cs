using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    // ── Client Claim (advance against a WO) ──────────────────────────

    public class OBS_ClientClaim
    {
        public string ClientClaimID { get; set; }
        public string WOInfoID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }

        // Context (read-only, joined in)
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string WONo { get; set; }
        public string TotalWOAmount { get; set; }

        // Claim fields
        public string ClaimAmount { get; set; }
        public string RemainingWOAmount { get; set; }
        public string ClaimDate { get; set; }
        public string ClaimRemarks { get; set; }   // stored Remarks, used when listing
        public string ClaimedBy { get; set; }
        public string IsFullPayment { get; set; }

        // Input-only (save payload)
        public string Remarks { get; set; }
        public string SetBy { get; set; }
    }

    public class OBS_ClientClaimItem
    {
        public string WOInfoItemID { get; set; }
        public string WOInfoID { get; set; }
        public string Description { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryMode { get; set; }
        public string QutnUnit { get; set; }
        public string WOQnty { get; set; }
        public string WOPrice { get; set; }
        public string WOAmtWithoutASFVat { get; set; }
        public string ASFPerc { get; set; }
        public string TotGivenASFAmt { get; set; }
        public string VatPerc { get; set; }
        public string VatAmt { get; set; }
        public string WOAmtWithASFVat { get; set; }
        public string ServiceItemName { get; set; }
        public string ServicesCategoryName { get; set; }
    }

    public class OBS_ClientAdvanceClaimHistoryRow
    {
        public string ClientClaimID { get; set; }
        public string WOInfoID { get; set; }
        public string TotalWOAmount { get; set; }
        public string ClaimAmount { get; set; }
        public string RemainingWOAmount { get; set; }
        public string ClaimDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string IsFullPayment { get; set; }
        public string AdvanceAlreadyAdjusted { get; set; }
        public string AdvanceAvailable { get; set; }
    }

    // ── Client Final Bill (partial billing against a WO) ─────────────

    public class OBS_ClientFinalBill
    {
        public string ClientFinalBillID { get; set; }
        public string WOInfoID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }

        // Context
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string WONo { get; set; }
        public string TotalWOAmount { get; set; }

        // List-row status
        public string TotalRemainingQty { get; set; }
        public string BillCount { get; set; }
        public string TotalBilledAmt { get; set; }

        // Bill totals
        public string BillItemsAmountWithoutASFVat { get; set; }
        public string BillItemsASFAmt { get; set; }
        public string BillItemsVatAmt { get; set; }
        public string BillItemsAmount { get; set; }

        // Input fields
        public string BillDate { get; set; }
        public string Remarks { get; set; }
        public string SetBy { get; set; }
    }

    public class OBS_ClientFinalBillItem
    {
        public string WOInfoItemID { get; set; }
        public string WOInfoID { get; set; }
        public string Description { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryMode { get; set; }
        public string QutnUnit { get; set; }

        // Reference-only, from the WO item
        public string WOQty { get; set; }
        public string WOPrice { get; set; }
        public string WOASFPerc { get; set; }
        public string WOVatPerc { get; set; }
        public string AlreadyBilledQty { get; set; }
        public string RemainingQty { get; set; }

        // Editable — submitted on save
        public string BillQty { get; set; }
        public string BillPrice { get; set; }
        public string BillASFPerc { get; set; }
        public string BillVatPerc { get; set; }

        // Computed client-side for display; server recomputes on save
        public string BillAmt { get; set; }
        public string BillASFAmt { get; set; }
        public string BillVatAmt { get; set; }
        public string BillTotalAmt { get; set; }
        public string ServiceItemName { get; set; }
        public string ServicesCategoryName { get; set; }
    }

    public class OBS_ClientFinalBillHistoryRow
    {
        public string ClientFinalBillID { get; set; }
        public string WOInfoID { get; set; }
        public string BillItemsAmountWithoutASFVat { get; set; }
        public string BillItemsASFAmt { get; set; }
        public string BillItemsVatAmt { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }
        public string NetReceivableAmt { get; set; }
        public string BillDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
    }
}
