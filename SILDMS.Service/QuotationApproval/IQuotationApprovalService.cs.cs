using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.QuotationApproval
{
    public interface IQuotationApprovalService
    {

        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientwithReqQoutn> AllAvailableClientsList);
        ValidationResult GetClientReqDataInfoService(string ClientID, string ClientReqID, out List<ClientReqData> GetClientReqDetails);
        ValidationResult GetVendorTermListService(string ClientQutnRecmID, out List<OBS_TermsItem> VendorTermTermList);

        string SaveQuotToClientService(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl);

    }
}
