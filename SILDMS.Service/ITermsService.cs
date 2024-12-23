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
        string SaveTermsAndConditions(OBS_Terms vmTerms, List<OBS_TermsItem> vmTermsItem);

        string SaveTermsMst(OBS_Terms _modelTermsMst);
        string SaveTermsItem(OBS_TermsItem _modelTermsMst);
        ValidationResult GetTermsSearchList(out List<OBS_Terms> TermsSearchList);
        ValidationResult GetTermsItemList(string ClientID, out List<OBS_TermsItem> TermsSearchList);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}
