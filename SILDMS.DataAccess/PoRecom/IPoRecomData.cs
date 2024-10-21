using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.PoRecom
{
    public interface IPoRecomData
    {
        List<OBS_ClientReq> GetPoRecomClientInfo(string serviceCategoryID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetPORecomDashBordData(string userID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetPORecomInfoTermList(string pOPreparationID, out string errorNumber);
        List<OBS_VendorCSRecmItem> GetVendorPORecomQuotationItem(string vendorID, string clientID, string serviceCategoryID, string pOPreparationID, out string errorNumber);
        string SaveVendorPORecomInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm);
    }
}
