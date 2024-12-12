using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorCSRecm
    {
        public string VendorCSInfoID { get; set; }
        public string VendorQutnID { get; set; }
        public string ServiceCategoryID { get; set; }
        public string POPreparationID { get; set; }
  
 
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string ClientName { get; set; }
        public double PoAmount { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        public string AutoCSNo { get; set; }
        [Required]
        public string CSNo { get; set; }
        public string AutoPoNo { get; set; }
        [Required]
        public string CSRecDate { get; set; }
        public string PORecDate { get; set; }
        public string Operation { get; set; }
        public string RecommendedBy { get; set; } 
        public string VendorQutnNo { get; set; }
        public string QuotationDate { get; set; }
        public string TolQnty { get; set; } 
        public string LastDateofQuotation { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
        public string VendorCSAprvID { get; set; }
     
       
        public double RecommendedAmount { get; set; }
        public object PORecmID { get; set; }
        public string VendorQutnItemID { get; set; }

        public string ClientReqNo { get; set; }
        public string VendorReqID { get; set; }
        public string ServiceItemID { get; set; }
        public string Description { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryMode { get; set; }
        public string ItemCount { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string WIInfoID { get; set; }
    }
}
