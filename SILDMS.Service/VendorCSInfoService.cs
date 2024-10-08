﻿using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public class VendorCSInfoService : IVendorCSInfoService
    {
        #region Fields

        private readonly IVendorCSInfoDataService clientInfoDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public VendorCSInfoService(IVendorCSInfoDataService repository, ILocalizationService localizationService)
        {
            this.clientInfoDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetVendorCSInfoDashBordData(string action, out List<OBS_VendorCSInfoItem> servicesCategoryList)
        {
            servicesCategoryList = clientInfoDataService.GetVendorCSInfoDashBordData(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = clientInfoDataService.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorInfoList(out List<OBS_VendorAndAddressInfo> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetVendorInfoList();
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSClientInfo(string ServiceItemCategoryID, out List<OBS_ClientReq> VendorInfoList)
        {
            VendorInfoList = clientInfoDataService.GetVendorCSClientInfo( ServiceItemCategoryID);
            return ValidationResult.Success;
        }
        public ValidationResult OBS_GetVendorCSVendorsUsingClient(string ClientID,out List<OBS_VendorCSInfo> VendorCSInfoSearchList)
        {
            VendorCSInfoSearchList = clientInfoDataService.OBS_GetVendorCSVendorsUsingClient( ClientID);
            return ValidationResult.Success;
        }
        public ValidationResult OBS_GetVendorCSQuotationItem(string VendorID, out List<OBS_VendorCSInfoItem> VendorCSInfoItemList)
        {
            VendorCSInfoItemList = clientInfoDataService.OBS_GetVendorCSQuotationItem(VendorID);
            return ValidationResult.Success;
        }
        public ValidationResult GetVendorCSInfoTermList(string VendorCSInfoID, out List<OBS_VendorCSInfoTerms> VendorCSInfoTermList)
        {
            VendorCSInfoTermList = clientInfoDataService.GetVendorCSInfoTermList(VendorCSInfoID);
            return ValidationResult.Success;
        }
        public string SaveVendorCSInfo(OBS_VendorCSInfo clientReq, List<OBS_VendorCSInfoItem> clientReqItem, List<OBS_VendorCSInfoTerms> clientReqTerm, List<OBS_VendorCSVendorsItemWise> vendorReqItemWise)
        {
            return clientInfoDataService.SaveVendorCSInfo(clientReq, clientReqItem, clientReqTerm, vendorReqItemWise);
        }
       
        public ValidationResult GetVendorCSInfoTermAgainstFormList(string TermsID, out List<OBS_VendorCSInfoTerms> VendorCSInfoTermList)
        {
            VendorCSInfoTermList = clientInfoDataService.GetVendorCSInfoTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public ValidationResult GetReqWiseVendorList(string VendorCSInfoID, out List<OBS_VendorCSVendorsItemWise> VendorCSInfoTermList)
        {
            VendorCSInfoTermList = clientInfoDataService.GetReqWiseVendorList(VendorCSInfoID);
            return ValidationResult.Success;
        }
        public string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID)
        {
            return clientInfoDataService.DeleteVendorCSInfoItemAndTerm(VendorCSInfoItemID, VendorCSInfoTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = clientInfoDataService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}