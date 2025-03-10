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
        ValidationResult GetVendorCSInfoDashBordData(string userid, out List<OBS_VendorCSRecmItem> VendorInfoSearchList);
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoSearchList);
        ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID, out List<OBS_ClientReq> VendorInfoSearchList);
        ValidationResult OBS_GetVendorCSVendorsUsingClient(string ClientID, out List<OBS_VendorCSRecm> VendorCSInfoSearchList);
        ValidationResult OBS_GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorQutnItemID, out List<OBS_VendorCSRecmItem> VendorCSInfoItemList);
        ValidationResult GetVendorCSInfoTermList(string VendorCSInfoID, out List<OBS_VendorCSRecmTerms> VendorCSInfoTermList);
        string SaveVendorCSInfo(OBS_VendorCSRecm clientReq, List<OBS_VendorCSRecmItem> clientReqItem, List<OBS_VendorCSRecmTerms> clientReqTerm);
        ValidationResult GetVendorCSInfoTermAgainstFormList(string TermsID, out List<OBS_VendorCSRecmTerms> VendorCSInfoTermList);
        ValidationResult GetReqWiseVendorList(string VendorCSInfoID, out List<OBS_VendorCSRecmVendors> VendorCSInfoTermList);
        string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
        ValidationResult GetAllRequisition(string userID, out List<Invitation> invitationList);
        ValidationResult GetMaterialByRequisition(string vendorRequisitionNumber, out List<OBS_VendorCSRecmItem> reqWiseMaterialList);
        ValidationResult GetVendorByMaterialService(string vendorReqID, string serviceItemID, out List<OBS_VendorCSRecmItem> matWiseVendorList);
        ValidationResult SearchCSService(string userID, out List<Invitation> searchCSList);
        ValidationResult CSVendorService(string userID, string cSNumber, out List<OBS_VendorCSRecmItem> vendorCSList);
        ValidationResult CSVendorTerms(string cSNumber, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList);
    }
}
