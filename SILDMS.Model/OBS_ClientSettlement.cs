using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientSettlement
    {
        public string ClientBillCollectionID { get; set; }
        public string ClientFinalBillID { get; set; }
        public string WOInfoID { get; set; }
        public string ClientReqID { get; set; }
        public string ClientID { get; set; }

        // Context
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string ClientReqNo { get; set; }
        public string WONo { get; set; }
        public string BillDate { get; set; }

        // Bill figures
        public string BillItemsAmountWithoutASFVat { get; set; }
        public string BillItemsASFAmt { get; set; }
        public string ASFPerc { get; set; }
        public string BillItemsVatAmt { get; set; }
        public string VatPerc { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }

        // From the collection page
        public string MoneyReceiptNo { get; set; }
        public string ReceiveDate { get; set; }
        public string AITAmount { get; set; }
        public string NetASFAmount { get; set; }
        public string VATPaidBy { get; set; }
        public string CollectionAmount { get; set; }

        // Running totals
        public string AITAmountTotal { get; set; }
        public string AITReceived { get; set; }
        public string AITDue { get; set; }
        public string VATAmount { get; set; }
        public string MushakReceived { get; set; }
        public string MushakDue { get; set; }
        public string EntryCount { get; set; }
        public string SettlementStatus { get; set; }   // Pending | Partially Settled | Settled

        // Save payload
        public string AITChallanReceiveAmt { get; set; }
        public string AITChallanNo { get; set; }
        public string AITChallanDate { get; set; }
        public string AITDocName { get; set; }
        public string AITDocPath { get; set; }
        public string MushakReceiveAmt { get; set; }
        public string MushakNo { get; set; }
        public string MushakDate { get; set; }
        public string MushakDocName { get; set; }
        public string MushakDocPath { get; set; }
        public string SetBy { get; set; }
    }

    public class OBS_ClientSettlementHistoryRow
    {
        public string ClientSettlementID { get; set; }
        public string ClientBillCollectionID { get; set; }
        public string AITChallanReceiveAmt { get; set; }
        public string AITDueAmt { get; set; }
        public string AITChallanNo { get; set; }
        public string AITChallanDate { get; set; }
        public string AITDocName { get; set; }
        public string HasAITDoc { get; set; }
        public string MushakReceiveAmt { get; set; }
        public string MushakDueAmt { get; set; }
        public string MushakNo { get; set; }
        public string MushakDate { get; set; }
        public string MushakDocName { get; set; }
        public string HasMushakDoc { get; set; }
        public string SetOn { get; set; }
        public string SetBy { get; set; }
    }
}
