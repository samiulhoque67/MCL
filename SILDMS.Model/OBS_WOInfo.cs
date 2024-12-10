using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_WOInfo
    {
        public string WOInfoID { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string ClientID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        [Required]
        public string AutoWOInfoNo { get; set; }
        [Required]
        public string WONo { get; set; }
        [Required]
        public string WODate { get; set; }
        public string WOAmt { get; set; }
        public string RequisitionDate { get; set; }
        public string AprvNo { get; set; }
        public string AprvDate { get; set; } 
        public string Remarks { get; set; }
        public string TolalItem { get; set; }
        public string SelectedItem { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        public string NoOfVendor { get; set; }
        [Required]
        public string Status { get; set; }
        public string VendorCSAprvID { get; set; }

        public string POPreparationID { get; set; }
        public string VendorID { get; set; }
        public string VendorQutnID { get; set; }
        public string VendorName { get; set; }
        public string PORecmID { get; set; }
    }
}
