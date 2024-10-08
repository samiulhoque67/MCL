﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientReq
    {
        public string ClientReqID { get; set; } 
        public string ClientID { get; set; }
        public string ClientName { get; set; } 
        [Required]
        public string AutoClientReqNo { get; set; }
        [Required]
        public string ClientReqNo { get; set; }
        [Required]
        public string RequisitionDate { get; set; }
        public string SubmissionDate { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        public string NoOfVendor { get; set; } 
        [Required]
        public string Status { get; set; } 
    }
}
