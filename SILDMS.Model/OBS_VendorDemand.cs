using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SILDMS.Model
{
    // Header row — one per PO Approval (OBS_POAprv), whether or not a
    // demand has been raised yet against it. Used for both the list
    // grid and the detail form, same as OBS_ClientReq is reused for PoAprv.
    public class OBS_VendorDemand
    {
        // Keys
        public string VendorDemandID { get; set; }
        public string POAprvID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        public string VendorID { get; set; }

        // Context (read-only, joined in from PO / Client / Vendor)
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string VendorName { get; set; }
        public string PoNo { get; set; }
        public string TotalPOAmount { get; set; }
        public string PoAprvDate { get; set; }
        public string BillCategory { get; set; }

        // Demand fields
        public string DemandAmount { get; set; }
        public string RemainingPOAmount { get; set; }
        public string DemandDate { get; set; }
        public string DemandRemarks { get; set; }   // stored Remarks, used when listing
        public string DemandedBy { get; set; }       // stored SetBy, used when listing

        // Input-only fields used on save (kept separate from the *Demand* aliases
        // above so a list row and a save payload can both use this one class)
        public string Remarks { get; set; }
        public string SetBy { get; set; }
        public string IsFullPayment { get; set; }
    }

    // Read-only item row for the item grid (sourced from OBS_POArpvItem)
    public class OBS_VendorDemandItem
    {
        public string POAprvItemID { get; set; }
        public string PoAprvID { get; set; }
        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }
        public string ServiceCategoryID { get; set; }
        public string ServiceCategoryName { get; set; }
        public string Description { get; set; }
        public string DeliveryLocation { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string UnitPriceWithoutVat { get; set; }
        public string VatPerc { get; set; }
        public string VatAmount { get; set; }
        public string TotalAmountWithVat { get; set; }
    }

    // Terms row — reuses the same shape as OBS_VendorCSRecmTerms
    // (TermsID, TermsCode, TermsName); if you'd rather not cross-reuse
    // that class here, copy it to OBS_VendorDemandTerm with identical fields.
}
