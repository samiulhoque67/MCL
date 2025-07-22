using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClaimReceived
{
    public interface IClaimReceivedService
    {
        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList);
        ValidationResult AllSavedAdvanceClaimService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList);

        ValidationResult WoQtforAdvanClaimService(string ClientID, string WOInfoID,  string AdvancClaimAprvID, out List<AdvanClaimWo> WODetails);
        ValidationResult AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID,  string AdvancClaimAprvID,string AdvancClaimRcvdID, out List<AdvanClaimWo> WODetails);

        string SaveQuotToClientService(string UserID, List<AdvanceClaimMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo);

    }
}
