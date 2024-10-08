﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorCSInfo
    {
        public string VendorCSInfoID { get; set; }
        public string VendorQutnID { get; set; } 
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        public string AutoCSNo { get; set; }
        [Required]
        public string CSNo { get; set; }
        [Required]
        public string CSRecDate { get; set; }
        public string Operation { get; set; }
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
    }
}
