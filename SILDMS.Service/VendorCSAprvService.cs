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
        public ValidationResult GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID, out List<OBS_VendorCSAprv> VendorCSAprvSearchList)
        {
            VendorCSAprvSearchList = clientInfoDataService.GetVendorCSVendorsUsingClient(ClientID, VendorCSRecmID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorCSRecmItemID, out List<OBS_VendorCSAprvItem> VendorCSAprvItemList)
        {
            VendorCSAprvItemList = clientInfoDataService.GetVendorCSQuotationItem(VendorID, ClientID, VendorCSRecmItemID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSAprvTermList(string VendorCSAprvID,string VendorID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = clientInfoDataService.GetVendorCSAprvTermList(VendorCSAprvID,VendorID);
            return ValidationResult.Success;
        }
        public string SaveVendorCSAprv(OBS_VendorCSAprv clientReq, List<OBS_VendorCSAprvItem> clientReqItem, List<OBS_VendorCSAprvTerms> clientReqTerm)
        {
            return clientInfoDataService.SaveVendorCSAprv(clientReq, clientReqItem, clientReqTerm);
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

        public ValidationResult GetAllRequisition(string userID, out List<Invitation> invitationList)
        {
            invitationList = clientInfoDataService.GetAllRequisition(userID);
            return ValidationResult.Success;
        }

        public ValidationResult GetMaterialByRequisition(string vendorRequisitionNumber, out List<OBS_VendorCSAprvItem> reqWiseMaterialList)
        {
            reqWiseMaterialList = clientInfoDataService.GetMaterialByRequisition(vendorRequisitionNumber);
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorByMaterialService(string vendorReqID, string serviceItemID, out List<OBS_VendorCSAprvItem> matWiseVendorList)
        {
            matWiseVendorList = clientInfoDataService.GetVendorByMaterialData(vendorReqID, serviceItemID);
            return ValidationResult.Success;
        }

        public ValidationResult SearchCSService(string userID, out List<Invitation> searchCSList)
        {
            searchCSList = clientInfoDataService.SearchCSData(userID);
            return ValidationResult.Success;
        }

        public ValidationResult CSVendorService(string userID, string cSNumber, out List<OBS_VendorCSRecmItem> vendorCSList)
        {
            vendorCSList = clientInfoDataService.CSVendorData(userID, cSNumber);
            return ValidationResult.Success;
        }

        public ValidationResult CSVendorTerms(string cSNumber, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList)
        {
            vendorCSInfoTermList = clientInfoDataService.CSVendorTerms(cSNumber);
            return ValidationResult.Success;
        }
    }
}
