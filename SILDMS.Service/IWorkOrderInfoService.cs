using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IWorkOrderInfoService
    {
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetClientInfoList(out List<OBS_WOInfo> ClientInfoSearchList);
        string SaveWorkOrderInfo(OBS_WOInfo woInfo, List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm);
        ValidationResult GetWOInfoSearchList(out List<OBS_WOInfo> WOInfoSearchList);
        ValidationResult GetWOInfoItemList(string WOInfoID, out List<OBS_WOInfoItem> WOInfoItemList);
        ValidationResult GetWOInfoTermList(string WOInfoID, out List<OBS_WOInfoTerms> WOInfoTermList);
        ValidationResult GetWOInfoSearchItemList(string WOInfoID, out List<OBS_WOInfoItem> WOInfoItemList);
        ValidationResult GetWOInfoSearchTermsList(string WOInfoID, out List<OBS_WOInfoTerms> WOInfoTermList);
        ValidationResult GetWOInfoTermAgainstFormList(string TermsID, out List<OBS_WOInfoTerms> WOInfoTermList);
        string DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
