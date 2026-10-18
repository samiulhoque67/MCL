using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Globalization;
using System.Linq;

namespace SILDMS.DataAccess.VendorSettlement
{
    public class VendorSettlementData : IVendorSettlementData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorSettlement> GetVendorSettlementList(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorSettlement> list = new List<OBS_VendorSettlement>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorSettlementList"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorSettlement
                    {
                        VendorBillPaymentID = reader.GetString("VendorBillPaymentID"),
                        VendorFinalBillID = reader.GetString("VendorFinalBillID"),
                        POAprvID = reader.GetString("POAprvID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientID = reader.GetString("ClientID"),
                        VendorID = reader.GetString("VendorID"),
                        ClientName = reader.GetString("ClientName"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        VendorName = reader.GetString("VendorName"),
                        PoNo = reader.GetString("PoNo"),
                        BillDate = reader.GetString("BillDate"),

                        BillItemsAmountWithoutVat = reader.GetString("BillItemsAmountWithoutVat"),
                        BillVatAmt = reader.GetString("BillVatAmt"),
                        VatPerc = reader.GetString("VatPerc"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),

                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        PaymentDate = reader.GetString("PaymentDate"),
                        TDSPerc = reader.GetString("TDSPerc"),
                        TDSAmount = reader.GetString("TDSAmount"),
                        VDSPaidBy = reader.GetString("VDSPaidBy"),
                        PayableAmount = reader.GetString("PayableAmount"),

                        TDSAmountTotal = reader.GetString("TDSAmountTotal"),
                        TDSReceived = reader.GetString("TDSReceived"),
                        TDSDue = reader.GetString("TDSDue"),
                        VDSAmountTotal = reader.GetString("VDSAmountTotal"),
                        MushakReceived = reader.GetString("MushakReceived"),
                        VDSDue = reader.GetString("VDSDue"),
                        EntryCount = reader.GetString("EntryCount"),
                        SettlementStatus = reader.GetString("SettlementStatus"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorBillPaymentItem> GetVendorSettlementBillItem(string vendorFinalBillID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorBillPaymentItem> list = new List<OBS_VendorBillPaymentItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorBillPaymentItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorFinalBillID", SqlDbType.NVarChar, vendorFinalBillID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorBillPaymentItem
                    {
                        VendorFinalBillItemID = reader.GetString("VendorFinalBillItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        BillQty = reader.GetString("BillQty"),
                        BillPrice = reader.GetString("BillPrice"),
                        BillAmt = reader.GetString("BillAmt"),
                        BillVatPerc = reader.GetString("BillVatPerc"),
                        BillVatAmt = reader.GetString("BillVatAmt"),
                        BillTotalAmt = reader.GetString("BillTotalAmt"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorSettlementHistoryRow> GetVendorSettlementHistory(string vendorBillPaymentID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorSettlementHistoryRow> list = new List<OBS_VendorSettlementHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorSettlementHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorBillPaymentID", SqlDbType.NVarChar, vendorBillPaymentID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorSettlementHistoryRow
                    {
                        VendorSettlementID = reader.GetString("VendorSettlementID"),
                        VendorBillPaymentID = reader.GetString("VendorBillPaymentID"),
                        TDSChallanReceiveAmt = reader.GetString("TDSChallanReceiveAmt"),
                        TDSDueAmt = reader.GetString("TDSDueAmt"),
                        TDSChallanNo = reader.GetString("TDSChallanNo"),
                        TDSChallanDate = reader.GetString("TDSChallanDate"),
                        TDSDocName = reader.GetString("TDSDocName"),
                        HasTDSDoc = reader.GetString("HasTDSDoc"),
                        MushakReceiveAmt = reader.GetString("MushakReceiveAmt"),
                        VDSDueAmt = reader.GetString("VDSDueAmt"),
                        MushakNo = reader.GetString("MushakNo"),
                        MushakDate = reader.GetString("MushakDate"),
                        MushakDocName = reader.GetString("MushakDocName"),
                        HasMushakDoc = reader.GetString("HasMushakDoc"),
                        SetOn = reader.GetString("SetOn"),
                        SetBy = reader.GetString("SetBy"),
                    }).ToList();
                }
            }
            return list;
        }

        public void GetVendorSettlementDocPath(string vendorSettlementID, string docType, out string docName, out string docPath)
        {
            docName = string.Empty;
            docPath = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorSettlementDocPath"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorSettlementID", SqlDbType.NVarChar, vendorSettlementID);
                db.AddInParameter(dbCommandWrapper, "@DocType", SqlDbType.NVarChar, docType);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow r = ds.Tables[0].Rows[0];
                    docName = r.GetString("DocName");
                    docPath = r.GetString("DocPath");
                }
            }
        }

        public string SaveVendorSettlementInfo(OBS_VendorSettlement settlement)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorSettlementInfo"))
                {
                    db.AddInParameter(cmd, "@VendorBillPaymentID", SqlDbType.NVarChar, settlement.VendorBillPaymentID);

                    db.AddInParameter(cmd, "@TDSChallanReceiveAmt", SqlDbType.Decimal, ToDecimal(settlement.TDSChallanReceiveAmt));
                    db.AddInParameter(cmd, "@TDSChallanNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.TDSChallanNo));
                    db.AddInParameter(cmd, "@TDSChallanDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.TDSChallanDate));
                    db.AddInParameter(cmd, "@TDSDocName", SqlDbType.NVarChar, settlement.TDSDocName);
                    db.AddInParameter(cmd, "@TDSDocPath", SqlDbType.NVarChar, settlement.TDSDocPath);

                    db.AddInParameter(cmd, "@MushakReceiveAmt", SqlDbType.Decimal, ToDecimal(settlement.MushakReceiveAmt));
                    db.AddInParameter(cmd, "@MushakNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.MushakNo));
                    db.AddInParameter(cmd, "@MushakDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.MushakDate));
                    db.AddInParameter(cmd, "@MushakDocName", SqlDbType.NVarChar, settlement.MushakDocName);
                    db.AddInParameter(cmd, "@MushakDocPath", SqlDbType.NVarChar, settlement.MushakDocPath);

                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, settlement.SetBy);

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

        private static decimal ToDecimal(string s)
        {
            decimal d;
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d) ? d : 0m;
        }
    }
}