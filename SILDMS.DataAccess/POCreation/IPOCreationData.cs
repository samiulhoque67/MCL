using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.POCreation
{
    public interface IPOCreationData
    {
        List<OBS_ClientReq> GetPoCreationClientInfo( out string errorNumber);
        List<OBS_VendorCSRecmItem> GetPOCreationDashBordData(string userID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetVendorPOInfoTermList(string vendorID, string ClientReqID, string WIInfoID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetVendorPOQuotationItem(string vendorID, string ClientReqID,string WIInfoID, out string errorNumber);
        List<OBS_VendorCSRecm> OBS_GetPOVendorsUsingClient(string ClientReqId,string WIInfoID,out string errorNumber);
        string SaveVendorPOInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSRecmTerms, List<OBS_VendorCSRecmVendors> vendorCSItemWise);
        List<Invitation> SearchPOData(string userID);
    }
}
