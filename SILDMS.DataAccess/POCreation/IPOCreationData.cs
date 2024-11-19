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
        List<OBS_VendorCSRecmTerms> GetVendorPOInfoTermList(string vendorCSAprvID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetVendorPOQuotationItem(string vendorID, string ClientReqID, out string errorNumber);
        List<OBS_VendorCSRecm> OBS_GetPOVendorsUsingClient(string ClientReqId,out string errorNumber);
        string SaveVendorPOInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmVendors> vendorCSItemWise);
    }
}
