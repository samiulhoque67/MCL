using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System;
using System.Linq;

namespace SILDMS.DataAccess.AdvanceApproval
{
    public class AdvanceApprovalData : IAdvanceApprovalData
    {
        private readonly string _spStatusParam;

        public AdvanceApprovalData()
        {
            _spStatusParam = "@p_Status";
        }


        public List<POinfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<POinfo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailablePOforAprv"))
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
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        PoNo = reader.GetString("PONo"),
                        PODate = reader.GetString("PODate"),
                        POAmt = reader.GetString("POAmt"),
                        POAprvAmnt = reader.GetString("POAprvAmnt"),
                        AdvncDemnAmt = reader.GetString("AdvncDemnAmt"),
                        POAprvID = reader.GetString("POAprvID"),
                        VendrAdvncDemnID = reader.GetString("VendrAdvncDemnID")
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public List<POinfo> AvailableClientDetailInfoDataService(string ClientID, string VendrAdvncDemnID, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var ClientDetails = new List<POinfo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailablePOforDemandAprv"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", DbType.String, ClientID);
                db.AddInParameter(dbCommandWrapper, "@VendrAdvncDemnID", DbType.String, VendrAdvncDemnID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return ClientDetails;
                    var dt1 = ds.Tables[0];
                    ClientDetails = dt1.AsEnumerable().Select(reader => new POinfo
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        VendorName = reader.GetString("VendorName"),
                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                        VendrAdvncDemnID = reader.GetString("VendrAdvncDemnID"),
                        AdvncDemnAmt = reader.GetString("AdvncDemnAmt"),
                        AdvncDemnAmtPrev = reader.GetString("AdvncDemnAmtPrev"),
                        AdvncDemnDate = reader.GetString("AdvncDemnDate"),
                        AdvncDemnDatePrev = reader.GetString("AdvncDemnDatePrev"),
                        RemainingAmt = reader.GetString("RemainingAmt"),
                        UserFullName = reader.GetString("UserFullName"),
                        RecomUserFullName = reader.GetString("RecomUserFullName"),
                        PoNo = reader.GetString("PoNo"),
                        POAprvID = reader.GetString("POAprvID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorID = reader.GetString("VendorID"),
                        POAprvAmnt = reader.GetString("POAprvAmnt"),
                        WONo = reader.GetString("WONo"),
                        WOAmt = reader.GetString("WOAmt"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        POInstallmentNo = reader.GetString("POInstallmentNo"),
                        POInstallmentID = reader.GetString("POInstallmentID"),
                        POInstallmentAmt = reader.GetString("POInstallmentAmt"),
                        Note = reader.GetString("Note"),
                        RecomNote = reader.GetString("RecomNote"),
                        PrevProposedAmt = reader.GetString("PrevProposedAmt"),
                        PrevRemainingAmt = reader.GetString("PrevRemainingAmt"),
                    }).ToList();

                }
            }

            return ClientDetails;
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
            masterDataTable.Columns.Add("RecomAmount", typeof(string));
            masterDataTable.Columns.Add("RecommendedDate", typeof(string));
            masterDataTable.Columns.Add("Operation", typeof(string));

            if (MasterData != null)
            {
                foreach (var masterItem in MasterData)
                {
                    DataRow masterRow = masterDataTable.NewRow();
                    masterRow["ClientID"] = string.IsNullOrEmpty(masterItem.ClientID) ? DBNull.Value : (object)masterItem.ClientID;
                    masterRow["VendorID"] = string.IsNullOrEmpty(masterItem.VendorID) ? DBNull.Value : (object)masterItem.VendorID;
                    masterRow["VendorQutnID"] = string.IsNullOrEmpty(masterItem.VendorQutnID) ? DBNull.Value : (object)masterItem.VendorQutnID;
                    masterRow["POAprvID"] = string.IsNullOrEmpty(masterItem.MoneyReceiptNo) ? DBNull.Value : (object)masterItem.MoneyReceiptNo;
                    masterRow["PurchaseOrderAmount"] = string.IsNullOrEmpty(masterItem.PurchaseOrderAmount) ? DBNull.Value : (object)masterItem.PurchaseOrderAmount;
                    masterRow["AdvanceInvoiceNo"] = string.IsNullOrEmpty(masterItem.AdvanceInvoiceNo) ? DBNull.Value : (object)masterItem.AdvanceInvoiceNo;
                    masterRow["AdvanceDemandAmount"] = string.IsNullOrEmpty(masterItem.AdvanceDemandAmount) ? DBNull.Value : (object)masterItem.AdvanceDemandAmount;
                    masterRow["AdvanceDemandDate"] = string.IsNullOrEmpty(masterItem.AdvanceDemandDate) ? DBNull.Value : (object)masterItem.AdvanceDemandDate;
                    masterRow["ProposedAmount"] = string.IsNullOrEmpty(masterItem.ProposedAmount) ? DBNull.Value : (object)masterItem.ProposedAmount;
                    masterRow["RemainingAmount"] = string.IsNullOrEmpty(masterItem.RemainingAmount) ? DBNull.Value : (object)masterItem.RemainingAmount;
                    masterRow["Note"] = string.IsNullOrEmpty(masterItem.Note) ? DBNull.Value : (object)masterItem.Note;
                    masterRow["RecomAmount"] = string.IsNullOrEmpty(masterItem.RecommendationAmount) ? DBNull.Value : (object)masterItem.RecommendationAmount;
                    masterRow["RecommendedDate"] = string.IsNullOrEmpty(masterItem.RecommendedDate) ? DBNull.Value : (object)masterItem.RecommendedDate;
                    masterRow["Operation"] = string.IsNullOrEmpty(masterItem.Operation) ? DBNull.Value : (object)masterItem.Operation;

                    masterDataTable.Rows.Add(masterRow);
                }
            }



            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveAdvanceDemandAprv"))
            {
                db.AddInParameter(dbCommandWrapper, "@OBS_AdvanceDemand_MasterType", SqlDbType.Structured, masterDataTable);
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.VarChar, MasterData[0].WOInfoID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, MasterData[0].ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.VarChar, MasterData[0].POAprvID);
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
