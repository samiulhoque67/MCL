using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccessInterface;

namespace SILDMS.Service
{
    public class WorkOrderInfoService : IWorkOrderInfoService
    {
        #region Fields

        private readonly IWorkOrderInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public WorkOrderInfoService(IWorkOrderInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = clientInfoDataService.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetClientInfoList(out List<OBS_WOInfo> ClientInfoList)
        {
            ClientInfoList = clientInfoDataService.GetClientInfoList();
            return ValidationResult.Success;
        }
        public string SaveWorkOrderInfo(OBS_WOInfo woInfo, List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm)
        {
            return clientInfoDataService.SaveWorkOrderInfo(woInfo, woInfoItem, woInfoTerm);
        }
        public ValidationResult GetWOInfoSearchList(out List<OBS_WOInfo> WOInfoSearchList)
        {
            WOInfoSearchList = clientInfoDataService.GetWOInfoSearchList();
            return ValidationResult.Success;
        }
        public ValidationResult GetWOInfoItemList(string ClientQutnAprvID, out List<OBS_WOInfoItem> WOInfoItemList)
        {
            WOInfoItemList = clientInfoDataService.GetWOInfoItemList(ClientQutnAprvID);
            return ValidationResult.Success;
        }
        public ValidationResult GetWOInfoTermList(string WOInfoID, out List<OBS_WOInfoTerms> WOInfoTermList)
        {
            WOInfoTermList = clientInfoDataService.GetWOInfoTermList(WOInfoID);
            return ValidationResult.Success;
        }
        public ValidationResult GetWOInfoSearchItemList(string WOInfoID, out List<OBS_WOInfoItem> WOInfoItemList)
        {
            WOInfoItemList = clientInfoDataService.GetWOInfoSearchItemList(WOInfoID);
            return ValidationResult.Success;
        }
        public ValidationResult GetWOInfoSearchTermsList(string WOInfoID, out List<OBS_WOInfoTerms> WOInfoTermList)
        {
            WOInfoTermList = clientInfoDataService.GetWOInfoSearchTermsList(WOInfoID);
            return ValidationResult.Success;
        }
        public ValidationResult GetWOInfoTermAgainstFormList(string TermsID, out List<OBS_WOInfoTerms> WOInfoTermList)
        {
            WOInfoTermList = clientInfoDataService.GetWOInfoTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public string DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID)
        {
            return clientInfoDataService.DeleteWOInfoItemAndTerm(WOInfoItemID, WOInfoTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}