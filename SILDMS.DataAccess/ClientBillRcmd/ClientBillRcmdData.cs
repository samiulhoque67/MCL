using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientBillRcmd
{
    public class ClientBillRcmdData : IClientBillRcmdData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<VendorBillRecvd> GetQutnSearchList()
        {
            string errorNumber = string.Empty;
            List<VendorBillRecvd> vendorBillRecvdList = new List<VendorBillRecvd>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientBillRecmList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new VendorBillRecvd
                    {
                        ClientFinalBilRecmID = reader.IsNull("ClientFinalBilRecmID") ? 0 : Convert.ToInt32(reader["ClientFinalBilRecmID"]),
                        ClientFinalBilPreprID = reader.IsNull("ClientFinalBilPreprID") ? 0 : Convert.ToInt32(reader["ClientFinalBilPreprID"]),
                        AdvancRecvID = reader.IsNull("AdvancClaimRcvdID") ? string.Empty : reader.GetString("AdvancClaimRcvdID"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        WOInfoID = reader.IsNull("WOInfoID") ? string.Empty : reader.GetString("WOInfoID"),
                        WONo = reader.IsNull("WOInfoNo") ? string.Empty : reader.GetString("WOInfoNo"),
                        WODate = reader.IsNull("WODate") ? string.Empty : reader.GetString("WODate"),
                        VendorQutnID = reader.IsNull("ClientQuotationID") ? string.Empty : reader.GetString("ClientQuotationID"),
                        RequisitionNo = reader.IsNull("ClientReqID") ? string.Empty : reader.GetString("ClientReqID"),
                        WOAmt = reader.GetToDecimal("WOAmount"),
                        AdvancClaimRcvAmt = reader.GetToDecimal("AdvancReceivedAmt"),
                        AdvancClaimRcvdDate = reader.IsNull("AdvancReceivedDate") ? string.Empty : reader.GetString("AdvancReceivedDate"),
                        VendorBillNo = reader.IsNull("BillRefNoNo") ? string.Empty : reader.GetString("BillRefNoNo"),
                        VendorBillDate = reader.IsNull("BillDate") ? string.Empty : reader.GetString("BillDate"),
                        BillReceiveDate = reader.IsNull("BillSubmitOn") ? string.Empty : reader.GetString("BillSubmitOn"),
                        RemainingAmnt = reader.GetToDecimal("DueAmountToBill"),
                        RecommendedAmnt = reader.GetToDecimal("RecommendationAmt"),
                        RecommendDate = reader.GetString("RecommendationDate"),
                        Operation = reader.GetString("Operation"),

                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        TotalBillAmount = reader.GetToDecimal("TotalBillAmount"),
                        NetPayableAmount = reader.GetToDecimal("NetReceivableAmount"),
                        Note = reader.IsNull("Note") ? string.Empty : reader.GetString("Note"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        QuotatDate = reader.IsNull("QuotationDate") ? string.Empty : reader.GetString("QuotationDate"),
                        RequiDate = reader.IsNull("RequisitionDate") ? string.Empty : reader.GetString("RequisitionDate")
                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public List<VendorBillRecvd> GetShowClientReqList()
        {
            string errorNumber = string.Empty;
            List<VendorBillRecvd> vendorBillRecvdList = new List<VendorBillRecvd>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientRecmWorkOrderList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new VendorBillRecvd
                    {
                        ClientFinalBilPreprID = reader.IsNull("ClientFinalBilPreprID") ? 0 : Convert.ToInt32(reader["ClientFinalBilPreprID"]),
                        AdvancRecvID = reader.IsNull("AdvancClaimRcvdID") ? string.Empty : reader.GetString("AdvancClaimRcvdID"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        WOInfoID = reader.IsNull("WOInfoID") ? string.Empty : reader.GetString("WOInfoID"),
                        WONo = reader.IsNull("WOInfoNo") ? string.Empty : reader.GetString("WOInfoNo"),
                        WODate = reader.IsNull("WODate") ? string.Empty : reader.GetString("WODate"),
                        VendorQutnNo = reader.IsNull("ClientQuotationID") ? string.Empty : reader.GetString("ClientQuotationID"),
                        RequisitionNo = reader.IsNull("ClientReqID") ? string.Empty : reader.GetString("ClientReqID"),
                        WOAmt = reader.GetToDecimal("WOAmount"),
                        AdvancClaimRcvAmt = reader.GetToDecimal("AdvancReceivedAmt"),
                        AdvancClaimRcvdDate = reader.IsNull("AdvancReceivedDate") ? string.Empty : reader.GetString("AdvancReceivedDate"),
                        VendorBillNo = reader.IsNull("BillRefNoNo") ? string.Empty : reader.GetString("BillRefNoNo"),
                        VendorBillDate = reader.IsNull("BillDate") ? string.Empty : reader.GetString("BillDate"),
                        BillReceiveDate = reader.IsNull("BillSubmitOn") ? string.Empty : reader.GetString("BillSubmitOn"),
                        RemainingAmnt = reader.GetToDecimal("DueAmountToBill"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        TotalBillAmount = reader.GetToDecimal("TotalBillAmount"),
                        NetPayableAmount = reader.GetToDecimal("NetReceivableAmount"),
                        Note = reader.IsNull("Note") ? string.Empty : reader.GetString("Note"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        QuotatDate = reader.IsNull("QuotationDate") ? string.Empty : reader.GetString("QuotationDate"),
                        RequiDate = reader.IsNull("RequisitionDate") ? string.Empty : reader.GetString("RequisitionDate")
                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public string SaveClientFinalBill(VendorBillRecvd billRecv)
        {
            DataTable dtVendorQutnItem = new DataTable();
            //dtVendorQutnItem.Columns.Add("VendorQutnItemID");

            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                if ((billRecv.ClientFinalBilRecmID) == 0)
                    billRecv.Action = "add";
                else
                    billRecv.Action = "edit";

                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientFinalBillRecm"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientFinalBilRecmID", SqlDbType.Int, billRecv.ClientFinalBilRecmID);
                    db.AddInParameter(dbCommandWrapper, "@ClientFinalBilPreprID", SqlDbType.Int, billRecv.ClientFinalBilPreprID);
                    db.AddInParameter(dbCommandWrapper, "@AdvancRecvID", SqlDbType.NVarChar, billRecv.AdvancRecvID);
                    //db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, billRecv.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@RequisitionNo", SqlDbType.NVarChar, billRecv.RequisitionNo);
                    //db.AddInParameter(dbCommandWrapper, "@VendorName", SqlDbType.NVarChar, billRecv.VendorName);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, billRecv.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientName", SqlDbType.NVarChar, billRecv.ClientName);
                    //db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.NVarChar, billRecv.TermsID);
                    //db.AddInParameter(dbCommandWrapper, "@FormName", SqlDbType.NVarChar, billRecv.FormName);
                    db.AddInParameter(dbCommandWrapper, "@ClientQutnNo", SqlDbType.NVarChar, (billRecv.VendorQutnNo));
                    db.AddInParameter(dbCommandWrapper, "@RequisitionDate", SqlDbType.NVarChar, billRecv.RequisitionDate);
                    db.AddInParameter(dbCommandWrapper, "@QuotationDate", SqlDbType.DateTime, billRecv.QuotationDate);
                    //db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(billRecv.Remarks));
                    //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.NVarChar, billRecv.Status);
                    db.AddInParameter(dbCommandWrapper, "@WONo", SqlDbType.NVarChar, billRecv.WONo);
                    db.AddInParameter(dbCommandWrapper, "@WODate", SqlDbType.NVarChar, billRecv.WODate);
                    db.AddInParameter(dbCommandWrapper, "@WOAmt", SqlDbType.Decimal, billRecv.WOAmt);
                    db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.Decimal, billRecv.WOInfoID);
                    db.AddInParameter(dbCommandWrapper, "@AdvanceRecvAmount", SqlDbType.Decimal, billRecv.AdvancClaimRcvAmt);
                    db.AddInParameter(dbCommandWrapper, "@AdvancClaimRcvdDate", SqlDbType.NVarChar, billRecv.AdvancClaimRcvdDate);
                    db.AddInParameter(dbCommandWrapper, "@RemainingAmnt", SqlDbType.Decimal, billRecv.RemainingAmnt);
                    db.AddInParameter(dbCommandWrapper, "@RecommendationAmt", SqlDbType.Decimal, billRecv.RecommendedAmnt);
                    db.AddInParameter(dbCommandWrapper, "@VendorBillNo", SqlDbType.NVarChar, billRecv.VendorBillNo);
                    db.AddInParameter(dbCommandWrapper, "@BillDate", SqlDbType.NVarChar, billRecv.VendorBillDate);
                    db.AddInParameter(dbCommandWrapper, "@BillSubmitDate", SqlDbType.NVarChar, billRecv.BillReceiveDate);
                    db.AddInParameter(dbCommandWrapper, "@RecommendationDate", SqlDbType.NVarChar, billRecv.RecommendDate);
                    db.AddInParameter(dbCommandWrapper, "@VATPercentage", SqlDbType.Decimal, billRecv.VATPercentage);
                    db.AddInParameter(dbCommandWrapper, "@VATAmount", SqlDbType.Decimal, billRecv.VATAmount);
                    db.AddInParameter(dbCommandWrapper, "@CommissionPercentage", SqlDbType.Decimal, billRecv.CommissionPercentage);
                    db.AddInParameter(dbCommandWrapper, "@CommissionAmount", SqlDbType.Decimal, billRecv.CommissionAmount);
                    db.AddInParameter(dbCommandWrapper, "@TotalBillAmount", SqlDbType.Decimal, billRecv.TotalBillAmount);
                    db.AddInParameter(dbCommandWrapper, "@NetPayableAmount", SqlDbType.Decimal, billRecv.NetPayableAmount);
                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, billRecv.Note);
                    db.AddInParameter(dbCommandWrapper, "@Operation", SqlDbType.NVarChar, billRecv.Operation);
                    db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.NVarChar, billRecv.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, billRecv.Action);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 10);
                    // Execute SP.
                    db.ExecuteNonQuery(dbCommandWrapper);
                    // Getting output parameters and setting response details.
                    if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                    {
                        // Get the error number, if error occurred.
                        errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                    }
                }
            }
            catch (Exception ex)
            {
                errorNumber = ex.InnerException.Message;// "E404"; // Log ex.Message  Insert Log Table               
            }
            return errorNumber;
        }
    }
}
