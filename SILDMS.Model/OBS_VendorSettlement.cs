using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_VendorSettlement
    {
        public string VendorSettlementID { get; set; }
        public string VendorFinalBilPaymentID { get; set; }

        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string CAddress { get; set; }

        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string VAddress { get; set; }


        public string PONo { get; set; }
        public string PODate { get; set; }

        public decimal POAmt { get; set; }

        public string WOInfoID { get; set; }
        public string WONo { get; set; }
        public string WODate { get; set; }
        public decimal WOAmt { get; set; }

        public string TDSChallanNo { get; set; }
        public string TDSChallanDate { get; set; }
        public string TDSAmount { get; set; }
        public string TDSPaidAmt { get; set; }
        public string TDSChallanAmt { get; set; }
        public string VDSChallanNo { get; set; }
        public string VDSChallanDate { get; set; }
        public string VDSAmount { get; set; }
        public string VDSPaidAmt { get; set; }
        public string VDSChallanAmt { get; set; }
        public string NetPayableAmount { get; set; }
        public string NetReceivableAmount { get; set; }
        public string Remarks { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }    
        public string Status { get; set; }
        ////////////////Show Field////////////////////
    }
}