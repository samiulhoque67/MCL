using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_QuotationToClientRevisedTerms
    {
        public string QuotationToClientRevisedTermID { get; set; }
        public string QuotationToClientRevisedID { get; set; }
        [Required]
        public string TermsID { get; set; }
        [Required]
        public string TermsCode { get; set; }
        [Required]
        public string TermsName { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
