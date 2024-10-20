using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoAprv
{
    public interface IPoAprvService
    {
        ValidationResult GetPoAprvClientInfo(string serviceCategoryID, out List<OBS_ClientReq> cSClientList);
        ValidationResult GetPOAprvDashBordData(string userID, out List<OBS_VendorCSRecmItem> result);
        ValidationResult GetPOAprvInfoTermList(string PORecmID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList);
        ValidationResult GetVendorPOAprvQuotationItem(string vendorID, string clientID, string serviceCategoryID, string PORecmID, out List<OBS_VendorCSRecmItem> venCSItemList);

        string SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm);

    }
}
