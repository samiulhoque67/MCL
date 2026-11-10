using SILDMS.DataAccess.VendorCSWorkflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorCSWorkflow
{
    public class VendorCSWorkflowService : IVendorCSWorkflowService
    {
        private readonly IVendorCSWorkflowDataService _wfData;

        public VendorCSWorkflowService(IVendorCSWorkflowDataService wfData)
        {
            _wfData = wfData;
        }

        public string Act(string stage, string docID, string action, string reason, string userID)
            => _wfData.Act(stage, docID, action, reason, userID);

        public Dictionary<string, object> GetState(string stage, string docID)
            => _wfData.GetState(stage, docID);

        public List<Dictionary<string, object>> GetHistory(string stage, string docID)
            => _wfData.GetHistory(stage, docID);

        public bool IsBlocked(string stage, string docID, out string message)
            => _wfData.IsBlocked(stage, docID, out message);

        public void LogForward(string stage, string docID, string action, string awardMode, string note, string userID)
            => _wfData.LogForward(stage, docID, action, awardMode, note, userID);
    }
}
