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
        List<OBS_VendorCSInfoItem> GetVendorCSInfoDashBordData(string action, out string errorNumber);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorAndAddressInfo> GetVendorInfoList();
        List<OBS_ClientReq> GetVendorCSClientInfo(string ServiceItemCategoryID);
        List<OBS_VendorCSInfo> OBS_GetVendorCSVendorsUsingClient(string ClientID);
        List<OBS_VendorCSInfoItem> OBS_GetVendorCSQuotationItem(string VendorID);
        List<OBS_VendorCSInfoTerms> GetVendorCSInfoTermList(string VendorCSInfoID);
        string SaveVendorCSInfo(OBS_VendorCSInfo clientReq, List<OBS_VendorCSInfoItem> clientReqItem, List<OBS_VendorCSInfoTerms> clientReqTerm, List<OBS_VendorCSVendorsItemWise> vendorReqItemWise);
       
        List<OBS_VendorCSInfoTerms> GetVendorCSInfoTermAgainstFormList(string TermsID);
        List<OBS_VendorCSVendorsItemWise> GetReqWiseVendorList(string VendorCSInfoID);
        string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}
