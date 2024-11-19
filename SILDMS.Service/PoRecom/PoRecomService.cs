using SILDMS.DataAccess.POCreation;
using SILDMS.DataAccess.PoRecom;
using SILDMS.Model;
using SILDMS.Service.PoCreation;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoRecom
{
    public class PoRecomService : IPoRecomService
    {
        private readonly IPoRecomData poRecomData;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        public PoRecomService(IPoRecomData _poRecomData, ILocalizationService localizationService)
        {
            poRecomData = _poRecomData;
            this.localizationService = localizationService;
        }

        public ValidationResult GetPoRecomClientInfo( out List<OBS_ClientReq> cSClientList)
        {
            cSClientList = poRecomData.GetPoRecomClientInfo(out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

       

        public ValidationResult GetPORecomInfoTermList(string pOPreparationID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList)
        {
            vendorCSInfoTermList = poRecomData.GetPORecomInfoTermList(pOPreparationID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetVendorPORecomQuotationItem(string vendorID, string clientID,  string pOPreparationID, out List<OBS_VendorCSRecmItem> venCSItemList)
        {
            venCSItemList = poRecomData.GetVendorPORecomQuotationItem(vendorID, clientID, pOPreparationID, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public string SaveVendorPORecomInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            return poRecomData.SaveVendorPORecomInfo(vendorCS, vendorCSItem, vendorCSTerm);

        }
    }
}
