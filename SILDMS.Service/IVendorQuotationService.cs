using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorQuotationService
    {
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetShowVendorReqList(out List<OBS_VendorQutn> ClientInfoSearchList);
        string SaveVendorQuotation(OBS_VendorQutn clientReq, List<OBS_VendorQutnItem> clientReqItem, List<OBS_VendorQutnTerms> clientReqTerm);
        ValidationResult GetVendorQutnSearchList(out List<OBS_VendorQutn> VendorQutnSearchList);
        ValidationResult GetVendorQutnItemList(string VendorQutnID, out List<OBS_VendorQutnItem> VendorQutnItemList);
        ValidationResult GetVendorQutnTermList(string VendorQutnID, out List<OBS_VendorQutnTerms> VendorQutnTermList);
        ValidationResult GetVendorQutnTermAgainstFormList(string TermsID, out List<OBS_VendorQutnTerms> VendorQutnTermList);
        string DeleteVendorQutnItemAndTerm(string VendorQutnItemID, string VendorQutnTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}

