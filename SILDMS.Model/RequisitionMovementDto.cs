using System;

namespace SILDMS.Model
{
    public class OBS_RequisitionMovementInfo
    {
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string RequisitionDate { get; set; }
        public string ClientReqStatus { get; set; }
        public string ClientName { get; set; }

        public string ClientReqItemID { get; set; }
        public string ServiceItemID { get; set; }
        public string ServiceItemName { get; set; }
        public string ReqType { get; set; }          // "QV" or "QC"
        public string DeliveryLocation { get; set; }
        public string DeliveryDate { get; set; }

        public string CsStatus { get; set; }          // null/empty = QC item, "1" = WCS, "NCS" = NCS

        public string VendorReqID { get; set; }
        public string VendorReqDate { get; set; }
        public bool VendorReqRaised { get; set; }

        public string FirstVendorQutnDate { get; set; }
        public int VendorQutnCount { get; set; }
        public bool VendorQutnReceived { get; set; }

        public bool CSRecommended { get; set; }
        public string CSRecmDate { get; set; }

        public bool CSApproved { get; set; }
        public string CSAprvDate { get; set; }

        public bool CSActualApproved { get; set; }
        public string CSActualAprvDate { get; set; }

        public bool WorkOrderRaised { get; set; }
        public string WODate { get; set; }

        public bool PurchaseOrderRaised { get; set; }
        public string PODate { get; set; }


        public bool VendorDemandRaised { get; set; }
        public string VendorDemandDate { get; set; }
        public bool VendorFinalBillRaised { get; set; }
        public string VendorFinalBillDate { get; set; }
        public int VendorFinalBillCount { get; set; }
        public bool VendorPaymentDone { get; set; }
        public string VendorPaymentDate { get; set; }
        public int VendorPaymentCount { get; set; }
        public string VendorSettlementStatus { get; set; }
        public string VendorSettlementDate { get; set; }

        // After WO – Client
        public bool ClientClaimRaised { get; set; }
        public string ClientClaimDate { get; set; }
        public decimal ClientClaimAmount { get; set; }
        public bool ClientFinalBillRaised { get; set; }
        public string ClientFinalBillDate { get; set; }
        public int ClientFinalBillCount { get; set; }
        public bool ClientCollectionDone { get; set; }
        public string ClientCollectionDate { get; set; }
        public int ClientCollectionCount { get; set; }
        public string ClientSettlementStatus { get; set; }
        public string ClientSettlementDate { get; set; }



        // Client Quotation layers
        public bool ClientQutnPrepared { get; set; }
        public string ClientQutnPrepDate { get; set; }
        public bool ClientQutnRecommended { get; set; }
        public string ClientQutnRecmDate { get; set; }
        public bool ClientQutnApproved { get; set; }
        public string ClientQutnAprvDate { get; set; }

        // Purchase Order layers (counts: a requisition can have one PO per vendor)
        public int POPrepCount { get; set; }
        public string POPrepDate { get; set; }
        public int PORecmCount { get; set; }
        public string PORecmDate { get; set; }
        public int POAprvCount { get; set; }
        public string POAprvDate { get; set; }

        // Client Claim only opens when a Vendor Advance Demand exists
        public bool ClientClaimApplicable { get; set; }
        public bool CSRecomAcc { get; set; }
        public string RecomAccDate { get; set; }
        public bool CSVerify { get; set; }
        public string CSVerifyDate { get; set; }
        public string ProjectName { get; set; }
    }
}