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
    public class VendorQuotationService : IVendorQuotationService
    {
         
        #region Fields

        private readonly IVendorQuotationDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorQuotationService(IVendorQuotationDataService repository, ILocalizationService localizationService)
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
        public ValidationResult GetVendorReqItemListForVenQutn(string VendorID, string VendorReqID, out List<OBS_VendorReqItem> VendorReqItemList)
        {
            VendorReqItemList = clientInfoDataService.GetVendorReqItemListForVenQutn( VendorID, VendorReqID);
            return ValidationResult.Success;
        }
        public ValidationResult GetShowVendorReqList(out List<OBS_VendorQutn> ClientInfoList)
        {
            ClientInfoList = clientInfoDataService.GetShowVendorReqList();
            return ValidationResult.Success;
        }
        public string SaveVendorQuotation(OBS_VendorQutn vendorQutn, List<OBS_VendorQutnItem> vendorQutnItem, List<OBS_VendorQutnTerms> vendorQutnTerm)
        {
            return clientInfoDataService.SaveVendorQuotation(vendorQutn, vendorQutnItem, vendorQutnTerm);
        }
        public ValidationResult GetVendorQutnSearchList(out List<OBS_VendorQutn> VendorQutnSearchList)
        {
            VendorQutnSearchList = clientInfoDataService.GetVendorQutnSearchList();
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorQutnItemList(string VendorQutnID, out List<OBS_VendorQutnItem> VendorQutnItemList)
        {
            VendorQutnItemList = clientInfoDataService.GetVendorQutnItemList(VendorQutnID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorQutnTermList(string VendorQutnID, out List<OBS_VendorQutnTerms> VendorQutnTermList)
        {
            VendorQutnTermList = clientInfoDataService.GetVendorQutnTermList(VendorQutnID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorQutnTermAgainstFormList(string TermsID, out List<OBS_VendorQutnTerms> VendorQutnTermList)
        {
            VendorQutnTermList = clientInfoDataService.GetVendorQutnTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public string DeleteVendorQutnItemAndTerm(string VendorQutnItemID, string VendorQutnTermID)
        {
            return clientInfoDataService.DeleteVendorQutnItemAndTerm(VendorQutnItemID, VendorQutnTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}
