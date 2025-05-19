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
    public interface IClientInfoService
    {
        string SaveClientInfoMst(OBS_ClientInfo _modelClientInfoMst, string ClientAddressID); 
        string SaveClientAddress(OBS_ClientAddressInfo _modelClientInfoMst); 
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetClientInfoSearchList(out List<OBS_ClientInfo> ClientInfoSearchList);
        ValidationResult GetClientAddressList(string ClientID,out List<OBS_ClientAddressInfo> ClientInfoSearchList);
        ValidationResult GetClientAddressList_beforeSave(string ClientAddressID, out List<OBS_ClientAddressInfo> ClientInfoSearchList);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}
