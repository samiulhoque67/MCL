using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IClientInfoDataService
    {
        string SaveClientInfoMst(OBS_ClientInfo _modelClientInfoMst);
        string SaveClientAddress(OBS_ClientAddressInfo _modelClientInfoMst);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_ClientInfo> GetClientInfoSearchList();
        List<OBS_ClientAddressInfo> GetClientAddressList(string ClientID); 
        List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber);
    }
}

