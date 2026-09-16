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
    }
}