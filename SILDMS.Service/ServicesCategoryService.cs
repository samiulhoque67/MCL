using SILDMS.Model.CBPSModule;
using SILDMS.Model.DocScanningModule;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.Model;
using SILDMS.Service.OwnerLevel;
using SILDMS.DataAccessInterface;

namespace SILDMS.Service
{
    public class ServicesCategoryService : IServicesCategoryService 
    {
        #region Fields

        private readonly IServicesCategoryDataService servicesCategoryDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public ServicesCategoryService(IServicesCategoryDataService repository, ILocalizationService localizationService)
        {
            this.servicesCategoryDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetServicesCategory(string ServicesCategoryId, string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = servicesCategoryDataService.GetServicesCategory(ServicesCategoryId, action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult AddServicesCategory(OBS_ServicesCategory servicesCategory, string action, out string status)
        {
            servicesCategoryDataService.AddServicesCategory(servicesCategory, action, out status);
            if (status.Length > 0)
            {
                return new ValidationResult(status, localizationService.GetResource(status));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas)
        {
            objMasterDatas = servicesCategoryDataService.GetJobLocation(UserID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

       
    }
}
