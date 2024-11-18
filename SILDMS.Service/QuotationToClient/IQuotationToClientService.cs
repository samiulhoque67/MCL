using System;
using System.Collections.Generic;
using SILDMS.Utillity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.Model;

namespace SILDMS.Service.QuotationToClient
{
    public interface IQuotationToClientService
    {
        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_ClientInfo> AllAvailableClientsList);
        ValidationResult AvailableClientDetailInfoService(string ClientID, out List<OBS_ClientDetails> ClientDetails);
        ValidationResult GetTermsConditionsListService(string VendorCSAprvID,  out List<OBS_TermsItem> VendorTermTermList);
        ValidationResult GetClientReqDataInfoService(string ClientID,string ClientReqID, out List<ClientReqData> GetClientReqDetails);
        string SaveQuotToClientService(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl);
       
    }
}
