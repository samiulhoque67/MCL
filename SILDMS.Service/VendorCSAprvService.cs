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
    public class VendorCSAprvService : IVendorCSAprvService
    {
        #region Fields

        private readonly IVendorCSAprvDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorCSAprvService(IVendorCSAprvDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetVendorCSAprvDashBordData(string action, out List<OBS_VendorCSAprvItem> servicesCategoryList)
        {
            servicesCategoryList = clientInfoDataService.GetVendorCSAprvDashBordData(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
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
        public ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetVendorInfoList();
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID, out List<OBS_ClientReq> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetVendorCSClientInfo(ServiceItemCategoryID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSVendorsUsingClient(string ClientID, out List<OBS_VendorCSAprv> VendorCSAprvSearchList)
        {
            VendorCSAprvSearchList = clientInfoDataService.GetVendorCSVendorsUsingClient(ClientID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSQuotationItem(string VendorID, string ClientID, out List<OBS_VendorCSAprvItem> VendorCSAprvItemList)
        {
            VendorCSAprvItemList = clientInfoDataService.GetVendorCSQuotationItem(VendorID, ClientID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSAprvTermList(string VendorCSAprvID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = clientInfoDataService.GetVendorCSAprvTermList(VendorCSAprvID);
            return ValidationResult.Success;
        }
        public string SaveVendorCSAprv(OBS_VendorCSAprv clientReq, List<OBS_VendorCSAprvItem> clientReqItem, List<OBS_VendorCSAprvTerms> clientReqTerm, List<OBS_VendorCSAprvVendors> vendorReqItemWise)
        {
            return clientInfoDataService.SaveVendorCSAprv(clientReq, clientReqItem, clientReqTerm, vendorReqItemWise);
        }

        public ValidationResult GetVendorCSAprvTermAgainstFormList(string TermsID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = clientInfoDataService.GetVendorCSAprvTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public ValidationResult GetReqWiseVendorList(string VendorCSAprvID, out List<OBS_VendorCSAprvVendors> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = clientInfoDataService.GetReqWiseVendorList(VendorCSAprvID);
            return ValidationResult.Success;
        }
        public string DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID)
        {
            return clientInfoDataService.DeleteVendorCSAprvItemAndTerm(VendorCSAprvItemID, VendorCSAprvTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}
