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

namespace SILDMS.DataAccess.ClientBillCollection
{
    public class ClientBillCollectionData : IClientBillCollectionData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientBillCollection> GetClientBillCollectionList(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientBillCollection> list = new List<OBS_ClientBillCollection>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientBillCollectionList"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientBillCollection
                    {
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
                        NetReceivableAmt = reader.GetString("NetReceivableAmt"),

                        ClientBillCollectionID = reader.GetString("ClientBillCollectionID"),
                        AITPaidBy = reader.GetString("AITPaidBy"),
                        AITAmount = reader.GetString("AITAmount"),
                        NetASFAmount = reader.GetString("NetASFAmount"),
                        VATPaidBy = reader.GetString("VATPaidBy"),
                        VATDeductedAmt = reader.GetString("VATDeductedAmt"),
                        CollectionAmount = reader.GetString("CollectionAmount"),
                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        PaymentMode = reader.GetString("PaymentMode"),
                        PaymentRefNo = reader.GetString("PaymentRefNo"),
                        ReceiveDate = reader.GetString("ReceiveDate"),
                        Note = reader.GetString("Note"),
                        HasSettlement = reader.GetString("HasSettlement"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientBillCollectionItem> GetClientBillCollectionItem(string clientFinalBillID, out string errorNumber)
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

        public string SaveClientBillCollectionInfo(OBS_ClientBillCollection collection)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetClientBillCollectionInfo"))
                {
                    db.AddInParameter(cmd, "@ClientFinalBillID", SqlDbType.NVarChar, collection.ClientFinalBillID);
                    db.AddInParameter(cmd, "@ClientBillCollectionID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(collection.ClientBillCollectionID));
                    db.AddInParameter(cmd, "@AITAmount", SqlDbType.Decimal, ToDecimal(collection.AITAmount));
                    db.AddInParameter(cmd, "@VATPaidBy", SqlDbType.NVarChar, collection.VATPaidBy);
                    db.AddInParameter(cmd, "@MoneyReceiptNo", SqlDbType.NVarChar, collection.MoneyReceiptNo);
                    db.AddInParameter(cmd, "@PaymentMode", SqlDbType.NVarChar, collection.PaymentMode);
                    db.AddInParameter(cmd, "@PaymentRefNo", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(collection.PaymentRefNo));
                    db.AddInParameter(cmd, "@ReceiveDate", SqlDbType.NVarChar, collection.ReceiveDate);
                    db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(collection.Note));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, collection.SetBy);

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
