using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ServiceItemInfo
    {
        public string ServiceItemID { get; set; }
        [Required]
        public string ServiceItemCode { get; set; }
        [Required]
        public string ServiceItemName { get; set; }
        public string ServiceItemCategoryID { get; set; }
        public string ServiceItemCategoryName { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
