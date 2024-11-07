using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ServicesCategory 
    {
        public string ServicesCategoryID { get; set; }
        [Required]
        public string ServicesCategoryCode { get; set; }
        [Required]
        public string ServicesCategoryName { get; set; }
        //public string LevelSL { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public int? Status { get; set; }
        public string ServiceItemName { get; set; }
        public string ServiceItemCode { get; set; }
        public string ServiceItemID { get; set; }
    }
}
