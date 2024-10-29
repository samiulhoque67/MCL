using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvanceClaim
{
    public interface IAdvanceClaimData
    {

        List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID,/* string POAprvID,*/ out string _errorNumber);

        string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData, out string _errorNumber);

    }
}
