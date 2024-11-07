using SILDMS.DataAccess;
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
    public class ClientRequisitionService : IClientRequisitionService
    {
        #region Fields

        private readonly IClientRequisitionDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public ClientRequisitionService(IClientRequisitionDataService repository, ILocalizationService localizationService)
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
        public ValidationResult GetClientInfoList(out List<OBS_ClientAndAddressInfo> ClientInfoList)
        {
            ClientInfoList = clientInfoDataService.GetClientInfoList();
            return ValidationResult.Success;
        }
        public string SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm)
        {
            return clientInfoDataService.SaveClientRequisition(clientReq, clientReqItem, clientReqTerm);
        }
        public ValidationResult GetClientReqSearchList(out List<OBS_ClientReq> ClientReqSearchList)
        {
            ClientReqSearchList = clientInfoDataService.GetClientReqSearchList();
            return ValidationResult.Success;
        }
        public ValidationResult GetClientReqItemList(string ClientReqID, string ReqType, out List<OBS_ClientReqItem> ClientReqItemList)
        {
            ClientReqItemList = clientInfoDataService.GetClientReqItemList(ClientReqID, ReqType);
            return ValidationResult.Success;
        }
        public ValidationResult GetClientReqTermList(string ClientReqID, out List<OBS_ClientReqTerms> ClientReqTermList)
        {
            ClientReqTermList = clientInfoDataService.GetClientReqTermList(ClientReqID);
            return ValidationResult.Success;
        }
        public ValidationResult GetClientReqTermAgainstFormList(string TermsID, out List<OBS_ClientReqTerms> ClientReqTermList)
        {
            ClientReqTermList = clientInfoDataService.GetClientReqTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public string DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID)
        {
            return clientInfoDataService.DeleteClientReqItemAndTerm(ClientReqItemID, ClientReqTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}
