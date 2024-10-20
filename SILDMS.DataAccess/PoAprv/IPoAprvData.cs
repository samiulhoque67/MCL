using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.PoAprv
{
    public interface IPoAprvData
    {
        List<OBS_ClientReq> GetPoAprvClientInfo(string serviceCategoryID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetPOAprvDashBordData(string userID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetPOAprvInfoTermList(string PORecmID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetVendorPOAprvQuotationItem(string vendorID, string clientID, string serviceCategoryID, string PORecmID, out string errorNumber);
        string SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm);

    }
}
