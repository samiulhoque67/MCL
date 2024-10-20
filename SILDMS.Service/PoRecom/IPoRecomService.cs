﻿using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoRecom
{
    public interface IPoRecomService
    {
        ValidationResult GetPoRecomClientInfo(string serviceCategoryID, out List<OBS_ClientReq> cSClientList);
        ValidationResult GetPORecomDashBordData(string userID, out List<OBS_VendorCSRecmItem> result);
        ValidationResult GetPORecomInfoTermList(string pOPreparationID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList);
        ValidationResult GetVendorPORecomQuotationItem(string vendorID, string clientID, string serviceCategoryID, string pOPreparationID, out List<OBS_VendorCSRecmItem> venCSItemList);
    
        string SaveVendorPORecomInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm);
    }
}
