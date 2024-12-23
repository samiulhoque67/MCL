using SILDMS.DataAccess;
using SILDMS.DataAccess.POCreation;
using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoCreation
{
    public class POCreationService : IPOCreationService
    {
        private readonly IPOCreationData _pOCreationData;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        public POCreationService(IPOCreationData pOCreationData, ILocalizationService localizationService)
        {
            _pOCreationData = pOCreationData;
            this.localizationService = localizationService;
        }

        public ValidationResult GetPoCreationClientInfo(out List<OBS_ClientReq> cSClientList)
        {
            cSClientList = _pOCreationData.GetPoCreationClientInfo(out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetPOCreationDashBordData(string userID, out List<OBS_VendorCSRecmItem> result)
        {
            result = _pOCreationData.GetPOCreationDashBordData(userID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorPOInfoTermList(string vendorID, string ClientReqID, string WIInfoID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList)
        {
            vendorCSInfoTermList = _pOCreationData.GetVendorPOInfoTermList(vendorID, ClientReqID, WIInfoID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorPOQuotationItem(string vendorID, string ClientReqID,string WIInfoID, out List<OBS_VendorCSRecmItem> cSVendorList)
        {
            cSVendorList = _pOCreationData.GetVendorPOQuotationItem(vendorID, ClientReqID, WIInfoID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult OBS_GetPOVendorsUsingClient(string ClientReqID,string WIInfoID, out List<OBS_VendorCSRecm> cSVendorList)
        {
            cSVendorList = _pOCreationData.OBS_GetPOVendorsUsingClient(ClientReqID, WIInfoID,out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public string SaveVendorPOInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem,List<OBS_VendorCSRecmTerms>vendorCSRecmTerms, List<OBS_VendorCSRecmVendors> vendorCSItemWise)
        {
            return _pOCreationData.SaveVendorPOInfo(vendorCS, vendorCSItem, vendorCSRecmTerms, vendorCSItemWise);
        }

        public ValidationResult SearchPOService(string userID, out List<Invitation> searchCSList)
        {
            searchCSList = _pOCreationData.SearchPOData(userID);
            return ValidationResult.Success;
        }
    }
}
