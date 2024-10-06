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
    }
}
