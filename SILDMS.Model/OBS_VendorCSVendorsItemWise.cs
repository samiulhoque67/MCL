using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorCSVendorsItemWise
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
}
