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

namespace SILDMS.Service
{
    public class ServiceItemInfoService   : IServiceItemInfoService
    {
        #region Fields

        private readonly IServiceItemInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public ServiceItemInfoService(IServiceItemInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion

        public string SaveServiceItemInfo(OBS_ServiceItemInfo modelServiceItemInfo)
        {
            return clientInfoDataService.SaveServiceItemInfo(modelServiceItemInfo);
        }

        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = clientInfoDataService.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetServiceItemInfoSearchList(string ServiceItemCategoryID, out List<OBS_ServiceItemInfo> ServiceItemInfoSearchList)
        {
            ServiceItemInfoSearchList = clientInfoDataService.GetServiceItemInfoSearchList(ServiceItemCategoryID);
            return ValidationResult.Success;
        }
        public ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas)
        {
            objMasterDatas = clientInfoDataService.GetJobLocation(UserID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
    }
}
