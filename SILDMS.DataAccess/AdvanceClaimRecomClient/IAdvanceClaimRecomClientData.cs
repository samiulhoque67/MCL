using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILDMS.Utillity;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvanceClaimRecomClient
{
    public interface IAdvanceClaimRecomClientData
    {
        List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID, string WOInfoID, string AdvancClaimID, out string _errorNumber);

        string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData,string Operation, out string _errorNumber);

    }
}
