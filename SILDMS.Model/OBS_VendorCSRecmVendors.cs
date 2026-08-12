using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorCSRecmVendors
    {
        public string VendorCSVendorsItemWiseID { get; set; }
        public string VendorCSInfoID { get; set; }
        public string VendorCSInfoItemID { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorQutnID { get; set; }
        public string VendorQutnNo { get; set; }
        public string QuotationDate { get; set; }
        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }
        public string VendorTinNo { get; set; }
        public string VendorBinNo { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TolQnty { get; set; }
        public string TolAmt { get; set; }
        public string VenReqQnty { get; set; }
        public string VenReqUnit { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; } 
    }
    public class POPreparationHeader
    {
        public string PoPreparationID { get; set; }
        public string ClientReqID { get; set; }
        public string VendorID { get; set; }
        public string PODate { get; set; }
        public string POAmt { get; set; }
        public string BillType { get; set; }
        public string BillCategory { get; set; }
        public string Installment { get; set; }
        public string InstallmentAmt { get; set; }
        public string Remarks { get; set; }
    }
    public class Invitation
    {
        public string VendorCSRecmName;
        public string CSRecmVendorID;

        public string VendorCSRecmID { get; set; }
        public string VendorRequisitionNumber { get; set; }
        public string ClientRequisitionNumber { get; set; }
        public string UserFullName { get; set; }
        public string ProjectName { get; set; }
        public string ClientReqID { get; set; }

        public string ClientID { get; set; }

        public string VendorReqID { get; set; }

        public string ClientName { get; set; }

        public string VendorCSNumber { get; set; }

        public string RequisitionDate { get; set; }

        public string CSRecDate {  get; set; }

        public string LastDateofQuotation { get; set; }

        public string VendorCSAprvID { get; set; }

        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }

        public string QuotationNo { get; set; }

        public string VendorName { get; set; }

        public string VendorID { get; set; }
        public string Operation { get; set; }
        public string RecommendedBy { get; set; }
        public string Remarks { get; set; }
        public string PoPreparationID { get; set; }
        public string PoDate { get; set; }
        public string ProcessStatus { get; set; }
        public string WOInfoID { get; set; }
        public string RecomAccOperation { get; set; }
        public string VerifyOperation { get; set; }
        public string ApprovalOperation { get; set; }
    }


 




}

