using SILDMS.DataAccess.PoAprv;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoAprv
{
    public class PoAprvService : IPoAprvService
    {
        private readonly IPoAprvData poAprvData;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        public PoAprvService(IPoAprvData _poAprvData, ILocalizationService localizationService)
        {
            poAprvData = _poAprvData;
            this.localizationService = localizationService;
        }

        public ValidationResult GetPoAprvClientInfo(string serviceCategoryID, out List<OBS_ClientReq> cSClientList)
        {
            cSClientList = poAprvData.GetPoAprvClientInfo(serviceCategoryID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetPOAprvDashBordData(string userID, out List<OBS_VendorCSRecmItem> result)
        {
            result = poAprvData.GetPOAprvDashBordData(userID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetPOAprvInfoTermList(string PORecmID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList)
        {
            vendorCSInfoTermList = poAprvData.GetPOAprvInfoTermList(PORecmID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorPOAprvQuotationItem(string vendorID, string clientID, string serviceCategoryID, string PORecmID, out List<OBS_VendorCSRecmItem> venCSItemList)
        {
            venCSItemList = poAprvData.GetVendorPOAprvQuotationItem(vendorID, clientID, serviceCategoryID, PORecmID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public string SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            return poAprvData.SaveVendorPOAprvInfo(vendorCS, vendorCSItem, vendorCSTerm);

        }
    }
}
