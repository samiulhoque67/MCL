﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace SILDMS.DataAccess.AdvDemandVendor
{
    public class AdvDemandVendorData : IAdvDemandVendorData
    {

        private readonly string _spStatusParam;

        public AdvDemandVendorData()
        {
            _spStatusParam = "@p_Status";
        }


        public List<POinfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<POinfo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailablePOInfo"))
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
                    AllAvailableClientsList = dt1.AsEnumerable().Select(reader => new POinfo
                    {
                        VendorName = reader.GetString("VendorName"),
                        PoNo = reader.GetString("PoNo"),
                        PODate = reader.GetString("PODate"),
                        POAprvAmnt = reader.GetString("AprvAmnt"),
                        ClientID = reader.GetString("ClientID"),
                        VendorID = reader.GetString("VendorID"),
                        POAprvID = reader.GetString("POAprvID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public string SaveQuotToClientServiceData(string UserID, List<AdvanceDemandMaster> MasterData, out string errorNumber)
        {
            errorNumber = string.Empty;
            string message = "";
            string Quotation_Number = string.Empty;
            // ################# Master Data ####################

            DataTable masterDataTable = new DataTable();
            masterDataTable.Columns.Add("ClientID", typeof(string));
            masterDataTable.Columns.Add("VendorID", typeof(string));
            masterDataTable.Columns.Add("VendorQutnID", typeof(string));
            masterDataTable.Columns.Add("POAprvID", typeof(string));
            masterDataTable.Columns.Add("PurchaseOrderAmount", typeof(string));
            masterDataTable.Columns.Add("AdvanceInvoiceNo", typeof(string));
            masterDataTable.Columns.Add("AdvanceDemandAmount", typeof(string));
            masterDataTable.Columns.Add("AdvanceDemandDate", typeof(string));
            masterDataTable.Columns.Add("ProposedAmount", typeof(string));
            masterDataTable.Columns.Add("RemainingAmount", typeof(string));
            masterDataTable.Columns.Add("Note", typeof(string));

            if (MasterData != null)
            {
                foreach (var masterItem in MasterData)
                {
                    DataRow masterRow = masterDataTable.NewRow();
                    masterRow["ClientID"] = string.IsNullOrEmpty(masterItem.ClientID) ? DBNull.Value : (object)masterItem.ClientID;
                    masterRow["VendorID"] = string.IsNullOrEmpty(masterItem.VendorID) ? DBNull.Value : (object)masterItem.VendorID;
                    masterRow["VendorQutnID"] = string.IsNullOrEmpty(masterItem.VendorQutnID) ? DBNull.Value : (object)masterItem.VendorQutnID;
                    masterRow["POAprvID"] = string.IsNullOrEmpty(masterItem.POAprvID) ? DBNull.Value : (object)masterItem.POAprvID;
                    masterRow["PurchaseOrderAmount"] = string.IsNullOrEmpty(masterItem.PurchaseOrderAmount) ? DBNull.Value : (object)masterItem.PurchaseOrderAmount;
                    masterRow["AdvanceInvoiceNo"] = string.IsNullOrEmpty(masterItem.AdvanceInvoiceNo) ? DBNull.Value : (object)masterItem.AdvanceInvoiceNo;
                    masterRow["AdvanceDemandAmount"] = string.IsNullOrEmpty(masterItem.AdvanceDemandAmount) ? DBNull.Value : (object)masterItem.AdvanceDemandAmount;
                    masterRow["AdvanceDemandDate"] = string.IsNullOrEmpty(masterItem.AdvanceDemandDate) ? DBNull.Value : (object)masterItem.AdvanceDemandDate;
                    masterRow["ProposedAmount"] = string.IsNullOrEmpty(masterItem.ProposedAmount) ? DBNull.Value : (object)masterItem.ProposedAmount;
                    masterRow["RemainingAmount"] = string.IsNullOrEmpty(masterItem.RemainingAmount) ? DBNull.Value : (object)masterItem.RemainingAmount;
                    masterRow["Note"] = string.IsNullOrEmpty(masterItem.Note) ? DBNull.Value : (object)masterItem.Note;

                    masterDataTable.Rows.Add(masterRow);
                }
            }

           
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveAdvanceDemand"))
            {
                db.AddInParameter(dbCommandWrapper, "@OBS_AdvanceDemand_MasterType", SqlDbType.Structured, masterDataTable);
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