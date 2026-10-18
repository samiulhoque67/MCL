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

namespace SILDMS.DataAccess.VendorBillPayment
{
    public class VendorBillPaymentData : IVendorBillPaymentData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorBillPayment> GetVendorBillPaymentList(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorBillPayment> list = new List<OBS_VendorBillPayment>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorBillPaymentList"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorBillPayment
                    {
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
                        NetPayableAmt = reader.GetString("NetPayableAmt"),

                        VendorBillPaymentID = reader.GetString("VendorBillPaymentID"),
                        TDSPaidBy = reader.GetString("TDSPaidBy"),
                        TDSPerc = reader.GetString("TDSPerc"),
                        TDSAmount = reader.GetString("TDSAmount"),
                        VDSPaidBy = reader.GetString("VDSPaidBy"),
                        VDSAmount = reader.GetString("VDSAmount"),
                        PayableAmount = reader.GetString("PayableAmount"),
                        MoneyReceiptNo = reader.GetString("MoneyReceiptNo"),
                        PaymentMode = reader.GetString("PaymentMode"),
                        PaymentRefNo = reader.GetString("PaymentRefNo"),
                        PaymentDate = reader.GetString("PaymentDate"),
                        Note = reader.GetString("Note"),
                        HasSettlement = reader.GetString("HasSettlement"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorBillPaymentItem> GetVendorBillPaymentItem(string vendorFinalBillID, out string errorNumber)
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

        public string SaveVendorBillPaymentInfo(OBS_VendorBillPayment payment)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorBillPaymentInfo"))
                {
                    db.AddInParameter(cmd, "@VendorFinalBillID", SqlDbType.NVarChar, payment.VendorFinalBillID);
                    db.AddInParameter(cmd, "@VendorBillPaymentID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(payment.VendorBillPaymentID));
                    db.AddInParameter(cmd, "@TDSPerc", SqlDbType.Decimal, ToDecimal(payment.TDSPerc));
                    db.AddInParameter(cmd, "@VDSPaidBy", SqlDbType.NVarChar, payment.VDSPaidBy);
                    db.AddInParameter(cmd, "@MoneyReceiptNo", SqlDbType.NVarChar, payment.MoneyReceiptNo);
                    db.AddInParameter(cmd, "@PaymentMode", SqlDbType.NVarChar, payment.PaymentMode);
                    db.AddInParameter(cmd, "@PaymentRefNo", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(payment.PaymentRefNo));
                    db.AddInParameter(cmd, "@PaymentDate", SqlDbType.NVarChar, payment.PaymentDate);
                    db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(payment.Note));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, payment.SetBy);

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
