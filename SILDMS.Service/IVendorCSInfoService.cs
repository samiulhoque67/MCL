using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorCSInfoService
    {
        ValidationResult GetVendorCSInfoDashBordData(string userid, out List<OBS_VendorCSInfoItem> VendorInfoSearchList);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoSearchList);
        ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID,out List<OBS_ClientReq> VendorInfoSearchList);
        ValidationResult OBS_GetVendorCSVendorsUsingClient(string ClientID,out List<OBS_VendorCSInfo> VendorCSInfoSearchList);
        ValidationResult OBS_GetVendorCSQuotationItem(string VendorID, out List<OBS_VendorCSInfoItem> VendorCSInfoItemList);
        ValidationResult GetVendorCSInfoTermList(string VendorCSInfoID, out List<OBS_VendorCSInfoTerms> VendorCSInfoTermList);
        string SaveVendorCSInfo(OBS_VendorCSInfo clientReq, List<OBS_VendorCSInfoItem> clientReqItem, List<OBS_VendorCSInfoTerms> clientReqTerm, List<OBS_VendorCSVendorsItemWise> vendorReqItemWise);
       
        ValidationResult GetVendorCSInfoTermAgainstFormList(string TermsID, out List<OBS_VendorCSInfoTerms> VendorCSInfoTermList);
        ValidationResult GetReqWiseVendorList(string VendorCSInfoID, out List<OBS_VendorCSVendorsItemWise> VendorCSInfoTermList);
        string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
