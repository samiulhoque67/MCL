using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.Claim_Received
{
    public interface IClaimReceivedDataService
    {
        List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID, string WOInfoID, string AdvancClaimAprvID, out string _errorNumber);

        string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo, out string _errorNumber);

    }
}
