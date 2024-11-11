using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorReq
    {
        public string VendorReqID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        [Required]
        public string AutoVendorReqNo { get; set; }
        [Required]
        public string RequisitionNo { get; set; }
        [Required]
        public string RequisitionDate { get; set; }
        public string SubmissionDate { get; set; }
        public string LastDateofQuotation { get; set; }
        public string Remarks { get; set; }
        public string CsStatus { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
        public int TolalItem { get; set; }
        public int SelectedItem { get; set; }
    }
}
