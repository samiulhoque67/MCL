using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorCSAprvDataService
    {
        List<OBS_VendorCSAprvItem> GetVendorCSAprvDashBordData(string action, out string errorNumber);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorAndAddressInfo> GetVendorInfoList();
        List<OBS_ClientReq> GetVendorCSClientInfo(string ServiceItemCategoryID);
        List<OBS_VendorCSAprv> GetVendorCSVendorsUsingClient(string ClientID);
        List<OBS_VendorCSAprvItem> GetVendorCSQuotationItem(string VendorID, string ClientID);
        List<OBS_VendorCSAprvTerms> GetVendorCSAprvTermList(string VendorCSAprvID);
        string SaveVendorCSAprv(OBS_VendorCSAprv clientReq, List<OBS_VendorCSAprvItem> clientReqItem, List<OBS_VendorCSAprvTerms> clientReqTerm, List<OBS_VendorCSAprvVendors> vendorReqItemWise);
        List<OBS_VendorCSAprvTerms> GetVendorCSAprvTermAgainstFormList(string TermsID);
        List<OBS_VendorCSAprvVendors> GetReqWiseVendorList(string VendorCSAprvID);
        string DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}

