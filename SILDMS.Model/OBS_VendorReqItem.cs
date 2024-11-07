using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorReqItem
    {
        public string VendorReqItemID { get; set; }
        public string ClientReqItemID { get; set; }
        public string VendorReqID { get; set; }
        public string ServiceCategoryID { get; set; }
        public string ServiceCategoryName { get; set; }
        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string DeliveryLocation { get; set; }
        [Required]
        public string DeliveryDate { get; set; }
        public string DeliveryMode { get; set; }
        public string ReqQnty { get; set; }
        public string ReqUnit { get; set; }
        public string CsStatus { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
