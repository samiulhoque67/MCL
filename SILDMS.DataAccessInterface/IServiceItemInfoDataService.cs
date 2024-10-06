using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IServiceItemInfoDataService
    {
        string SaveServiceItemInfo(OBS_ServiceItemInfo _modelServiceItemInfo);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_ServiceItemInfo> GetServiceItemInfoSearchList(string ServiceItemCategoryID);
        List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber);
    }
}

