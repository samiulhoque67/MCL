using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.DataAccessInterface.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.Model.CBPSModule;
//using SILDMS.Model.CheckPrintModule;
using SILDMS.Model.DocScanningModule;
//using SILDMS.Model.LCModule;

namespace SILDMS.Service.Reports
{
    public class ReportsService : IReportsService
    {
        #region Fields

        private readonly IReportsDataService _reportDataService;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        #endregion

        #region Constructor

        public ReportsService(IReportsDataService reportDataService, ILocalizationService localizationService)
        {
            _reportDataService = reportDataService;
            _localizationService = localizationService;
        }

        #endregion

        public ValidationResult GetRptUserActivityStatus(string FromDate, string ToDate, string userID, string id, string action, out DataTable dt)
        {
            dt = _reportDataService.GetRptUserActivityStatus(FromDate, ToDate, userID, id, action, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetRptUserDetails(string UserRptID, string BillReceiveFromDate, string Status, string id, string action, out DataTable dt)
        {
            dt = _reportDataService.GetRptUserDetails(UserRptID, BillReceiveFromDate, Status, id, action, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult RequisitionMovementInfo(string RequisitionNo, out DataTable dt)
        {
            dt = _reportDataService.RequisitionMovementInfo(RequisitionNo, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult VendorCSApprevedReport(string VendorReqID, string ServiceItemID, out DataTable dt)
        {
            dt = _reportDataService.VendorCSApprevedReport(VendorReqID, ServiceItemID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult VendorRequisitionReport(string VendorReqID, string VendorID, out DataTable dt)
        {
            dt = _reportDataService.VendorRequisitionReport(VendorReqID, VendorID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult VendorAgeingReport(string VendorID, out DataTable dt)
        {
            dt = _reportDataService.VendorAgeingReport(VendorID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult ClientAgeingReport(string ClientID, out DataTable dt)
        {
            dt = _reportDataService.ClientAgeingReport(ClientID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetRptOwnerList(string OwnerLevelID, string OwnerID, string ParentOwnerID, string Status, string id, string action, out DataTable dt)
        {
            dt = _reportDataService.GetRptOwnerList(OwnerLevelID, OwnerID, ParentOwnerID, Status, id, action, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetRptDocumentsList(string OwnerLevelID, string OwnerID, string DocCategoryID, string DocTypeID, string DocPropertyID, string Status, string id, string action, out DataTable dt)
        {
            dt = _reportDataService.GetRptDocumentsList(OwnerLevelID, OwnerID, DocCategoryID, DocTypeID, DocPropertyID, Status, id, action, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult GetRptRoleList(string OwnerLevelID, string OwnerID, string RoleID, string Status, string id, string action, out DataTable dt)
        {
            dt = _reportDataService.GetRptRoleList(OwnerLevelID, OwnerID, RoleID, Status, id, action, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public string GetCompanyOrOwnerNameByUserID(string UserID)
        {
            return _reportDataService.GetCompanyOrOwnerNameByUserID(UserID);
        }

        public ValidationResult GetVendorNameAndAddress(string vendorID, out List<RptVendorWithAddress> RptAllEFTVendorList)
        {
            RptAllEFTVendorList = _reportDataService.GetVendorNameAndAddress(vendorID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult ClientAprvBillReport(string woinfoID, int installmentNo, int clientBillAprvID, string BillCategory, out DataTable dt)
        {
            dt = _reportDataService.ClientAprvBillReport(woinfoID, installmentNo, clientBillAprvID, BillCategory, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult FinalClientBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out DataTable dt)
        {
            dt = _reportDataService.FinalClientBillReport(clientID, billReceiveFromDate, billReceiveToDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult FinalClientDueBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out DataTable dt)
        {
            dt = _reportDataService.FinalClientDueBillReport(clientID, billReceiveFromDate, billReceiveToDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult ClientQuotationApproveReport(string ClientQutnAprvID, out DataTable dt)
        {
            dt = _reportDataService.ClientQuotationApproveReport(ClientQutnAprvID, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult OutputVatStatementReport(string billReceiveFromDate, string billReceiveToDate, out DataTable dt)
        {
            dt = _reportDataService.OutputVatStatementReport(billReceiveFromDate, billReceiveToDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult AITVDSReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out DataTable dt)
        {
            dt = _reportDataService.AITVDSReport(clientID, billReceiveFromDate, billReceiveToDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult TDSVDSReport(string VendorID, string billReceiveFromDate, string billReceiveToDate, out DataTable dt)
        {
            dt = _reportDataService.TDSVDSReport(VendorID, billReceiveFromDate, billReceiveToDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }


        public ValidationResult MonthWiseVendorFinalBillPayment(string VendorID, string CertificateFromDate, out DataTable dt)
        {
            dt = _reportDataService.MonthWiseVendorFinalBillPayment(VendorID, CertificateFromDate, out _errorNumber);
            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }

    }
}
