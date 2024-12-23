using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_WOInfoItem
    {
        public string WOInfoItemID { get; set; }
        public string WOInfoID { get; set; }

        public string ClientQutnAprvItemID { get; set; }
        public string ClientQutnAprvID { get; set; }

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
        public string QutnQnty { get; set; }
        public string QutnPrice { get; set; }
        public string QutnUnit { get; set; }
        public string QutnAmt { get; set; }
        public string WOQnty { get; set; }
        public string WOPrice { get; set; }
        public string WOAmt { get; set; }
        public string Amt { get; set; } 
        public string VatPerc { get; set; }
        public string VatAmt { get; set; }
        public string TolAmt { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
