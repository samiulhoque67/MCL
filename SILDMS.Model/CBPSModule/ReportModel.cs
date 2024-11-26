using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model.CBPSModule
{
    public class ReportModel 
    {
        public string UserRptID { get; set; }
        public string UserFullName { get; set; }
        public string RequisitionNo { get; set; }
        public string ClientName { get; set; }
        public string Remarks { get; set; }
        public string VendorName { get; set; }
        public string VendorID { get; set; }
        public string VendorCode { get; set; }
        public string VendorAddress { get; set; }
        public string Status { get; set; }
        public string ButtonType { get; set; }
        public string ReportType { get; set; }
        public string Company { get; set; }
        public string VendorType { get; set; }
        public string POType { get; set; }
        public string SignatoryName { get; set; }
        public string SignatoryDesignaion { get; set; }
        public string TIN { get; set; }
        public string BIN { get; set; }
        public string PONo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class RptVendorWithAddress
    {
        public string VendorID { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string TransAccountName { get; set; }
        public string BIN { get; set; }
        public string TIN { get; set; }
        public string VatRegNo { get; set; }
        public string VendorAddress { get; set; }
        public string VendorEmail { get; set; }
    }
}
