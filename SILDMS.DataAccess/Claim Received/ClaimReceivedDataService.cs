using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.Claim_Received
{
    public class ClaimReceivedDataService : IClaimReceivedDataService
    {

        private readonly string _spStatusParam;

        public ClaimReceivedDataService()
        {
            _spStatusParam = "@p_Status";
        }


        public List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOforAdvnClaimRciv"))
            {
                db.AddInParameter(dbCommandWrapper, "@page", SqlDbType.Int, page);
                db.AddInParameter(dbCommandWrapper, "@itemsPerPage", SqlDbType.Int, itemsPerPage);
                db.AddInParameter(dbCommandWrapper, "@sortBy", SqlDbType.NVarChar, sortBy);
                db.AddInParameter(dbCommandWrapper, "@reverse", SqlDbType.Int, reverse ? 1 : 0);
                db.AddInParameter(dbCommandWrapper, "@search", SqlDbType.NVarChar, search);
                db.AddInParameter(dbCommandWrapper, "@type", SqlDbType.NVarChar, type.ToString());
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return AllAvailableClientsList;
                    var dt1 = ds.Tables[0];
                    AllAvailableClientsList = dt1.AsEnumerable().Select(reader => new OBS_ClientwithReqQoutn
                    {
                        AdvancClaimRecmID = reader.GetString("AdvancClaimRecmID"),
                        AdvancClaimAprvID = reader.GetString("AdvancClaimAprvID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WONo = reader.GetString("WONo"),
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        ClientAdvanceClaimDate = reader.GetString("ClientAdvanceClaimDate")
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public List<OBS_ClientwithReqQoutn> AllSavedAdvanceClaimDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_AllSavedAdvanceClaimRcvd"))
            {
                db.AddInParameter(dbCommandWrapper, "@page", SqlDbType.Int, page);
                db.AddInParameter(dbCommandWrapper, "@itemsPerPage", SqlDbType.Int, itemsPerPage);
                db.AddInParameter(dbCommandWrapper, "@sortBy", SqlDbType.NVarChar, sortBy);
                db.AddInParameter(dbCommandWrapper, "@reverse", SqlDbType.Int, reverse ? 1 : 0);
                db.AddInParameter(dbCommandWrapper, "@search", SqlDbType.NVarChar, search);
                db.AddInParameter(dbCommandWrapper, "@type", SqlDbType.NVarChar, type.ToString());
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return AllAvailableClientsList;
                    var dt1 = ds.Tables[0];
                    AllAvailableClientsList = dt1.AsEnumerable().Select(reader => new OBS_ClientwithReqQoutn
                    {
                        AdvancClaimRcvdID = reader.GetString("AdvancClaimRcvdID"),
                        //AdvancClaimID = reader.GetString("AdvancClaimID"),
                        AdvancClaimRecmID = reader.GetString("AdvancClaimRecmID"),
                        AdvancClaimAprvID = reader.GetString("AdvancClaimAprvID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WONo = reader.GetString("WONo"),
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        /* ClientAdvanceClaimDate = reader.GetString("QuotationAprvDate")*/
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }


        public List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID, string WOInfoID, string AdvancClaimAprvID, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var WODetails = new List<AdvanClaimWo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailQtforAdvanClaimRciv"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", DbType.String, ClientID);
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", DbType.String, WOInfoID);
                db.AddInParameter(dbCommandWrapper, "@AdvancClaimAprvID", DbType.String, AdvancClaimAprvID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return WODetails;
                    var dt1 = ds.Tables[0];
                    WODetails = dt1.AsEnumerable().Select(reader => new AdvanClaimWo
                    {
                        ClientID = reader.GetString("ClientID"),
                        AdvancClaimAprvID = reader.GetString("AdvancClaimAprvID"),
                        AdvancClaimID = reader.GetString("AdvancClaimID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WOAmt = reader.GetString("WOAmt"),
                        AdvancClaimAprvAmt = reader.GetString("AdvancClaimAprvAmt"),
                        RemainingAmt = reader.GetString("RemainingAmt"),
                        AdvancClimAprvDate = reader.GetString("AdvancClimAprvDate"),
                        Note = reader.GetString("Note"),
                        WOInstallmentNo = reader.GetString("WOInstallmentNo"),
                        WOInstallmentAmt = reader.GetString("WOInstallmentAmt"),
                        Receivedby = reader.GetString("UserFullName")
                    }).ToList();

                }
            }

            return WODetails;
        }



        public List<AdvanClaimWo> AllSavedAdvanceClaimDetails(string ClientID, string WOInfoID, string AdvancClaimAprvID, string AdvancClaimRcvdID, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var WODetails = new List<AdvanClaimWo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAllSavedAdvanClaimRciv"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", DbType.String, ClientID);
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", DbType.String, WOInfoID);
                db.AddInParameter(dbCommandWrapper, "@AdvancClaimAprvID", DbType.String, AdvancClaimAprvID);
                db.AddInParameter(dbCommandWrapper, "@AdvancClaimRcvdID", DbType.String, AdvancClaimRcvdID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return WODetails;
                    var dt1 = ds.Tables[0];
                    WODetails = dt1.AsEnumerable().Select(reader => new AdvanClaimWo
                    {
                        ClientID = reader.GetString("ClientID"),
                        AdvancClaimAprvID = reader.GetString("AdvancClaimAprvID"),
                        AdvancClaimID = reader.GetString("AdvancClaimID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WOAmt = reader.GetString("WOAmt"),
                        AdvancClaimAprvAmt = reader.GetString("AdvancClaimAprvAmt"),
                        RemainingAmt = reader.GetString("RemainingAmt"),
                        AdvancClimAprvDate = reader.GetString("AdvancClimAprvDate"),
                        Note = reader.GetString("Note"),
                        WOInstallmentNo = reader.GetString("WOInstallmentNo"),
                        WOInstallmentAmt = reader.GetString("WOInstallmentAmt"),
                        Receivedby = reader.GetString("UserFullName"),
                        TransactionMode = reader.GetString("TransactionMode"),
                        ParticularNo = reader.GetString("ParticularNo"),
                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        AdvancClimRcvdDate = reader.GetString("AdvancClimRcvdDate"),
                        RcvdNote = reader.GetString("RcvdNote"),
                    }).ToList();

                }
            }

            return WODetails;
        }


        public string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData, string TransactionMode, string ParticularNo, string MoneyReceiptNo, out string errorNumber)
        {
            errorNumber = string.Empty;
            string message = "";
            string Quotation_Number = string.Empty;
            // ################# Master Data ####################

            DataTable masterDataTable = new DataTable();
            masterDataTable.Columns.Add("ClientID", typeof(string));
            masterDataTable.Columns.Add("ClientQutnAprvID", typeof(string));
            masterDataTable.Columns.Add("WOInfoID", typeof(string));
            masterDataTable.Columns.Add("AdvanceClaimAmount", typeof(string));
            masterDataTable.Columns.Add("AdvanceClaimDate", typeof(string));
            masterDataTable.Columns.Add("RemainingAmount", typeof(string));
            masterDataTable.Columns.Add("Note", typeof(string));

            if (MasterData != null)
            {
                foreach (var masterItem in MasterData)
                {
                    DataRow masterRow = masterDataTable.NewRow();
                    masterRow["ClientID"] = string.IsNullOrEmpty(masterItem.ClientID) ? DBNull.Value : (object)masterItem.ClientID;
                    masterRow["ClientQutnAprvID"] = string.IsNullOrEmpty(masterItem.ClientQutnAprvID) ? DBNull.Value : (object)masterItem.ClientQutnAprvID;
                    masterRow["WOInfoID"] = string.IsNullOrEmpty(masterItem.WOInfoID) ? DBNull.Value : (object)masterItem.WOInfoID;
                    masterRow["AdvanceClaimAmount"] = string.IsNullOrEmpty(masterItem.AdvanceClaimAmount) ? DBNull.Value : (object)masterItem.AdvanceClaimAmount;
                    masterRow["AdvanceClaimDate"] = string.IsNullOrEmpty(masterItem.AdvanceClaimDate) ? DBNull.Value : (object)masterItem.AdvanceClaimDate;
                    masterRow["RemainingAmount"] = string.IsNullOrEmpty(masterItem.RemainingAmount) ? DBNull.Value : (object)masterItem.RemainingAmount;
                    masterRow["Note"] = string.IsNullOrEmpty(masterItem.Note) ? DBNull.Value : (object)masterItem.Note;

                    masterDataTable.Rows.Add(masterRow);
                }
            }


            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveClientAdvanceClaimRciv"))
            {
                db.AddInParameter(dbCommandWrapper, "@OBS_AdvanceClaim_MasterType", SqlDbType.Structured, masterDataTable);
                db.AddInParameter(dbCommandWrapper, "@TransactionMode", SqlDbType.VarChar, TransactionMode);
                db.AddInParameter(dbCommandWrapper, "@ParticularNo", SqlDbType.VarChar, ParticularNo);
                db.AddInParameter(dbCommandWrapper, "@MoneyReceiptNo", SqlDbType.VarChar, MoneyReceiptNo);
                db.AddInParameter(dbCommandWrapper, "@WOInstallmentNo", SqlDbType.VarChar, MasterData[0].WOInstallmentNo);
                db.AddInParameter(dbCommandWrapper, "@WOInstallmentAmt", SqlDbType.VarChar, MasterData[0].WOInstallmentAmt);
                db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.VarChar, UserID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 1200);


                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    var dr = dt.Rows[0];

                    message = dr["Status"].ToString();
                }
            }

            return message;
        }

    }
}
