using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvDemandVendor
{
    public interface IAdvDemandVendorData
    {
        List<POinfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);
        string SaveQuotToClientServiceData(string UserID, List<AdvanceDemandMaster> MasterData, out string _errorNumber);


    }
}
