using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//IVendorReqDataService
namespace SILDMS.DataAccessInterface
{
    public interface IVendorRequisitionDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_VendorAndAddressInfo> GetVendorInfoList();
        List<OBS_ClientReq> GetClientReqInfoList();
        List<OBS_ClientReq> GetClientListForVendorRequisition();
        string SaveVendorRequisition(OBS_VendorReq clientReq, List<OBS_VendorReqItem> clientReqItem, List<OBS_VendorReqTerms> clientReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise);
        List<OBS_VendorReq> GetVendorReqSearchList();
        List<OBS_VendorInfo> GetVendorWiseItemList(string ServiceCategoryID);
        List<OBS_VendorReqItem> GetVendorReqItemList(string VendorReqID);
        List<OBS_VendorReqTerms> GetVendorReqTermList(string VendorReqID);
        List<OBS_VendorReqTerms> GetVendorReqTermAgainstFormList(string TermsID);
        List<OBS_VendorReqItemWise> GetReqWiseVendorList(string VendorReqID);
        string DeleteVendorReqItemAndTerm(string VendorReqItemID, string VendorReqTermID);
        List<OBS_Terms> GetTermsConditionsList();
      
    }
}
