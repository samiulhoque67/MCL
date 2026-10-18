using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientSettlement
{
    public class ClientSettlementData : IClientSettlementData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientSettlement> GetClientSettlementList(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientSettlement> list = new List<OBS_ClientSettlement>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientSettlementList"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientSettlement
                    {
                        ClientBillCollectionID = reader.GetString("ClientBillCollectionID"),
                        ClientFinalBillID = reader.GetString("ClientFinalBillID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        WONo = reader.GetString("WONo"),
                        BillDate = reader.GetString("BillDate"),

                        BillItemsAmountWithoutASFVat = reader.GetString("BillItemsAmountWithoutASFVat"),
                        BillItemsASFAmt = reader.GetString("BillItemsASFAmt"),
                        ASFPerc = reader.GetString("ASFPerc"),
                        BillItemsVatAmt = reader.GetString("BillItemsVatAmt"),
                        VatPerc = reader.GetString("VatPerc"),
                        BillItemsAmount = reader.GetString("BillItemsAmount"),
                        AdvanceAdjustedAmt = reader.GetString("AdvanceAdjustedAmt"),

                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        ReceiveDate = reader.GetString("ReceiveDate"),
                        AITAmount = reader.GetString("AITAmount"),
                        NetASFAmount = reader.GetString("NetASFAmount"),
                        VATPaidBy = reader.GetString("VATPaidBy"),
                        CollectionAmount = reader.GetString("CollectionAmount"),

                        AITAmountTotal = reader.GetString("AITAmountTotal"),
                        AITReceived = reader.GetString("AITReceived"),
                        AITDue = reader.GetString("AITDue"),
                        VATAmount = reader.GetString("VATAmount"),
                        MushakReceived = reader.GetString("MushakReceived"),
                        MushakDue = reader.GetString("MushakDue"),
                        EntryCount = reader.GetString("EntryCount"),
                        SettlementStatus = reader.GetString("SettlementStatus"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientBillCollectionItem> GetClientSettlementBillItem(string clientFinalBillID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientBillCollectionItem> list = new List<OBS_ClientBillCollectionItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientBillCollectionItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientFinalBillID", SqlDbType.NVarChar, clientFinalBillID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientBillCollectionItem
                    {
                        ClientFinalBillItemID = reader.GetString("ClientFinalBillItemID"),
                        Description = reader.GetString("Description"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        BillQty = reader.GetString("BillQty"),
                        BillPrice = reader.GetString("BillPrice"),
                        BillAmt = reader.GetString("BillAmt"),
                        BillASFPerc = reader.GetString("BillASFPerc"),
                        BillASFAmt = reader.GetString("BillASFAmt"),
                        BillVatPerc = reader.GetString("BillVatPerc"),
                        BillVatAmt = reader.GetString("BillVatAmt"),
                        BillTotalAmt = reader.GetString("BillTotalAmt"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientSettlementHistoryRow> GetClientSettlementHistory(string clientBillCollectionID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientSettlementHistoryRow> list = new List<OBS_ClientSettlementHistoryRow>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientSettlementHistory"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientBillCollectionID", SqlDbType.NVarChar, clientBillCollectionID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientSettlementHistoryRow
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

        public void GetClientSettlementDocPath(string clientSettlementID, string docType, out string docName, out string docPath)
        {
            docName = string.Empty;
            docPath = string.Empty;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientSettlementDocPath"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientSettlementID", SqlDbType.NVarChar, clientSettlementID);
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

        public string SaveClientSettlementInfo(OBS_ClientSettlement settlement)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetClientSettlementInfo"))
                {
                    db.AddInParameter(cmd, "@ClientBillCollectionID", SqlDbType.NVarChar, settlement.ClientBillCollectionID);

                    db.AddInParameter(cmd, "@AITChallanReceiveAmt", SqlDbType.Decimal, ToDecimal(settlement.AITChallanReceiveAmt));
                    db.AddInParameter(cmd, "@AITChallanNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.AITChallanNo));
                    db.AddInParameter(cmd, "@AITChallanDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(settlement.AITChallanDate));
                    db.AddInParameter(cmd, "@AITDocName", SqlDbType.NVarChar, settlement.AITDocName);
                    db.AddInParameter(cmd, "@AITDocPath", SqlDbType.NVarChar, settlement.AITDocPath);

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
