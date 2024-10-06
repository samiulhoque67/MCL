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
    public class VendorInfoService : IVendorInfoService
    {
        #region Fields

        private readonly IVendorInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorInfoService(IVendorInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion

        public string SaveVendorInfoMst(OBS_VendorInfo modelVendorInfoMst)
        {
            return clientInfoDataService.SaveVendorInfoMst(modelVendorInfoMst);
        }

        public string SaveVendorAddress(OBS_VendorAddressInfo modelVendorAddress)
        {
            return clientInfoDataService.SaveVendorAddress(modelVendorAddress);
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

        public ValidationResult GetVendorInfoSearchList(out List<OBS_VendorInfo> VendorInfoSearchList)
        {
            VendorInfoSearchList = clientInfoDataService.GetVendorInfoSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorAddressList(string VendorID, out List<OBS_VendorAddressInfo> VendorAddressList)
        {
            VendorAddressList = clientInfoDataService.GetVendorAddressList(VendorID);
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