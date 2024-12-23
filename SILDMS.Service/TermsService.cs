using SILDMS.DataAccessInterface;
using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess;

namespace SILDMS.Service
{
    public class TermsService: ITermsService
    {
        #region Fields

        private readonly ITermsDataService termsDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public TermsService(ITermsDataService repository, ILocalizationService localizationService)
        {
            this.termsDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion

        public string SaveTermsAndConditions(OBS_Terms vmTerms, List<OBS_TermsItem> vmTermsItem)
        {
            return termsDataService.SaveTermsAndConditions(vmTerms, vmTermsItem);
        }

        public string SaveTermsMst(OBS_Terms modelTermsMst)
        {
            return termsDataService.SaveTermsMst(modelTermsMst);
        }

        public string SaveTermsItem(OBS_TermsItem modelTermsItem)
        {
            return termsDataService.SaveTermsItem(modelTermsItem);
        }

        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = termsDataService.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetTermsSearchList(out List<OBS_Terms> TermsSearchList)
        {
            TermsSearchList = termsDataService.GetTermsSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetTermsItemList(string ClientID, out List<OBS_TermsItem> TermsItemList)
        {
            TermsItemList = termsDataService.GetTermsItemList(ClientID);
            return ValidationResult.Success;
        }

        public ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas)
        {
            objMasterDatas = termsDataService.GetJobLocation(UserID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
    }
}