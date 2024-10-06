using SILDMS.Model;
using SILDMS.Model.CBPSModule;
using SILDMS.Model.DocScanningModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IServicesCategoryDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string ServicesCategoryId, string action, out string errorNumber);
        string AddServicesCategory(OBS_ServicesCategory ownerLevel, string action, out string errorNumber);
        List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber);
    }
}
