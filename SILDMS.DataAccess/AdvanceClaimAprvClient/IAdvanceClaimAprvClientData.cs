using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvanceClaimAprvClient
{
    public interface IAdvanceClaimAprvClientData
    {
        List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<OBS_ClientwithReqQoutn> AllSavedAdvanceClaimDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);

        List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID, string WOInfoID, string AdvancClaimID,string AdvancClaimRecmID, out string _errorNumber);
        List<AdvanClaimWo> AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimID,string AdvancClaimAprvID, out string _errorNumber);

        string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData, string Operation, out string _errorNumber);

    }
}
