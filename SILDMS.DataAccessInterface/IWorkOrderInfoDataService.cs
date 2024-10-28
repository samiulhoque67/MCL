using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IWorkOrderInfoDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_WOInfo> GetClientInfoList();
        string SaveWorkOrderInfo(OBS_WOInfo woInfo, List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm);
        List<OBS_WOInfo> GetWOInfoSearchList();
        List<OBS_WOInfoItem> GetWOInfoItemList(string WOInfoID);
        List<OBS_WOInfoTerms> GetWOInfoTermList(string WOInfoID);
        List<OBS_WOInfoItem> GetWOInfoSearchItemList(string WOInfoID);
        List<OBS_WOInfoTerms> GetWOInfoSearchTermsList(string WOInfoID);
        List<OBS_WOInfoTerms> GetWOInfoTermAgainstFormList(string TermsID);
        string DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}