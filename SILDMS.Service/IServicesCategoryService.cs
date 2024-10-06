using SILDMS.Model;
using SILDMS.Model.CBPSModule;
using SILDMS.Model.DocScanningModule;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IServicesCategoryService 
    {
        ValidationResult GetServicesCategory(string ServicesCategoryId, string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult AddServicesCategory(OBS_ServicesCategory ownerLevel, string action, out string status);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}
