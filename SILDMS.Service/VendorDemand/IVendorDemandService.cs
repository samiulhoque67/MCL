using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorDemand
{
    public interface IVendorDemandService
    {
        void GetVendorDemandClientInfo(out List<OBS_VendorDemand> list);
        void GetVendorDemandItem(string pOAprvID, out List<OBS_VendorDemandItem> list);
        void GetVendorDemandTermList(string pOAprvID, out List<OBS_VendorCSRecmTerms> list);
        string SaveVendorDemandInfo(OBS_VendorDemand vendorDemand);
    }
}
