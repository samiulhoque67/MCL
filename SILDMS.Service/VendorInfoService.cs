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
    public class VendorInfoService : IVendorInfoService
    {
        #region Fields

        private readonly IVendorInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorInfoService(IVendorInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }


        #endregion

        public ValidationResult GetAllListedVendorsService(string userID, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_VendorInfo> listedVendorsList)
        {
            listedVendorsList = clientInfoDataService.GetAllListedVendorsDataService(userID, page, itemsPerPage, sortBy, reverse, search, type, out errorNumber);

            return errorNumber.Length > 0 ? new ValidationResult(errorNumber, localizationService.GetResource(errorNumber))
                : ValidationResult.Success;
        }



        public ValidationResult GetAllMaterialService(out List<OBS_ServicesCategory> materials)
        {
            materials = clientInfoDataService.GetAllMaterialData(out errorNumber);
            return errorNumber.Length > 0
                ? new ValidationResult(errorNumber, localizationService.GetResource(errorNumber))
                : ValidationResult.Success;
        }


        public string SaveVendorwithMatService(string UserID, string VendorCode, string VendorName, string ContactPerson, string ContactNumber, string Email,
            string VendorTinNo, string VendorBinNo, string VAddress,
 string BankName, string AccountName, string AccountNumber, string RoutingNumber, List<OBS_ServicesCategory> ServiceItemInfo, int VendorStatus, string TempVendorID, out string errorNumber)
        {
            return clientInfoDataService.SaveVendorwithMatDataService(UserID, VendorCode, VendorName, ContactPerson, ContactNumber, Email,
             VendorTinNo, VendorBinNo, VAddress, BankName, AccountName, AccountNumber, RoutingNumber, ServiceItemInfo, VendorStatus, TempVendorID, out errorNumber);

            throw new NotImplementedException();
        }

    }
}