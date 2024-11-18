using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorRequisitionService
    {
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoSearchList);
        ValidationResult GetClientReqInfoList(out List<OBS_ClientReq> VendorInfoSearchList);
        ValidationResult GetClientListForVendorRequisition(out List<OBS_ClientReq> VendorInfoSearchList);
        string SaveVendorRequisition(OBS_VendorReq clientReq, List<OBS_VendorReqItem> clientReqItem, List<OBS_VendorReqTerms> clientReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise);
        ValidationResult rptRequisitionToVendorReport(string UserRptID, string action, out DataSet UserDetails);
        ValidationResult GetVendorReqSearchList(out List<OBS_VendorReq> VendorReqSearchList);
        ValidationResult GetVendorWiseItemList(string ServiceCategoryID, out List<OBS_VendorInfo> VendorReqSearchList);
        ValidationResult GetVendorReqItemList(string VendorReqID, out List<OBS_VendorReqItem> VendorReqItemList);
        ValidationResult GetVendorReqTermList(string VendorReqID, out List<OBS_VendorReqTerms> VendorReqTermList);
        ValidationResult GetVendorReqTermAgainstFormList(string TermsID, out List<OBS_VendorReqTerms> VendorReqTermList);
        ValidationResult GetReqWiseVendorList(string VendorReqID, out List<OBS_VendorReqItemWise> VendorReqTermList);
        string DeleteVendorReqItemAndTerm(string VendorReqItemID, string VendorReqTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
