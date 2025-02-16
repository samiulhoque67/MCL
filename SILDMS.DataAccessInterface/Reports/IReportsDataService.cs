using SILDMS.Model.CBPSModule;
//using SILDMS.Model.CheckPrintModule;
using SILDMS.Model.DocScanningModule;
//using SILDMS.Model.LCModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface.Reports
{
    public interface IReportsDataService
    {
        DataTable GetRptUserActivityStatus(string FromDate, string ToDate, string userID, string id, string action, out string errorNumber);
        DataTable GetRptUserDetails(string UserRptID, string BillReceiveFromDate, string Status, string id, string action, out string errorNumber);
        DataTable RequisitionMovementInfo(string RequisitionNo, out string errorNumber);
        DataTable VendorCSApprevedReport(string VendorReqID, string ServiceItemID, out string errorNumber);
        DataTable GetRptOwnerList(string OwnerLevelID, string OwnerID, string ParentOwnerID, string Status, string id, string action, out string errorNumber);
        DataTable GetRptDocumentsList(string OwnerLevelID, string OwnerID, string DocCategoryID, string DocTypeID, string DocPropertyID, string Status, string id, string action, out string errorNumber);
        DataTable GetRptRoleList(string OwnerLevelID, string OwnerID, string RoleID, string Status, string id, string action, out string errorNumber);
        string GetCompanyOrOwnerNameByUserID(string UserID);

        List<RptVendorWithAddress> GetVendorNameAndAddress(string companyID, out string errorNumber);
        DataTable ClientAprvBillReport(string woinfoID, int installmentNo, int clientBillAprvID, out string errorNumber);
        DataTable ClientQuotationApproveReport(string ClientQutnAprvID, out string errorNumber);
        DataTable ClientAprvBillReport(string woinfoID, int installmentNo, int clientBillAprvID,  string BillCategory, out string _errorNumber);
        DataTable FinalClientBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber);
        DataTable FinalClientDueBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber);
    }
}
