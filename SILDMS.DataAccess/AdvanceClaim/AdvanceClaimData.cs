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

namespace SILDMS.DataAccess.AdvanceClaim
{
    public class AdvanceClaimData : IAdvanceClaimData
    {
        private readonly string _spStatusParam;

        public AdvanceClaimData()
        {
            _spStatusParam = "@p_Status";
        }


        public List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOforAdvnClaim"))
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
                        WOInfoID = reader.GetString("WOInfoID"),
                        WONo = reader.GetString("WONo"),
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        QuotationAprvDate = reader.GetString("QuotationAprvDate")
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public List<AdvanClaimWo> WoQtforAdvanClaimDataService(string ClientID, string WOInfoID, string WONo, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var WODetails = new List<AdvanClaimWo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailQtforAdvanClaim"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", DbType.String, ClientID);
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", DbType.String, WOInfoID);
                db.AddInParameter(dbCommandWrapper, "@WONo", DbType.String, WONo);
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
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WOAmt = reader.GetString("WOAmt")
                    }).ToList();

                }
            }

            return WODetails;
        }


        public string SaveQuotToClientServiceData(string UserID, List<AdvanceClaimMaster> MasterData, out string errorNumber)
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
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveClientAdvanceClaim"))
            {
                db.AddInParameter(dbCommandWrapper, "@OBS_AdvanceClaim_MasterType", SqlDbType.Structured, masterDataTable);
                db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.VarChar, UserID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 1200);


                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    var dr = dt.Rows[0];

                    if (dr["Status"].ToString() == "Successfully Submitted")
                    {
                        message = "Operation Done";
                    }
                    else
                    {
                        message = "Error Found";
                    }
                }
            }

            return message;
        }

    }
}
