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
    public interface ITermsService
    {
        string SaveTermsMst(OBS_Terms _modelTermsMst);
        string SaveTermsItem(OBS_TermsItem _modelTermsMst);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetTermsSearchList(out List<OBS_Terms> TermsSearchList);
        ValidationResult GetTermsItemList(string ClientID, out List<OBS_TermsItem> TermsSearchList);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}
