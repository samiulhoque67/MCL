using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    
        public class POinfo
        {
        public string PoNo { get; set; }
        public string PODate { get; set; }
        public string VendorName { get; set; }
        public string ClientID { get; set; }
        public string VendorQutnID { get; set; }
        public string VendorQutnNo { get; set; }
        public string QuotationDate { get; set; }
        public string POAprvAmnt { get; set; }
        public string VendorID { get; set; }
        public string POAprvID { get; set; }
         }


    public class AdvanceDemandMaster
    {
        public string ClientID { get; set; }
        public string VendorID { get; set; }
        public string VendorQutnID { get; set; }
        public string POAprvID { get; set; }
        public string PurchaseOrderAmount { get; set; }
        public string AdvanceInvoiceNo { get; set; }
        public string AdvanceDemandAmount { get; set; }
        public string AdvanceDemandDate { get; set; }
        public string ProposedAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string Note { get; set; }
    }

}
