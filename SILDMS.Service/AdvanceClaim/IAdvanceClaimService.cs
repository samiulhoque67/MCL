using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceClaim
{
    public interface IAdvanceClaimService
    {

        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList);
        ValidationResult WoQtforAdvanClaimService(string ClientID, string WOInfoID, string WONo, out List<AdvanClaimWo> WODetails);

        string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData);
    }
}
