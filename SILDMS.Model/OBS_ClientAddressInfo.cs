﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientAddressInfo
    {
        public string ClientAddressID { get; set; }
        [Required]
        public string ClientID { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string AddressStatus { get; set; }
        //public int? AddressStatus { get; set; }
    }
}
