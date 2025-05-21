using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.DataAccessInterface.Reports;
using SILDMS.Model.CBPSModule;
//using SILDMS.Model.CheckPrintModule;
using SILDMS.Model.DocScanningModule;
//using SILDMS.Model.LCModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.Reports
{
    public class ReportsDataService : IReportsDataService
    {
        private readonly string spErrorParam = "@p_Error";
        private readonly string spStatusParam = "@p_Status";

        public DataTable GetRptUserActivityStatus(string FromDate, string ToDate, string userID, string id, string action, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetRptUserActivityStatus"))
            {
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, action);
                //db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, id);
                //db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);

                if (!string.IsNullOrEmpty(FromDate))
                {
                    string[] fromdate = FromDate.Split('/');
                    FromDate = fromdate[2] + "-" + fromdate[1] + "-" + fromdate[0];
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    string[] todate = ToDate.Split('/');
                    ToDate = todate[2] + "-" + todate[1] + "-" + todate[0];
                }

                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@FromDate", SqlDbType.VarChar, FromDate);
                db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);

                dbCommandWrapper.CommandTimeout = 300;

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable GetRptUserDetails(string userID, string FromDate, string Status, string id, string action, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetRptUserDetails"))
            {
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, action);
                //db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, id);
                //db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);

                if (!string.IsNullOrEmpty(FromDate))
                {
                    string[] fromdate = FromDate.Split('/');
                    FromDate = fromdate[2] + "-" + fromdate[1] + "-" + fromdate[0];
                }
                //if (!string.IsNullOrEmpty(ToDate))
                //{
                //    string[] todate = ToDate.Split('/');
                //    ToDate = todate[2] + "-" + todate[1] + "-" + todate[0];
                //}

                db.AddInParameter(dbCommandWrapper, "@RptUserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@FromDate", SqlDbType.VarChar, FromDate);
                //db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                //db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable RequisitionMovementInfo(string ClientReqNo, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetRptRequisitionMovementInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientReqNo", SqlDbType.VarChar, ClientReqNo);
                //db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.VarChar, ServiceItemID);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable VendorCSApprevedReport(string VendorReqID, string ServiceItemID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetReportVendorCSApprevedData"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.VarChar, ServiceItemID);
                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable VendorRequisitionReport(string VendorReqID, /*string VendorID,*/ out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetRptRequisitionToVendorReport"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                //db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }
        public DataTable VendorRequisitionTermsReport(string VendorReqID, /*string VendorID,*/ out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetRptRequisitionToVendorReportsub2"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                //db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable VendorAgeingReport(string VendorID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetRptVendorAgeing"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);

                db.AddInParameter(dbCommandWrapper, "@First", SqlDbType.Int, 30);
                db.AddInParameter(dbCommandWrapper, "@Second", SqlDbType.Int, 60);
                db.AddInParameter(dbCommandWrapper, "@Third", SqlDbType.Int, 90);
                db.AddInParameter(dbCommandWrapper, "@Four", SqlDbType.Int, 120);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable ClientAgeingReport(string ClientID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetRptClientAgeing"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);

                db.AddInParameter(dbCommandWrapper, "@First", SqlDbType.Int, 30);
                db.AddInParameter(dbCommandWrapper, "@Second", SqlDbType.Int, 60);
                db.AddInParameter(dbCommandWrapper, "@Third", SqlDbType.Int, 90);
                db.AddInParameter(dbCommandWrapper, "@Four", SqlDbType.Int, 120);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable GetRptOwnerList(string OwnerLevelID, string OwnerID, string ParentOwnerID, string Status, string id, string action, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetRptOwnerList"))
            {
                db.AddInParameter(dbCommandWrapper, "@OwnerLevelID", SqlDbType.VarChar, OwnerLevelID);
                db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, OwnerID);
                db.AddInParameter(dbCommandWrapper, "@ParentID", SqlDbType.VarChar, ParentOwnerID);
                db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable GetRptDocumentsList(string OwnerLevelID, string OwnerID, string DocCategoryID, string DocTypeID, string DocPropertyID, string Status, string id, string action, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetRptDocumentsList"))
            {

                db.AddInParameter(dbCommandWrapper, "@OwnerLevelID", SqlDbType.VarChar, OwnerLevelID);
                db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, OwnerID);
                db.AddInParameter(dbCommandWrapper, "@DocCategoryID", SqlDbType.VarChar, DocCategoryID);
                db.AddInParameter(dbCommandWrapper, "@DocTypeID", SqlDbType.VarChar, DocTypeID);
                db.AddInParameter(dbCommandWrapper, "@DocPropertyID", SqlDbType.VarChar, DocPropertyID);
                db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable GetRptRoleList(string OwnerLevelID, string OwnerID, string RoleID, string Status, string id, string action, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetRptRoleList"))
            {
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, action);
                //db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, id);
                //db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);

                db.AddInParameter(dbCommandWrapper, "@OwnerLevelID", SqlDbType.VarChar, OwnerLevelID);
                db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.VarChar, OwnerID);
                db.AddInParameter(dbCommandWrapper, "@RoleID", SqlDbType.VarChar, RoleID);
                db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public string GetCompanyOrOwnerNameByUserID(string UserID)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetRptComOwnerDivisionByUserID"))
            {
                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, UserID);
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                    return dt1.Rows[0]["CompanyName"].ToString();
                else
                    return string.Empty;
            }
        }

        public List<RptVendorWithAddress> GetVendorNameAndAddress(string VendorID, out string errorNumber)
        {
            List<RptVendorWithAddress> RptAllEFTVendorList = new List<RptVendorWithAddress>();
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_RptGetVendorsWithAddress"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);

                var ds = db.ExecuteDataSet(dbCommandWrapper);

                DataTable dt1 = ds.Tables[0];
                RptAllEFTVendorList = dt1.AsEnumerable().Select(reader => new RptVendorWithAddress
                {
                    VendorID = reader.GetString("VendorID"),
                    VendorCode = reader.GetString("VendorCode"),
                    VendorName = reader.GetString("VendorName"),
                    TransAccountName = reader.GetString("TransAccountName"),
                    TIN = reader.GetString("TIN"),
                    BIN = reader.GetString("BIN"),
                    VatRegNo = reader.GetString("VatRegNo"),
                    VendorAddress = reader.GetString("VendorAddress"),
                    VendorEmail = reader.GetString("VendorEmail")
                }).ToList();

                return RptAllEFTVendorList;
            }
        }



        public DataTable ClientAprvBillReport(string woinfoID, int installmentNo, int clientBillAprvID, string BillCategory, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientAprvBillReportData"))
            {
                db.AddInParameter(dbCommandWrapper, "@WoInfoID", SqlDbType.VarChar, woinfoID);
                db.AddInParameter(dbCommandWrapper, "@WOInstallmentNo", SqlDbType.Int, installmentNo);
                db.AddInParameter(dbCommandWrapper, "@clientBillAprvID", SqlDbType.Int, clientBillAprvID);
                db.AddInParameter(dbCommandWrapper, "@BillCategory", SqlDbType.VarChar, BillCategory);
                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable FinalClientBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetFinalClientBillReportData"))
            {
                db.AddInParameter(dbCommandWrapper, "@clientID", SqlDbType.VarChar, clientID);
                db.AddInParameter(dbCommandWrapper, "@billReceiveFromDate", SqlDbType.VarChar, billReceiveFromDate);
                db.AddInParameter(dbCommandWrapper, "@billReceiveToDate", SqlDbType.VarChar, billReceiveToDate);

                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }


        public DataTable FinalClientDueBillReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetFinalClientDueBillReportData"))
            {
                db.AddInParameter(dbCommandWrapper, "@clientID", SqlDbType.VarChar, clientID);
                db.AddInParameter(dbCommandWrapper, "@billReceiveFromDate", SqlDbType.VarChar, billReceiveFromDate);
                db.AddInParameter(dbCommandWrapper, "@billReceiveToDate", SqlDbType.VarChar, billReceiveToDate);

                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }


        public DataTable ClientQuotationApproveReport(string ClientQutnAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQuotationApproveReportData3"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, ClientQutnAprvID);
                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable OutputVatStatementReport(string billReceiveFromDate, string billReceiveToDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_OutputVatStatementReportData"))
            {

                db.AddInParameter(dbCommandWrapper, "@billReceiveFromDate", SqlDbType.VarChar, billReceiveFromDate);
                db.AddInParameter(dbCommandWrapper, "@billReceiveToDate", SqlDbType.VarChar, billReceiveToDate);


                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }

        public DataTable AITVDSReport(string clientID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAITVDSReportData"))
            {
                db.AddInParameter(dbCommandWrapper, "@clientID", SqlDbType.VarChar, clientID);
                db.AddInParameter(dbCommandWrapper, "@billReceiveFromDate", SqlDbType.VarChar, billReceiveFromDate);
                db.AddInParameter(dbCommandWrapper, "@billReceiveToDate", SqlDbType.VarChar, billReceiveToDate);

                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;

            }
        }

        public DataTable TDSVDSReport(string VendorID, string billReceiveFromDate, string billReceiveToDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetTDSVDSReportData"))
            {
                db.AddInParameter(dbCommandWrapper, "@vendorID", SqlDbType.VarChar, VendorID);
                db.AddInParameter(dbCommandWrapper, "@billReceiveFromDate", SqlDbType.VarChar, billReceiveFromDate);
                db.AddInParameter(dbCommandWrapper, "@billReceiveToDate", SqlDbType.VarChar, billReceiveToDate);

                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;

            }
        }


        public DataTable MonthWiseVendorFinalBillPayment(string VendorID, string CertificateFromDate, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_MonthWiseVendorFinalBillPayment"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                db.AddInParameter(dbCommandWrapper, "@CertificateFromDate", SqlDbType.VarChar, CertificateFromDate);


                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }

        }

        public DataTable ClientQuotationApproveReport1(string clientQutnAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQuotationApproveReportData4"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, clientQutnAprvID);
                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }
        public DataTable ClientQuotationApproveReport2(string clientQutnAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQuotationApproveReportData5"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, clientQutnAprvID);
                var ds = db.ExecuteDataSet(dbCommandWrapper);
                DataTable dt1 = ds.Tables[0];
                return dt1;
            }
        }
    }
}
