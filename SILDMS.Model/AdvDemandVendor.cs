using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    
        public class POinfo
        {
        // Common Properties
        public string PoNo { get; set; }
        public string PODate { get; set; }
        public string VendorName { get; set; }
        public string ClientID { get; set; }
        public string VendorQutnID { get; set; }
        public string VendorQutnNo { get; set; }
        public string QuotationDate { get; set; }
        public string POAprvAmnt { get; set; }
        public string AdvncDemnAmt { get; set; }
        public string VendorID { get; set; }
        public string POAprvID { get; set; }
        public string ClientName { get; set; }
        public string VendrAdvncDemnID { get; set; }
        public string POAmt { get; set; }

        // Unique Properties from POAdvaceRecomDetails
        public string ProposedAmt { get; set; }
        public string AdvncDemnDate { get; set; }
        public string RemainingAmt { get; set; }
        public string UserFullName { get; set; }
        public string ClientReqID { get; set; }
        public string WONo { get; set; }
        public string WOInfoID { get; set; }
        public string WOAmt { get; set; }
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

        //----
        public string Operation { get; set; }
        public string RecommendationAmount { get; set; }
        public string RecommendedDate { get; set; }
        public string VendorAdvancID { get; set; }
        public string WOInfoID { get; set; }
        public string ClientReqID { get; set; }

    }

    public class AdvanceClaimMaster
    {
        public string ClientID { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string WOInfoID { get; set; }
        public string WOAmt { get; set; }
        public string AdvanceClaimAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string AdvanceClaimDate { get; set; }
        public string Note { get; set; }
    }

}
