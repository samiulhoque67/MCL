using SILDMS.DataAccess.VendorCSWorkflow;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorCSWorkflowController : Controller
    {
        private readonly IVendorCSWorkflowDataService _wf = new VendorCSWorkflowDataService();   // or inject like your other controllers
        private string UserID { get { return SILAuthorization.GetUserID(); } }                  // <- use the same UserID source as your other controllers

        // Back / Postpone / Resume.  Reason is mandatory - enforced here AND in the stored procedure.
        [HttpPost]
        public async Task<dynamic> Act(string Stage, string DocID, string Action, string Reason)
        {
            if ((Action == "BACK" || Action == "POSTPONE" || Action == "RESUME") &&
                (string.IsNullOrWhiteSpace(Reason) || Reason.Trim().Length < 5))
                return Json(new { status = "E400", message = "A reason (at least 5 characters) is required." });

            string status = string.Empty;
            await Task.Run(() => status = _wf.Act(Stage, DocID, Action, Reason, UserID));
            return Json(new { status });
        }

        [HttpPost]
        public async Task<dynamic> GetState(string Stage, string DocID)
        {
            object state = null;
            await Task.Run(() => state = _wf.GetState(Stage, DocID));
            return Json(new { state }, JsonRequestBehavior.AllowGet);
        }

        // CS All Approved / Print page: state of every CS inside the listed requisitions (comma separated ids)
        [HttpPost]
        public async Task<dynamic> GetRequisitionStates(string VendorReqIDs)
        {
            List<Dictionary<string, object>> states = null;
            await Task.Run(() => states = _wf.GetRequisitionStates(VendorReqIDs));
            var r = Json(new { states }, JsonRequestBehavior.AllowGet);
            r.MaxJsonLength = Int32.MaxValue;
            return r;
        }

        [HttpPost]
        public async Task<dynamic> GetHistory(string Stage, string DocID)
        {
            List<Dictionary<string, object>> history = null;
            await Task.Run(() => history = _wf.GetHistory(Stage, DocID));
            var r = Json(new { history }, JsonRequestBehavior.AllowGet);
            r.MaxJsonLength = Int32.MaxValue;
            return r;
        }
    }
}