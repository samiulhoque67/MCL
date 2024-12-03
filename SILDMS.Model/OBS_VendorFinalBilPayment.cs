using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorFinalBilPayment
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
        public DateTime? QuotationDate { get; set; }
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
    }
}
