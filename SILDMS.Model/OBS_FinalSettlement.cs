using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_FinalSettlement
    {
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string WOInfoID { get; set; }
        public string WONo { get; set; }
        public string WOAmt { get; set; }
        public string ClaimReceivedAmt { get; set; }
        public string ClientBillAmount { get; set; }
        public string CiCommissionAmount { get; set; }
        public string CiBaseAmount { get; set; }
        public string AITAmount { get; set; }
        public string VatAmount { get; set; }
        public string CiReceivedAmount { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string POInfoID { get; set; }
        public string PONo { get; set; }
        public string POAmt { get; set; }
        public string VendorBillAmount { get; set; }
        public string VenAdvncPaidAmt { get; set; }
        public string VenCommissionAmount { get; set; }
        public string VenBaseAmount { get; set; }
        public string VenVatAmount { get; set; }
        public string VenTDSAmount { get; set; }
        public string VenNetPayableAmount { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
    }
}
