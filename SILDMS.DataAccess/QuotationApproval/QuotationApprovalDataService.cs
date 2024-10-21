﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.QuotationApproval
{
    public class QuotationApprovalDataService : IQuotationApprovalDataService
    {

        private readonly string _spStatusParam;

        public QuotationApprovalDataService()
        {
            _spStatusParam = "@p_Status";
        }

        public List<OBS_ClientwithReqQoutn> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<OBS_ClientwithReqQoutn>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetQuotationforApprv"))
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
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        QuotationNo = reader.GetString("AutoQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                        QutnPrice = reader.GetString("QutnPrice"),
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public List<ClientReqData> GetClientReqDataInfoDataService(string ClientID, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var GetClientReqDetails = new List<ClientReqData>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientApprovQDataInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.Int, ClientID);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return GetClientReqDetails;
                    var dt1 = ds.Tables[0];
                    GetClientReqDetails = dt1.AsEnumerable().Select(reader => new ClientReqData
                    {

                        ClientID = reader.GetString("ClientID"),
                        ClientQutnRecmID = reader.GetString("ClientReqID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        TermsID = reader.GetString("TermsID"),
                        ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        ReqUnit = reader.GetString("QutnUnit"),
                        QutnQnty = reader.GetString("QutnQnty"),
                        QutnPrice = reader.GetString("QutnPrice"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        QutnAmt = reader.GetString("RecmAmt"),
                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),
                        TolAmt = reader.GetString("TolAmt")
                    }).ToList();

                }
            }

            return GetClientReqDetails;
        }

        public string SaveQuotToClientServiceData(string UserID, List<OBS_QutntoClientMaster> MasterData, List<ClientReqData> DetailData, List<OBS_TermsItem> AllTermsDtl, out string errorNumber)
        {
            errorNumber = string.Empty;
            string message = "";
            string Quotation_Number = string.Empty;
            // ################# Master Data ####################

            DataTable masterDataTable = new DataTable();
            masterDataTable.Columns.Add("ClientID", typeof(string));
            masterDataTable.Columns.Add("ClientReqID", typeof(string));
            masterDataTable.Columns.Add("ClientQuotationID", typeof(string));
            masterDataTable.Columns.Add("ClientQutnRecmID", typeof(string));
            masterDataTable.Columns.Add("BriefingDate", typeof(string));
            masterDataTable.Columns.Add("Operation", typeof(string));
            masterDataTable.Columns.Add("QuotationNote", typeof(string));

            if (MasterData != null)
            {
                foreach (var masterItem in MasterData)
                {
                    DataRow masterRow = masterDataTable.NewRow();
                    masterRow["ClientID"] = string.IsNullOrEmpty(masterItem.ClientID) ? DBNull.Value : (object)masterItem.ClientID;
                    masterRow["ClientReqID"] = string.IsNullOrEmpty(masterItem.ClientReqID) ? DBNull.Value : (object)masterItem.ClientReqID;
                    masterRow["ClientQuotationID"] = string.IsNullOrEmpty(masterItem.ClientQuotationID) ? DBNull.Value : (object)masterItem.ClientQuotationID;
                    masterRow["ClientQutnRecmID"] = string.IsNullOrEmpty(masterItem.ClientQutnRecmID) ? DBNull.Value : (object)masterItem.ClientQutnRecmID;
                    masterRow["BriefingDate"] = string.IsNullOrEmpty(masterItem.BriefingDate) ? DBNull.Value : (object)masterItem.BriefingDate;
                    masterRow["Operation"] = string.IsNullOrEmpty(masterItem.Operation) ? DBNull.Value : (object)masterItem.Operation;
                    masterRow["QuotationNote"] = string.IsNullOrEmpty(masterItem.QuotationNote) ? DBNull.Value : (object)masterItem.QuotationNote;

                    masterDataTable.Rows.Add(masterRow);
                }
            }


            // ################# Detail Data ####################

            DataTable detailDataTable = new DataTable();
            detailDataTable.Columns.Add("ServiceCategoryID", typeof(string));
            detailDataTable.Columns.Add("TermsID", typeof(string));
            detailDataTable.Columns.Add("ServiceItemID", typeof(string));
            detailDataTable.Columns.Add("Description", typeof(string));
            detailDataTable.Columns.Add("DeliveryLocation", typeof(string));
            detailDataTable.Columns.Add("DeliveryDate", typeof(string));
            detailDataTable.Columns.Add("DeliveryMode", typeof(string));
            detailDataTable.Columns.Add("QutnQnty", typeof(string));
            detailDataTable.Columns.Add("QutnUnit", typeof(string));
            detailDataTable.Columns.Add("VenPrice", typeof(string));
            detailDataTable.Columns.Add("MclPrice", typeof(string));
            detailDataTable.Columns.Add("QutnAmt", typeof(string));
            detailDataTable.Columns.Add("VatPerc", typeof(string));
            detailDataTable.Columns.Add("VatAmt", typeof(string));
            detailDataTable.Columns.Add("TolAmt", typeof(string));

            if (DetailData != null)
            {
                foreach (var item in DetailData)
                {
                    DataRow objDataRow = detailDataTable.NewRow();
                    objDataRow["ServiceCategoryID"] = string.IsNullOrEmpty(item.ServiceCategoryID) ? DBNull.Value : (object)item.ServiceCategoryID;
                    objDataRow["TermsID"] = string.IsNullOrEmpty(item.TermsID) ? DBNull.Value : (object)item.TermsID;
                    objDataRow["ServiceItemID"] = string.IsNullOrEmpty(item.TermsID) ? DBNull.Value : (object)item.ServiceItemID;
                    objDataRow["Description"] = string.IsNullOrEmpty(item.Description) ? DBNull.Value : (object)item.Description;
                    objDataRow["DeliveryLocation"] = string.IsNullOrEmpty(item.DeliveryLocation) ? DBNull.Value : (object)item.DeliveryLocation;
                    objDataRow["DeliveryDate"] = string.IsNullOrEmpty(item.DeliveryDate) ? DBNull.Value : (object)item.DeliveryDate;
                    objDataRow["DeliveryMode"] = string.IsNullOrEmpty(item.DeliveryMode) ? DBNull.Value : (object)item.DeliveryMode;
                    objDataRow["QutnQnty"] = string.IsNullOrEmpty(item.QutnQnty) ? DBNull.Value : (object)item.QutnQnty;
                    objDataRow["QutnUnit"] = string.IsNullOrEmpty(item.QutnUnit) ? DBNull.Value : (object)item.QutnUnit;
                    objDataRow["VenPrice"] = string.IsNullOrEmpty(item.VenPrice) ? DBNull.Value : (object)item.VenPrice;
                    objDataRow["MclPrice"] = string.IsNullOrEmpty(item.MclPrice) ? DBNull.Value : (object)item.MclPrice;
                    objDataRow["QutnAmt"] = string.IsNullOrEmpty(item.QutnAmt) ? DBNull.Value : (object)item.QutnAmt;
                    objDataRow["VatPerc"] = string.IsNullOrEmpty(item.VatPerc) ? DBNull.Value : (object)item.VatPerc;
                    objDataRow["VatAmt"] = string.IsNullOrEmpty(item.VatAmt) ? DBNull.Value : (object)item.VatAmt;
                    objDataRow["TolAmt"] = string.IsNullOrEmpty(item.TolAmt) ? DBNull.Value : (object)item.TolAmt;

                    detailDataTable.Rows.Add(objDataRow);
                }
            }

            // ################# Terms Detail Data ####################

            DataTable TermsDtlTable = new DataTable(); // Renamed
            TermsDtlTable.Columns.Add("TermsID", typeof(string));
            TermsDtlTable.Columns.Add("TermsCode", typeof(string));
            TermsDtlTable.Columns.Add("TermsName", typeof(string));

            if (AllTermsDtl != null)
            {
                foreach (var TermsItem in AllTermsDtl)
                {
                    DataRow TermRow = TermsDtlTable.NewRow();
                    TermRow["TermsID"] = string.IsNullOrEmpty(TermsItem.TermsID) ? DBNull.Value : (object)TermsItem.TermsID;
                    TermRow["TermsCode"] = string.IsNullOrEmpty(TermsItem.TermsCode) ? DBNull.Value : (object)TermsItem.TermsCode;
                    TermRow["TermsName"] = string.IsNullOrEmpty(TermsItem.TermsName) ? DBNull.Value : (object)TermsItem.TermsName;

                    TermsDtlTable.Rows.Add(TermRow); // Fixed table reference
                }
            }

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveClientQuotAprov"))
            {
                db.AddInParameter(dbCommandWrapper, "@OBS_QtC_MasterType", SqlDbType.Structured, masterDataTable);
                db.AddInParameter(dbCommandWrapper, "@OBS_QtC_DetailType", SqlDbType.Structured, detailDataTable);
                db.AddInParameter(dbCommandWrapper, "@OBS_Qtc_TermsDtl", SqlDbType.Structured, TermsDtlTable);
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
