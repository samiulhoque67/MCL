using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    public class OBS_ClientBillCollection
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

        // Bill figures (read-only; the save proc re-derives them itself)
        public string BillItemsAmountWithoutASFVat { get; set; }
        public string BillItemsASFAmt { get; set; }
        public string ASFPerc { get; set; }
        public string BillItemsVatAmt { get; set; }
        public string VatPerc { get; set; }
        public string BillItemsAmount { get; set; }
        public string AdvanceAdjustedAmt { get; set; }
        public string NetReceivableAmt { get; set; }

        // Collection fields
        public string AITPaidBy { get; set; }
        public string AITAmount { get; set; }
        public string NetASFAmount { get; set; }
        public string VATPaidBy { get; set; }          // "MediaCom" | "Client"
        public string VATDeductedAmt { get; set; }
        public string CollectionAmount { get; set; }

        // The 5 common payment fields
        public string MoneyReceiptNo { get; set; }
        public string PaymentMode { get; set; }        // Cheque | EFT | Cash
        public string PaymentRefNo { get; set; }
        public string ReceiveDate { get; set; }
        public string Note { get; set; }

        public string HasSettlement { get; set; }      // "1" once any settlement entry exists
        public string SetBy { get; set; }
    }

    // Bill detail grid row — used by both the Collection and Settlement pages
    public class OBS_ClientBillCollectionItem
    {
        public string ClientFinalBillItemID { get; set; }
        public string Description { get; set; }
        public string QutnUnit { get; set; }
        public string BillQty { get; set; }
        public string BillPrice { get; set; }
        public string BillAmt { get; set; }
        public string BillASFPerc { get; set; }
        public string BillASFAmt { get; set; }
        public string BillVatPerc { get; set; }
        public string BillVatAmt { get; set; }
        public string BillTotalAmt { get; set; }
    }
}
