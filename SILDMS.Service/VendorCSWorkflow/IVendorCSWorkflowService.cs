using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorCSWorkflow
{
    public interface IVendorCSWorkflowService
    {
        string Act(string stage, string docID, string action, string reason, string userID);
        Dictionary<string, object> GetState(string stage, string docID);
        List<Dictionary<string, object>> GetHistory(string stage, string docID);
        bool IsBlocked(string stage, string docID, out string message);
        void LogForward(string stage, string docID, string action, string awardMode, string note, string userID);
    }
}
