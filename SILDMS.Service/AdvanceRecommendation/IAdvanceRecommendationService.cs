using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceRecommendation
{
    public interface IAdvanceRecommendationService
    {
        ValidationResult AllAvailableCSVendorApprovalService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<POinfo> AllAvailableClientsList);
        ValidationResult AvailableClientDetailInfoService(string ClientID, string VendrAdvncDemnID, out List<POinfo> ClientDetails);
        string SaveQuotToClientService(string UserID, List<AdvanceDemandMaster> MasterData);

    }
}
