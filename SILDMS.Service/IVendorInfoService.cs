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
        string SaveVendorInfoMst(OBS_VendorInfo _modelVendorInfoMst);
        string SaveVendorAddress(OBS_VendorAddressInfo _modelVendorInfoMst);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetVendorInfoSearchList(out List<OBS_VendorInfo> VendorInfoSearchList);
        ValidationResult GetVendorAddressList(string VendorID, out List<OBS_VendorAddressInfo> VendorInfoSearchList);
        ValidationResult GetJobLocation(string UserID, out List<Sys_MasterData> objMasterDatas);
    }
}

