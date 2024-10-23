﻿using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.PoCreation
{
    public interface IPOCreationService
    {
        ValidationResult GetPoCreationClientInfo(string serviceCategoryID, out List<OBS_ClientReq> cSClientList);
        ValidationResult GetPOCreationDashBordData(string userID, out List<OBS_VendorCSRecmItem> result);
        ValidationResult GetVendorPOInfoTermList(string vendorCSAprvID, out List<OBS_VendorCSRecmTerms> vendorCSInfoTermList);
        ValidationResult GetVendorPOQuotationItem(string vendorID, string clientID, string serviceCategoryID,string VendorCSAprvID, out List<OBS_VendorCSRecmItem> cSVendorList);
        ValidationResult OBS_GetPOVendorsUsingClient(string clientID,string VendorCSAprvID, string serviceCategoryID, out List<OBS_VendorCSRecm> cSVendorList);
        string SaveVendorPOInfo(OBS_VendorCSRecm vendorCS, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm, List<OBS_VendorCSRecmVendors> vendorCSItemWise);
    }
}