using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SILDMS.Service.AdvDemandVendor
{
    public interface IAdvDemandVendorService
    {
        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList);
        string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData);

    }
}
