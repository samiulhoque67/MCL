using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientSettlement
    {
        public string ClientSettlementID { get; set; } 
        public string ClientFinalPaymentRcvdID { get; set; }
        public string ClientID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string ClientName { get; set; }
        public string VendorID { get; set; }
        public string WOInfoID { get; set; }
        public string WONo { get; set; }
        public string WODate { get; set; }
        public decimal WOAmt { get; set; }
        public string VendorName { get; set; }
        [Required]
        public string AutoVendorQutnNo { get; set; }
        [Required]
        public string VendorQutnNo { get; set; }
        [Required]

        public string QuotationDate { get; set; }
        public string QutnReceivedDate { get; set; }
       
        public string TDSChallanNo { get; set; }
        public string TDSChallanDate { get; set; }
        public string TDSAmount { get; set; }
        public string TDSPaidAmt { get; set; }
        public string TDSChallanAmt { get; set; }
        public string VDSChallanNo { get; set; }
        public string VDSChallanDate { get; set; }
        public string VDSAmount { get; set; }
        public string VDSPaidAmt { get; set; }
        public string VDSChallanAmt { get; set; }
        public string NetReceivableAmount { get; set; }

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

        public string Address { get; set; }
        public decimal AdvancClaimRcvAmt { get; set; }
        public decimal RemainingAmnt { get; set; }
        public string AdvancRecvID { get; set; }
        public string AdvancClaimRcvdDate { get; set; }
    }
}
