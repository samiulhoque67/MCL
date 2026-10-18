using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorDemand
{
    public interface IVendorDemandData
    {
        List<OBS_VendorDemand> GetVendorDemandClientInfo(out string errorNumber);
        List<OBS_VendorDemandItem> GetVendorDemandItem(string pOAprvID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetVendorDemandTermList(string pOAprvID, out string errorNumber);
        string SaveVendorDemandInfo(OBS_VendorDemand vendorDemand);
    }
}
