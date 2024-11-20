using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorQuotationDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorReqItem> GetVendorReqItemListForVenQutn(string VendorReqID);
        List<OBS_VendorQutn> GetShowVendorReqList();
        string SaveVendorQuotation(OBS_VendorQutn vendorQutn, List<OBS_VendorQutnItem> vendorQutnItem, List<OBS_VendorQutnTerms> vendorQutnTerm);
        List<OBS_VendorQutn> GetVendorQutnSearchList();
        List<OBS_VendorQutnItem> GetVendorQutnItemList(string VendorQutnID);
        List<OBS_VendorQutnTerms> GetVendorQutnTermList(string VendorQutnID);
        List<OBS_VendorQutnTerms> GetVendorQutnTermAgainstFormList(string TermsID);
        string DeleteVendorQutnItemAndTerm(string VendorQutnItemID, string VendorQutnTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}
