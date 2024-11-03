using SILDMS.DataAccessInterface;
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
    public class VendorRequisitionService : IVendorRequisitionService
    {
        #region Fields

        private readonly IVendorRequisitionDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorRequisitionService(IVendorRequisitionDataService repository, ILocalizationService localizationService)
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
        public ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetVendorInfoList();
            return ValidationResult.Success;
        }
        public ValidationResult GetClientReqInfoList(out List<OBS_ClientReq> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetClientReqInfoList();
            return ValidationResult.Success;
        }
        public string SaveVendorRequisition(OBS_VendorReq clientReq, List<OBS_VendorReqItem> clientReqItem, List<OBS_VendorReqTerms> clientReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise)
        {
            return clientInfoDataService.SaveVendorRequisition(clientReq, clientReqItem, clientReqTerm, vendorReqItemWise);
        }
        public ValidationResult GetVendorReqSearchList(out List<OBS_VendorReq> VendorReqSearchList)
        {
            VendorReqSearchList = clientInfoDataService.GetVendorReqSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetServiceCategoryWiseVendorList(string ServiceCategoryID, out List<OBS_VendorInfo> VendorReqSearchList)
        {
            VendorReqSearchList = clientInfoDataService.GetServiceCategoryWiseVendorList(ServiceCategoryID);
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorReqItemList(string VendorReqID, out List<OBS_VendorReqItem> VendorReqItemList)
        {
            VendorReqItemList = clientInfoDataService.GetVendorReqItemList(VendorReqID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorReqTermList(string VendorReqID, out List<OBS_VendorReqTerms> VendorReqTermList)
        {
            VendorReqTermList = clientInfoDataService.GetVendorReqTermList(VendorReqID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorReqTermAgainstFormList(string TermsID, out List<OBS_VendorReqTerms> VendorReqTermList)
        {
            VendorReqTermList = clientInfoDataService.GetVendorReqTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public ValidationResult GetReqWiseVendorList(string VendorReqID, out List<OBS_VendorReqItemWise> VendorReqTermList)
        {
            VendorReqTermList = clientInfoDataService.GetReqWiseVendorList(VendorReqID);
            return ValidationResult.Success;
        }
        public string DeleteVendorReqItemAndTerm(string VendorReqItemID, string VendorReqTermID)
        {
            return clientInfoDataService.DeleteVendorReqItemAndTerm(VendorReqItemID, VendorReqTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}

