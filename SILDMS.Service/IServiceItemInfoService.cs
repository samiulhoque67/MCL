using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IServiceItemInfoService
    {
        string SaveServiceItemInfo(OBS_ServiceItemInfo _modelServiceItemInfo);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetServiceItemInfoSearchList(string ServiceItemCategoryID,out List<OBS_ServiceItemInfo> ServiceItemInfoSearchList);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}


