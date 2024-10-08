﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorAndAddressInfo 
    {
        public string VendorID { get; set; }
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public string VendorName { get; set; }
        public string VendorCategoryID { get; set; }
        public string VendorCategoryName { get; set; }
        public string VendorTinNo { get; set; }
        public string VendorBinNo { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
        public string VendorAddressID { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AddressStatus { get; set; }
    }
}
