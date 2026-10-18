using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace SILDMS.DataAccess.VendorFinalBill
{
 
    public class VendorFinalBillData : IVendorFinalBillData
    {

        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorFinalBill> GetVendorFinalBillClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorFinalBill> list = new List<OBS_VendorFinalBill>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorFinalBillClientInfo"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorFinalBill
                    {
                        POAprvID = reader.GetString("POAprvID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        PoNo = reader.GetString("PoNo"),
                        TotalPOAmount = reader.GetString("TotalPOAmount"),
                        PoAprvDate = reader.GetString("PoAprvDate"),
                        BillCategory = reader.GetString("BillCategory"),
                        TotalRemainingQty = reader.GetString("TotalRemainingQty"),
                        BillCount = reader.GetString("BillCount"),
                        TotalBilledAmt = reader.GetString("TotalBilledAmt"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorFinalBillItem> GetVendorFinalBillItem(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorFinalBillItem> list = new List<OBS_VendorFinalBillItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorFinalBillItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorFinalBillItem
                    {
                        POAprvItemID = reader.GetString("POAprvItemID"),
                        PoAprvID = reader.GetString("PoAprvID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServiceCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        POQty = reader.GetString("POQty"),
                        POPrice = reader.GetString("POPrice"),
                        POVatPerc = reader.GetString("POVatPerc"),
                        AlreadyBilledQty = reader.GetString("AlreadyBilledQty"),
                        RemainingQty = reader.GetString("RemainingQty"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorDemandHistoryRow> GetVendorAdvanceDemandHistory(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorDemandHistoryRow> list = new List<OBS_VendorDemandHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorAdvanceDemandHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorDemandHistoryRow
                    {
                        VendorDemandID = reader.GetString("VendorDemandID"),
                        POAprvID = reader.GetString("POAprvID"),
                        TotalPOAmount = reader.GetString("TotalPOAmount"),
                        DemandAmount = reader.GetString("DemandAmount"),
                        RemainingPOAmount = reader.GetString("RemainingPOAmount"),
                        DemandDate = reader.GetString("DemandDate"),
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

        public List<OBS_VendorFinalBillHistoryRow> GetVendorFinalBillHistory(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorFinalBillHistoryRow> list = new List<OBS_VendorFinalBillHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorFinalBillHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorFinalBillHistoryRow
                    {
                        VendorFinalBillID = reader.GetString("VendorFinalBillID"),
                        POAprvID = reader.GetString("POAprvID"),
                        BillItemsAmountWithoutVat = reader.GetString("BillItemsAmountWithoutVat"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        GrandTotalAmt = reader.GetString("GrandTotalAmt"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),
                        NetPayableAmt = reader.GetString("NetPayableAmt"),
                        BillDate = reader.GetString("BillDate"),
                        Remarks = reader.GetString("Remarks"),
                        SetOn = reader.GetString("SetOn"),
                        SetBy = reader.GetString("SetBy"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorCSRecmTerms> GetVendorFinalBillTermList(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> list = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorFinalBillTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        TermsID = reader.GetString("POAprvTermID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                    }).ToList();
                }
            }
            return list;
        }

        public string SaveVendorFinalBillInfo(OBS_VendorFinalBill vendorFinalBill, List<OBS_VendorFinalBillItem> vendorFinalBillItem)
        {
            string errorNumber = string.Empty;

            DataTable itemTable = new DataTable();
            itemTable.Columns.Add("POAprvItemID", typeof(string));
            itemTable.Columns.Add("ServiceItemID", typeof(int));
            itemTable.Columns.Add("BillQty", typeof(decimal));
            itemTable.Columns.Add("BillPrice", typeof(decimal));
            itemTable.Columns.Add("BillVatPerc", typeof(decimal));

            foreach (var item in vendorFinalBillItem)
            {
                DataRow r = itemTable.NewRow();
                r["POAprvItemID"] = item.POAprvItemID;
                r["ServiceItemID"] = string.IsNullOrEmpty(item.ServiceItemID) ? (object)DBNull.Value : int.Parse(item.ServiceItemID);
                r["BillQty"] = item.BillQty;
                r["BillPrice"] = item.BillPrice;
                r["BillVatPerc"] = item.BillVatPerc;
                itemTable.Rows.Add(r);
            }

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorFinalBillInfo"))
                {
                    db.AddInParameter(cmd, "@POAprvID", SqlDbType.NVarChar, vendorFinalBill.POAprvID);
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, vendorFinalBill.ClientReqID);
                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, vendorFinalBill.ClientID);
                    db.AddInParameter(cmd, "@VendorID", SqlDbType.NVarChar, vendorFinalBill.VendorID);
                    db.AddInParameter(cmd, "@BillDate", SqlDbType.NVarChar, vendorFinalBill.BillDate);
                    db.AddInParameter(cmd, "@Remarks", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorFinalBill.Remarks));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, vendorFinalBill.SetBy);
                    db.AddInParameter(cmd, "@VendorFinalBillItemType", SqlDbType.Structured, itemTable);

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