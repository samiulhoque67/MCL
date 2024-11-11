using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorCSAprvService
    {
        ValidationResult GetVendorCSAprvDashBordData(string userid, out List<OBS_VendorCSAprvItem> VendorInfoSearchList);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoSearchList);
        ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID, out List<OBS_ClientReq> VendorInfoSearchList);
        ValidationResult GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID, out List<OBS_VendorCSAprv> VendorCSAprvSearchList);
        ValidationResult GetVendorCSQuotationItem(string VendorID, string ClientID,  string VendorCSRecmItemID, out List<OBS_VendorCSAprvItem> VendorCSAprvItemList);
        ValidationResult GetVendorCSAprvTermList(string VendorCSAprvID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList);
        string SaveVendorCSAprv(OBS_VendorCSAprv clientReq, List<OBS_VendorCSAprvItem> clientReqItem, List<OBS_VendorCSAprvTerms> clientReqTerm, List<OBS_VendorCSAprvVendors> vendorReqItemWise);
        ValidationResult GetVendorCSAprvTermAgainstFormList(string TermsID, out List<OBS_VendorCSAprvTerms> VendorCSAprvTermList);
        ValidationResult GetReqWiseVendorList(string VendorCSAprvID, out List<OBS_VendorCSAprvVendors> VendorCSAprvTermList);
        string DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
