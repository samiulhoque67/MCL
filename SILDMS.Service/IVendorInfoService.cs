using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorInfoService
    {
        string SaveVendorwithMatService(string UserID, string VendorCode, string VendorName, string ContactPerson, string ContactNumber, string Email,
            string VendorTinNo, string VendorBinNo, string VAddress, List<OBS_ServicesCategory> ServiceItemInfo, int VendorStatus, string TempVendorID, out string Status);

        ValidationResult GetAllMaterialService(out List<OBS_ServicesCategory> materials);
        ValidationResult GetAllListedVendorsService(string userID, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out List<OBS_VendorInfo> listedVendorsList);
    }
}

