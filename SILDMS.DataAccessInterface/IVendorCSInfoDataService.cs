using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorCSInfoDataService
    {
        List<OBS_VendorCSRecmItem> GetVendorCSInfoDashBordData(string action, out string errorNumber);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorAndAddressInfo> GetVendorInfoList();
        List<OBS_ClientReq> GetVendorCSClientInfo(string ServiceItemCategoryID);
        List<OBS_VendorCSRecm> OBS_GetVendorCSVendorsUsingClient(string ClientID);
        List<OBS_VendorCSRecmItem> OBS_GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorQutnItemID);
        List<OBS_VendorCSRecmTerms> GetVendorCSInfoTermList(string VendorCSInfoID);
        string SaveVendorCSInfo(OBS_VendorCSRecm clientReq, List<OBS_VendorCSRecmItem> clientReqItem, List<OBS_VendorCSRecmTerms> clientReqTerm);

        List<OBS_VendorCSRecmTerms> GetVendorCSInfoTermAgainstFormList(string TermsID);
        List<OBS_VendorCSRecmVendors> GetReqWiseVendorList(string VendorCSInfoID);
        string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID);
        List<OBS_Terms> GetTermsConditionsList();
        List<Invitation> GetAllRequisition(string userID);
        List<OBS_VendorCSRecmItem> GetMaterialByRequisition(string vendorRequisitionNumber);
        List<OBS_VendorCSRecmItem> GetVendorByMaterialData(string vendorReqID, string serviceItemID);
    }
}
