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
        ValidationResult GetPoAprvClientInfo( out List<OBS_ClientReq> cSClientList);
    
        ValidationResult GetPOAprvInfoTermList(string PORecmID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList);
        ValidationResult GetVendorPOAprvQuotationItem(string vendorID, string clientID,string PORecmID, out List<OBS_VendorCSRecmItem> venCSItemList);

        string SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm);

    }
}
