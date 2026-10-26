using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientFinalBill
{
    public class ClientFinalBillData : IClientFinalBillData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientFinalBill> GetClientFinalBillClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientFinalBill> list = new List<OBS_ClientFinalBill>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientFinalBillClientInfo"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientFinalBill
                    {
                        WOInfoID = reader.GetString("WOInfoID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        WONo = reader.GetString("WONo"),
                        TotalWOAmount = reader.GetString("TotalWOAmount"),
                        TotalRemainingQty = reader.GetString("TotalRemainingQty"),
                        BillCount = reader.GetString("BillCount"),
                        TotalBilledAmt = reader.GetString("TotalBilledAmt"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientFinalBillItem> GetClientFinalBillItem(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientFinalBillItem> list = new List<OBS_ClientFinalBillItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientFinalBillItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientFinalBillItem
                    {
                        WOInfoItemID = reader.GetString("WOInfoItemID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServicesCategoryName = reader.GetString("ServicesCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        WOQty = reader.GetString("WOQty"),
                        WOPrice = reader.GetString("WOPrice"),
                        WOASFPerc = reader.GetString("WOASFPerc"),
                        WOVatPerc = reader.GetString("WOVatPerc"),
                        AlreadyBilledQty = reader.GetString("AlreadyBilledQty"),
                        RemainingQty = reader.GetString("RemainingQty"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientAdvanceClaimHistoryRow> GetClientAdvanceClaimHistory(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientAdvanceClaimHistoryRow> list = new List<OBS_ClientAdvanceClaimHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientAdvanceClaimHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientAdvanceClaimHistoryRow
                    {
                        ClientClaimID = reader.GetString("ClientClaimID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        TotalWOAmount = reader.GetString("TotalWOAmount"),
                        ClaimAmount = reader.GetString("ClaimAmount"),
                        RemainingWOAmount = reader.GetString("RemainingWOAmount"),
                        ClaimDate = reader.GetString("ClaimDate"),
                        Remarks = reader.GetString("Remarks"),
                        SetOn = reader.GetString("SetOn"),
                        SetBy = reader.GetString("SetBy"),
                        IsFullPayment = reader.GetString("IsFullPayment"),
                        AdvanceAlreadyAdjusted = reader.GetString("AdvanceAlreadyAdjusted"),
                        AdvanceAvailable = reader.GetString("AdvanceAvailable"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientFinalBillHistoryRow> GetClientFinalBillHistory(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientFinalBillHistoryRow> list = new List<OBS_ClientFinalBillHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientFinalBillHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientFinalBillHistoryRow
                    {
                        ClientFinalBillID = reader.GetString("ClientFinalBillID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        BillItemsAmountWithoutASFVat = reader.GetString("BillItemsAmountWithoutASFVat"),
                        BillItemsASFAmt = reader.GetString("BillItemsASFAmt"),
                        BillItemsVatAmt = reader.GetString("BillItemsVatAmt"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),
                        NetReceivableAmt = reader.GetString("NetReceivableAmt"),
                        BillDate = reader.GetString("BillDate"),
                        Remarks = reader.GetString("Remarks"),
                        SetOn = reader.GetString("SetOn"),
                        SetBy = reader.GetString("SetBy"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorCSRecmTerms> GetClientFinalBillTermList(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> list = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientFinalBillTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        TermsID = reader.GetString("WOInfoTermID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                    }).ToList();
                }
            }
            return list;
        }

        public string SaveClientFinalBillInfo(OBS_ClientFinalBill clientFinalBill, List<OBS_ClientFinalBillItem> clientFinalBillItem)
        {
            string errorNumber = string.Empty;

            DataTable itemTable = new DataTable();
            itemTable.Columns.Add("WOInfoItemID", typeof(string));
            itemTable.Columns.Add("BillQty", typeof(decimal));
            itemTable.Columns.Add("BillPrice", typeof(decimal));
            itemTable.Columns.Add("BillASFPerc", typeof(decimal));
            itemTable.Columns.Add("BillVatPerc", typeof(decimal));

            foreach (var item in clientFinalBillItem)
            {
                DataRow r = itemTable.NewRow();
                r["WOInfoItemID"] = item.WOInfoItemID;
                r["BillQty"] = item.BillQty;
                r["BillPrice"] = item.BillPrice;
                r["BillASFPerc"] = item.BillASFPerc;
                r["BillVatPerc"] = item.BillVatPerc;
                itemTable.Rows.Add(r);
            }

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetClientFinalBillInfo"))
                {
                    db.AddInParameter(cmd, "@WOInfoID", SqlDbType.NVarChar, clientFinalBill.WOInfoID);
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientFinalBill.ClientReqID);
                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, clientFinalBill.ClientID);
                    db.AddInParameter(cmd, "@BillDate", SqlDbType.NVarChar, clientFinalBill.BillDate);
                    db.AddInParameter(cmd, "@Remarks", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(clientFinalBill.Remarks));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, clientFinalBill.SetBy);
                    db.AddInParameter(cmd, "@ClientFinalBillItemType", SqlDbType.Structured, itemTable);

                    db.AddOutParameter(cmd, spStatusParam, SqlDbType.VarChar, 10);
                    db.ExecuteNonQuery(cmd);

                    if (!db.GetParameterValue(cmd, spStatusParam).IsNullOrZero())
                        errorNumber = db.GetParameterValue(cmd, spStatusParam).PrefixErrorCode();
                }
            }
            catch (Exception ex)
            {
                errorNumber = ex.InnerException?.Message ?? ex.Message;
            }

            return errorNumber;
        }
    }
}
