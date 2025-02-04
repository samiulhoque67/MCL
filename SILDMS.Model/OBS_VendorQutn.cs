using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorQutn
    {
        public string VendorQutnID { get; set; }
        public string ClientID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string ClientName { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        [Required]
        public string AutoVendorQutnNo { get; set; }
        [Required]
        public string VendorQutnNo { get; set; }
        [Required]

        public string QuotationDate { get; set; }
        public string QutnReceivedDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
        ////////////////Show Field////////////////////
        public string VendorReqID { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionDate { get; set; }
        public string SubmissionDate { get; set; }
        public string LastDateofQuotation { get; set; } 

        public string PoNo { get; set; }
        public string PoAprvDate { get; set; }
        public string POAmount { get; set; }
        public string WONo { get; set; }

        public string WODate { get; set; }
        public decimal WOAmt { get; set; }
       
        public string Address { get; set; }
        public decimal AdvancClaimRcvAmt { get; set; }
        public decimal RemainingAmnt { get; set; }
        public string AdvancRecvID { get; set; }
        public string WOInfoID { get; set; }
        public string AdvancClaimRcvdDate { get; set; }
        public int POInstallmentNo { get; set; }
        public int POInstallmentID { get; set; }
        public double POInstallmentAmt { get; set; }
        public string BillType { get; set; }
        public string BillCategory { get; set; }
    }


    public class VendorBillRecvd
    {
        public string VendorQutnID { get; set; }
        public string RequisitionNo { get; set; }
        public string VendorName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string TermsID { get; set; }
        public string FormName { get; set; }
        public string VendorQutnNo { get; set; }
        public DateTime? RequisitionDate { get; set; }
        public string QuotationDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public decimal? POAmount { get; set; }
        public decimal? AdvancePaidAmount { get; set; }
        public decimal? BillAmount { get; set; }
        public string VendorBillNo { get; set; }
        public string VendorBillDate { get; set; }
        public string BillReceiveDate { get; set; }
        public decimal? VATPercentage { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? TDSPercentage { get; set; }
        public decimal? TDSAmount { get; set; }
        public decimal? VDSPercentage { get; set; }
        public decimal? VDSAmount { get; set; }
        public decimal? TotalBillAmount { get; set; }
        public decimal? NetPayableAmount { get; set; }
        public string Note { get; set; }
        public string SetBy { get; set; }
        public string VendorID { get; set; }
        public int VendrFinalBilRcvdID { get; set; }
        public string PoNo { get; set; }
        public string PoAprvDate { get; set; }
        public string Action { get; set; }
        public int VendorFinalBilRecmID { get; set; }
        public string RecommendDate { get; set; }
        public string AprvDate { get; set; }
        public string Operation { get; set; }
        public decimal? RAWO { get; set; }
        public decimal? RecommendedAmnt { get; set; }
        public decimal? AprvAmnt { get; set; }
        public int VendrFinalBilAprvID { get; set; }

        public string WONo { get; set; }
        public string WODate { get; set; }
        public decimal WOAmt { get; set; }
        public string Address { get; set; }
        public decimal AdvancClaimRcvAmt { get; set; }
        public decimal RemainingAmnt { get; set; }
        public int ClientFinalBilPreprID { get; set; }
        public string AdvancRecvID { get; set; }
        public string WOInfoID { get; set; }
        public string AdvancClaimRcvdDate { get; set; }
        public string QuotatDate { get; set; }
        public string RequiDate { get; set; }
        public int ClientFinalBilRecmID { get; set; }
        public int ClientFinalBilAprvID { get; set; }
        public string AdvancePaidDate { get; set; }
        public string AdvancePaidID { get; set; }
        public object POInstallmentNo { get; set; }
        public object POInstallmentID { get; set; }
        public object POInstallmentAmt { get; set; }
        public object BillType { get; set; }
        public object BillCategory { get; set; }
    }
}
