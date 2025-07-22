using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorInfoDataService
    {

        string SaveVendorwithMatDataService(string UserID, string VendorCode, string VendorName, string ContactPerson, string ContactNumber, string Email,
            string VendorTinNo, string VendorBinNo, string VAddress, string BankName, string AccountName, string AccountNumber, string RoutingNumber, List<OBS_ServicesCategory> ServiceItemInfo,int VendorStatus, string TempVendorID, out string _errorNumber);

        List<OBS_VendorInfo> GetAllListedVendorsDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber);

        List<OBS_ServicesCategory> GetAllMaterialData(out string _errorNumber);


    }
}
