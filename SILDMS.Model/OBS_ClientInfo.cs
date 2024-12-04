using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientInfo
    {
        public string ClientID { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string ClientCategoryID { get; set; }
        public string ClientCategoryName { get; set; }
        public string ClientTinNo { get; set; }
        public string ClientBinNo { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        [Required]
        public string Status { get; set; }
        public string ClientReqID { get; set; }
        public string ReqType { get; set; }
    }

    public class OBS_ClientDetails
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientTinNo { get; set; }
        public string ClientBinNo { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string ServiceCategoryID { get; set; }
        public string ClientReqID { get; set; }
    }

    public class OBS_ClientwithReqQoutn
    {
        public string ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string ClientQutnID { get; set; }
        public string QuotationNo { get; set; }
        public string ClientAdvanceClaimDate { get; set; }
        public string QutnAmt { get; set; }
        public string ServiceCategoryID { get; set; }
        public string QutnPrice { get; set; }
        public string ProcessStatus { get; set; }
        public string ClientReqID { get; set; }
        public string WOInfoID { get; set; }
        public string WONo { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string AdvancClaimID { get; set; }
        public string AdvancClaimRecmID { get; set; }
        public string AdvancClaimAprvID { get; set; }
        public string ReqType { get; set; }
    }


    public class ClientReqData
    {
        public string ServiceItemID { get; set; }
        public string ServiceItemCode { get; set; }
        public string ServiceItemName { get; set; }
        public string ServiceCategoryID { get; set; }
        public string Description { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryMode { get; set; }
        public string ReqQnty { get; set; }
        public string ReqUnit { get; set; }
        public string QutnQnty { get; set; }
        public string QutnPrice { get; set; }
        public string VenPrice { get; set; }
        public string MclPrice { get; set; }
        public string QutnUnit { get; set; }
        public string QutnAmt { get; set; }
        public string VatPerc { get; set; }
        public string VatAmt { get; set; }
        public string TolAmt { get; set; }
        public string TermsID { get; set; }
        public string ClientID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientQuotationID { get; set; }
        public string ClientQutnRecmID { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string VendorCSAprvID { get; set; }
        public string ClientReqNo { get; set; }
        public string ClientQutnID { get; set; }
        public string ClientQutnAmt { get; set; }
        public string VendorCSAprvItemID { get; set; }
        public string ClientQutnItemID { get; set; }
        public string ClientQutnRecmItemID { get; set; }
        public string Remarks { get; set; }
        public string BriefingDate { get; set; }
        public string QutnNo { get; set; }
        public string QuotationDate { get; set; }
    }

    public class OBS_QutntoClientMaster
    {
        public string BriefingDate { get; set; }
        public string ClientID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientQuotationID { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string QuotationNote { get; set; }
        public string Operation { get; set; }
        public string ClientQutnRecmID { get; set; }
    }

    public class AdvanClaimWo
    {
        public string ClientID { get; set; }
        public string ClientQutnAprvID { get; set; }
        public string WOInfoID { get; set; }
        public string WOAmt { get; set; }
        public string AdvancClaimAmt { get; set; }
        public string RemainingAmt { get; set; }
        public string AdvancClaimDate { get; set; }
        public string AdvancClaimNote { get; set; }
        public string AdvancClaimID { get; set; }
        public string AdvancClaimRecmID { get; set; }
        public string AdvancClaimRecmAmt { get; set; }
        public string RecomRemainingAmt { get; set; }
        public string AdvancClimRecmDate { get; set; }
        public string RecomNote { get; set; }
        public string AdvancClaimAprvID { get; set; }
        public string AdvancClaimAprvAmt { get; set; }
        public string AdvancClimAprvDate { get; set; }
        public string Note { get; set; }
    }

}
