using SILDMS.DataAccess.VendorDemand;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorDemand
{
    public class VendorDemandService : IVendorDemandService
    {
        private readonly IVendorDemandData vendorDemandData;

        public VendorDemandService(IVendorDemandData _vendorDemandData)
        {
            vendorDemandData = _vendorDemandData;
        }

        public void GetVendorDemandClientInfo(out List<OBS_VendorDemand> list)
        {
            string errorNumber;
            list = vendorDemandData.GetVendorDemandClientInfo(out errorNumber);
        }

        public void GetVendorDemandItem(string pOAprvID, out List<OBS_VendorDemandItem> list)
        {
            string errorNumber;
            list = vendorDemandData.GetVendorDemandItem(pOAprvID, out errorNumber);
        }

        public void GetVendorDemandTermList(string pOAprvID, out List<OBS_VendorCSRecmTerms> list)
        {
            string errorNumber;
            list = vendorDemandData.GetVendorDemandTermList(pOAprvID, out errorNumber);
        }

        public string SaveVendorDemandInfo(OBS_VendorDemand vendorDemand)
            => vendorDemandData.SaveVendorDemandInfo(vendorDemand);
    }
}
