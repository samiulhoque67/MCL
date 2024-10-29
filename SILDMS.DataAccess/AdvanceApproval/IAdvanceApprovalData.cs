using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvanceApproval
{
    public interface IAdvanceApprovalData
    {
        List<POinfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        List<POinfo> AvailableClientDetailInfoDataService(string ClientID, string VendrAdvncDemnID, out string _errorNumber);
        string SaveQuotToClientServiceData(string UserID, List<AdvanceDemandMaster> MasterData, out string _errorNumber);

    }
}
