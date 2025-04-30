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
    public class ClientInfoService : IClientInfoService
    {
        #region Fields

        private readonly IClientInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public ClientInfoService(IClientInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion

        public string SaveClientInfoMst(OBS_ClientInfo modelClientInfoMst, string ClientAddressID)
        {
            return clientInfoDataService.SaveClientInfoMst(modelClientInfoMst, ClientAddressID);
        }

        public string SaveClientAddress(OBS_ClientAddressInfo modelClientAddress)
        {
            return clientInfoDataService.SaveClientAddress(modelClientAddress);
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

        public ValidationResult GetClientInfoSearchList(out List<OBS_ClientInfo> ClientInfoSearchList)
        {
            ClientInfoSearchList = clientInfoDataService.GetClientInfoSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetClientAddressList(string ClientID, out List<OBS_ClientAddressInfo> ClientAddressList)
        {
            ClientAddressList = clientInfoDataService.GetClientAddressList(ClientID);
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
