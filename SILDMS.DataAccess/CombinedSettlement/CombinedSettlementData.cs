using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace SILDMS.DataAccess.CombinedSettlement
{
    public class CombinedSettlementData : ICombinedSettlementData
    {
        public List<OBS_CombinedSettlement> GetCombinedSettlementList(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_CombinedSettlement> list = new List<OBS_CombinedSettlement>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetCombinedSettlementList"))
            {
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = ds.Tables[0].AsEnumerable().Select(reader => new OBS_CombinedSettlement
                    {
                        WOInfoID = reader.GetString("WOInfoID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientName = reader.GetString("ClientName"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        WONo = reader.GetString("WONo"),

                        WOAmount = reader.GetString("WOAmount"),
                        CollectionAmount = reader.GetString("CollectionAmount"),
                        ClientBillCount = reader.GetString("ClientBillCount"),
                        ClientCollectedCount = reader.GetString("ClientCollectedCount"),
                        ClientSettlementStatus = reader.GetString("ClientSettlementStatus"),

                        PoCount = reader.GetString("PoCount"),
                        PoAmount = reader.GetString("PoAmount"),
                        PayableAmount = reader.GetString("PayableAmount"),
                        VendorBillCount = reader.GetString("VendorBillCount"),
                        VendorPaidCount = reader.GetString("VendorPaidCount"),
                        VendorSettlementStatus = reader.GetString("VendorSettlementStatus"),

                        OverallCode = reader.GetString("OverallCode"),
                        OverallStatus = reader.GetString("OverallStatus"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_CombinedClientBill> GetCombinedClientBills(string woInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_CombinedClientBill> list = new List<OBS_CombinedClientBill>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetCombinedSettlementClientBills"))
            {
                db.AddInParameter(cmd, "@WOInfoID", SqlDbType.NVarChar, woInfoID);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = ds.Tables[0].AsEnumerable().Select(reader => new OBS_CombinedClientBill
                    {
                        ClientFinalBillID = reader.GetString("ClientFinalBillID"),
                        BillDate = reader.GetString("BillDate"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),

                        ClientBillCollectionID = reader.GetString("ClientBillCollectionID"),
                        AITAmount = reader.GetString("AITAmount"),
                        VATPaidBy = reader.GetString("VATPaidBy"),
                        VATAmount = reader.GetString("VATAmount"),
                        CollectionAmount = reader.GetString("CollectionAmount"),
                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        PaymentMode = reader.GetString("PaymentMode"),
                        PaymentRefNo = reader.GetString("PaymentRefNo"),
                        ReceiveDate = reader.GetString("ReceiveDate"),

                        AITReceived = reader.GetString("AITReceived"),
                        AITDue = reader.GetString("AITDue"),
                        MushakReceived = reader.GetString("MushakReceived"),
                        MushakDue = reader.GetString("MushakDue"),
                        EntryCount = reader.GetString("EntryCount"),
                        SettlementStatus = reader.GetString("SettlementStatus"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientSettlementHistoryRow> GetCombinedClientEntries(string woInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientSettlementHistoryRow> list = new List<OBS_ClientSettlementHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetCombinedSettlementClientEntries"))
            {
                db.AddInParameter(cmd, "@WOInfoID", SqlDbType.NVarChar, woInfoID);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = ds.Tables[0].AsEnumerable().Select(reader => new OBS_ClientSettlementHistoryRow
                    {
                        ClientSettlementID = reader.GetString("ClientSettlementID"),
                        ClientBillCollectionID = reader.GetString("ClientBillCollectionID"),
                        AITChallanReceiveAmt = reader.GetString("AITChallanReceiveAmt"),
                        AITDueAmt = reader.GetString("AITDueAmt"),
                        AITChallanNo = reader.GetString("AITChallanNo"),
                        AITChallanDate = reader.GetString("AITChallanDate"),
                        AITDocName = reader.GetString("AITDocName"),
                        HasAITDoc = reader.GetString("HasAITDoc"),
                        MushakReceiveAmt = reader.GetString("MushakReceiveAmt"),
                        MushakDueAmt = reader.GetString("MushakDueAmt"),
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

        public List<OBS_CombinedVendorBill> GetCombinedVendorBills(string clientReqID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_CombinedVendorBill> list = new List<OBS_CombinedVendorBill>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetCombinedSettlementVendorBills"))
            {
                db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientReqID);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = ds.Tables[0].AsEnumerable().Select(reader => new OBS_CombinedVendorBill
                    {
                        VendorFinalBillID = reader.GetString("VendorFinalBillID"),
                        POAprvID = reader.GetString("POAprvID"),
                        PoNo = reader.GetString("PoNo"),
                        VendorName = reader.GetString("VendorName"),
                        POAmount = reader.GetString("POAmount"),
                        BillDate = reader.GetString("BillDate"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),

                        VendorBillPaymentID = reader.GetString("VendorBillPaymentID"),
                        TDSPerc = reader.GetString("TDSPerc"),
                        TDSAmount = reader.GetString("TDSAmount"),
                        VDSPaidBy = reader.GetString("VDSPaidBy"),
                        VDSAmount = reader.GetString("VDSAmount"),
                        PayableAmount = reader.GetString("PayableAmount"),
                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        PaymentMode = reader.GetString("PaymentMode"),
                        PaymentRefNo = reader.GetString("PaymentRefNo"),
                        PaymentDate = reader.GetString("PaymentDate"),

                        TDSReceived = reader.GetString("TDSReceived"),
                        TDSDue = reader.GetString("TDSDue"),
                        MushakReceived = reader.GetString("MushakReceived"),
                        VDSDue = reader.GetString("VDSDue"),
                        EntryCount = reader.GetString("EntryCount"),
                        SettlementStatus = reader.GetString("SettlementStatus"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorSettlementHistoryRow> GetCombinedVendorEntries(string clientReqID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorSettlementHistoryRow> list = new List<OBS_VendorSettlementHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetCombinedSettlementVendorEntries"))
            {
                db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientReqID);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = ds.Tables[0].AsEnumerable().Select(reader => new OBS_VendorSettlementHistoryRow
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
    }
}