using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    namespace SILDMS.Model
    {
        // One dashboard card (IsQueue = "1") or shortcut link (IsQueue = "0")
        public class OBS_DashboardStage
        {
            public string StageCode { get; set; }
            public string StageTitle { get; set; }
            public string GroupName { get; set; }
            public string MenuID { get; set; }
            public string Url { get; set; }
            public string SortOrder { get; set; }
            public string IsOptional { get; set; }     // "1" = optional layer, not counted in the totals
            public string IsQueue { get; set; }        // "1" = work queue, "0" = shortcut link
            public string PendingMeans { get; set; }   // what "pending" means for this stage (tooltip)
        }

        // One waiting item in a stage's queue
        public class OBS_DashboardItem
        {
            public string StageCode { get; set; }
            public string ClientReqID { get; set; }
            public string ClientReqNo { get; set; }
            public string ProjectName { get; set; }
            public string ClientName { get; set; }
            public string RefNo { get; set; }          // PO no / WO no / vendor, depending on the stage
            public string Detail { get; set; }
            public string WaitingSince { get; set; }   // dd/MM/yyyy
            public string DaysWaiting { get; set; }
            public string RaisedBy { get; set; }       // who raised the client requisition
            public string IsMine { get; set; }         // "1" when the logged-in user raised it
        }
    }
}
