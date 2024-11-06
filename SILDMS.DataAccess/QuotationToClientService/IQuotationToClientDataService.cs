using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.QuotationToClientService
{
    public interface IQuotationToClientDataService
    {
        List<OBS_ClientInfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<OBS_ClientDetails> AvailableClientDetailInfoDataService(string ClientID, out string _errorNumber);
        List<OBS_VendorReqTerms> GetTermsConditionsListServiceData(string TermsID, out string _errorNumber);
        List<ClientReqData> GetClientReqDataInfoDataService(string ClientID, out string _errorNumber);
        string SaveQuotToClientServiceData(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl, out string _errorNumber);

    }
}
