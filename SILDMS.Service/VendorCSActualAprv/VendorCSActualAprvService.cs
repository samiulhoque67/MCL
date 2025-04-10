using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.VendorCSActualAprv;
using SILDMS.DataAccess;

namespace SILDMS.Service.VendorCSActualAprv
{
    public class VendorCSActualAprvService : IVendorCSActualAprvService
    {
        private readonly IVendorCSActualAprvDataService vendorCSActualAprvData;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";


        #region Constractor

        public VendorCSActualAprvService(IVendorCSActualAprvDataService repository, ILocalizationService localizationService)
        {
            this.vendorCSActualAprvData = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetVendorCSAprvDashBordData(string action, out List<OBS_VendorCSAprvItem> servicesCategoryList)
        {
            servicesCategoryList = vendorCSActualAprvData.GetVendorCSAprvDashBordData(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = vendorCSActualAprvData.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoList)
        {
            VendorInfoList = vendorCSActualAprvData.GetVendorInfoList();
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID, out List<OBS_ClientReq> VendorInfoList)
        {
            VendorInfoList = vendorCSActualAprvData.GetVendorCSClientInfo(ServiceItemCategoryID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID, out List<OBS_VendorCSAprv> VendorCSAprvSearchList)
        {
            VendorCSAprvSearchList = vendorCSActualAprvData.GetVendorCSVendorsUsingClient(ClientID, VendorCSRecmID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorCSRecmItemID, out List<OBS_VendorCSAprvItem> VendorCSAprvItemList)
        {
            VendorCSAprvItemList = vendorCSActualAprvData.GetVendorCSQuotationItem(VendorID, ClientID, VendorCSRecmItemID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSAprvTermList(string VendorCSAprvID,string VendorID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = vendorCSActualAprvData.GetVendorCSAprvTermList(VendorCSAprvID,VendorID);
            return ValidationResult.Success;
        }
        public string SaveVendorCSAprv(OBS_VendorCSAprv clientReq, List<OBS_VendorCSAprvItem> clientReqItem, List<OBS_VendorCSAprvTerms> clientReqTerm)
        {
            return vendorCSActualAprvData.SaveVendorCSAprv(clientReq, clientReqItem, clientReqTerm);
        }

        public ValidationResult GetVendorCSAprvTermAgainstFormList(string TermsID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = vendorCSActualAprvData.GetVendorCSAprvTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public ValidationResult GetReqWiseVendorList(string VendorCSAprvID, out List<OBS_VendorCSAprvVendors> VendorCSAprvTermList)
        {
            VendorCSAprvTermList = vendorCSActualAprvData.GetReqWiseVendorList(VendorCSAprvID);
            return ValidationResult.Success;
        }
        public string DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID)
        {
            return vendorCSActualAprvData.DeleteVendorCSAprvItemAndTerm(VendorCSAprvItemID, VendorCSAprvTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = vendorCSActualAprvData.GetTermsConditionsList();
            return ValidationResult.Success;
        }

        public ValidationResult GetAllRequisition(string userID, out List<Invitation> invitationList)
        {
            invitationList = vendorCSActualAprvData.GetAllRequisition(userID);
            return ValidationResult.Success;
        }

        public ValidationResult GetMaterialByRequisition(string vendorRequisitionNumber, out List<OBS_VendorCSAprvItem> reqWiseMaterialList)
        {
            reqWiseMaterialList = vendorCSActualAprvData.GetMaterialByRequisition(vendorRequisitionNumber);
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorByMaterialService(string vendorReqID, string serviceItemID, out List<OBS_VendorCSAprvItem> matWiseVendorList)
        {
            matWiseVendorList = vendorCSActualAprvData.GetVendorByMaterialData(vendorReqID, serviceItemID);
            return ValidationResult.Success;
        }

        public ValidationResult SearchCSService(string userID, out List<Invitation> searchCSList)
        {
            searchCSList = vendorCSActualAprvData.SearchCSData(userID);
            return ValidationResult.Success;
        }

        public ValidationResult CSVendorService(string userID, string cSNumber, out List<OBS_VendorCSRecmItem> vendorCSList)
        {
            vendorCSList = vendorCSActualAprvData.CSVendorData(userID, cSNumber);
            return ValidationResult.Success;
        }

        public ValidationResult CSVendorTerms(string cSNumber, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList)
        {
            vendorCSInfoTermList = vendorCSActualAprvData.CSVendorTerms(cSNumber);
            return ValidationResult.Success;
        }
    }
}
