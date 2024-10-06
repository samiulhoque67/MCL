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
        string SaveVendorInfoMst(OBS_VendorInfo _modelVendorInfoMst);
        string SaveVendorAddress(OBS_VendorAddressInfo _modelVendorInfoMst);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorInfo> GetVendorInfoSearchList();
        List<OBS_VendorAddressInfo> GetVendorAddressList(string VendorID);
        List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber);
    }
}
