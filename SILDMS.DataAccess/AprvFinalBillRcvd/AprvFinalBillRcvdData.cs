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

namespace SILDMS.DataAccess.AprvFinalBillRcvd
{
    public class AprvFinalBillRcvdData : IAprvFinalBillRcvdData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<VendorBillRecvd> GetPOSearchList()
        {
            string errorNumber = string.Empty;
            List<VendorBillRecvd> vendorBillRecvdList = new List<VendorBillRecvd>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorAprvBillRecvdList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new VendorBillRecvd
                    {
                        VendrFinalBilAprvID = reader.IsNull("VendrFinalBilAprvID") ? 0 : Convert.ToInt32(reader["VendrFinalBilAprvID"]),
                        VendorID = reader.IsNull("VendorID") ? string.Empty : reader.GetString("VendorID"),
                        VendorName = reader.IsNull("VendorName") ? string.Empty : reader.GetString("VendorName"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        NetPayableAmount = reader.GetToDecimal("NetPayableAmount"),
                        PoNo = reader.IsNull("PONo") ? string.Empty : reader.GetString("PONo"),
                        PoAprvDate = reader.IsNull("PoAprvDate") ? string.Empty : reader.GetString("PoAprvDate"),
                        POAmount = reader.GetToDecimal("PoAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        VendorBillNo = reader.IsNull("VendorBillNo") ? string.Empty : reader.GetString("VendorBillNo"),
                        BillReceiveDate = reader.IsNull("BillReceiveDate") ? string.Empty : reader.GetString("BillReceiveDate"),
                        //RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        TotalBillAmount = reader.GetToDecimal("VendorTotalBillAmount"),
                        BillAmount = reader.GetToDecimal("VendorBillAmount"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VendorBillDate = reader.IsNull("VendorBillDate") ? string.Empty : reader.GetString("VendorBillDate"),
                        Note = reader.IsNull("Note") ? string.Empty : reader.GetString("Note"),
                        VendorFinalBilRecmID = reader.IsNull("VendrFinalBilRecmID") ? 0 : Convert.ToInt32(reader["VendrFinalBilRecmID"]),
                        Operation = reader.IsNull("Operation") ? string.Empty : reader.GetString("Operation"),
                        RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        RecommendDate = reader.IsNull("RecommendDate") ? string.Empty : reader.GetString("RecommendDate"),
                        AprvAmnt = reader.GetToDecimal("AprvAmnt"),
                        AprvDate = reader.IsNull("AprvDate") ? string.Empty : reader.GetString("AprvDate"),
                        RAWO = reader.GetToDecimal("RAWO"),

                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public List<VendorBillRecvd> GetShowVendorReqList()
        {
            string errorNumber = string.Empty;
            List<VendorBillRecvd> vendorBillRecvdList = new List<VendorBillRecvd>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetShowAprvPoList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new VendorBillRecvd
                    {
                        VendorFinalBilRecmID = reader.IsNull("VendrFinalBilRecmID") ? 0 : Convert.ToInt32(reader["VendrFinalBilRecmID"]),
                        VendorID = reader.IsNull("VendorID") ? string.Empty : reader.GetString("VendorID"),
                        VendorName = reader.IsNull("VendorName") ? string.Empty : reader.GetString("VendorName"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        NetPayableAmount = reader.GetToDecimal("NetPayableAmount"),
                        PoNo = reader.IsNull("PONo") ? string.Empty : reader.GetString("PONo"),
                        PoAprvDate = reader.IsNull("PoAprvDate") ? string.Empty : reader.GetString("PoAprvDate"),
                        POAmount = reader.GetToDecimal("PoAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        VendorBillNo = reader.IsNull("VendorBillNo") ? string.Empty : reader.GetString("VendorBillNo"),
                        BillReceiveDate = reader.IsNull("BillReceiveDate") ? string.Empty : reader.GetString("BillReceiveDate"),
                        BillAmount = reader.GetToDecimal("VendorBillAmount"),
                        TotalBillAmount = reader.GetToDecimal("VendorTotalBillAmount"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VendorBillDate = reader.IsNull("VendorBillDate") ? string.Empty : reader.GetString("VendorBillDate"),
                        RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        RecommendDate = reader.IsNull("RecommendDate") ? string.Empty : reader.GetString("RecommendDate"),
                        RAWO = reader.GetToDecimal("RAWO"),
                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public string SaveVendorFinalBill(VendorBillRecvd billRecv)
        {
            DataTable dtVendorQutnItem = new DataTable();
            //dtVendorQutnItem.Columns.Add("VendorQutnItemID");

            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                if ((billRecv.VendrFinalBilAprvID) == 0)
                    billRecv.Action = "add";
                else
                    billRecv.Action = "edit";

                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAprvFinalBill"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendrFinalBilAprvID", SqlDbType.Int, billRecv.VendrFinalBilAprvID);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, billRecv.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@PoNo", SqlDbType.NVarChar, billRecv.RequisitionNo);
                    db.AddInParameter(dbCommandWrapper, "@VendorName", SqlDbType.NVarChar, billRecv.VendorName);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, billRecv.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientName", SqlDbType.NVarChar, billRecv.ClientName);
                    //db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.NVarChar, billRecv.TermsID);
                    //db.AddInParameter(dbCommandWrapper, "@FormName", SqlDbType.NVarChar, billRecv.FormName);
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnNo", SqlDbType.NVarChar, (billRecv.VendorQutnNo));
                    db.AddInParameter(dbCommandWrapper, "@PoDate", SqlDbType.NVarChar, billRecv.RequisitionDate);
                    //db.AddInParameter(dbCommandWrapper, "@QuotationDate", SqlDbType.DateTime, billRecv.QuotationDate);
                    //db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(billRecv.Remarks));
                    //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.NVarChar, billRecv.Status);
                    db.AddInParameter(dbCommandWrapper, "@POAmount", SqlDbType.Decimal, billRecv.POAmount);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidAmount", SqlDbType.Decimal, billRecv.AdvancePaidAmount);
                    db.AddInParameter(dbCommandWrapper, "@BillAmount", SqlDbType.Decimal, billRecv.BillAmount);
                    db.AddInParameter(dbCommandWrapper, "@VendorBillNo", SqlDbType.NVarChar, billRecv.VendorBillNo);
                    db.AddInParameter(dbCommandWrapper, "@VendorBillDate", SqlDbType.NVarChar, billRecv.VendorBillDate);
                    db.AddInParameter(dbCommandWrapper, "@BillReceiveDate", SqlDbType.NVarChar, billRecv.BillReceiveDate);
                    db.AddInParameter(dbCommandWrapper, "@VATPercentage", SqlDbType.Decimal, billRecv.VATPercentage);
                    db.AddInParameter(dbCommandWrapper, "@VATAmount", SqlDbType.Decimal, billRecv.VATAmount);
                    db.AddInParameter(dbCommandWrapper, "@CommissionPercentage", SqlDbType.Decimal, billRecv.CommissionPercentage);
                    db.AddInParameter(dbCommandWrapper, "@CommissionAmount", SqlDbType.Decimal, billRecv.CommissionAmount);
                    db.AddInParameter(dbCommandWrapper, "@TotalBillAmount", SqlDbType.Decimal, billRecv.TotalBillAmount);
                    db.AddInParameter(dbCommandWrapper, "@NetPayableAmount", SqlDbType.Decimal, billRecv.NetPayableAmount);
                    db.AddInParameter(dbCommandWrapper, "@VendorFinalBilRecmID", SqlDbType.Int, billRecv.VendorFinalBilRecmID);
                    db.AddInParameter(dbCommandWrapper, "@RecommendedAmnt", SqlDbType.Decimal, billRecv.RecommendedAmnt);
                    db.AddInParameter(dbCommandWrapper, "@RecommendDate", SqlDbType.NVarChar, billRecv.RecommendDate);
                    db.AddInParameter(dbCommandWrapper, "@AprvAmnt", SqlDbType.Decimal, billRecv.AprvAmnt);
                    db.AddInParameter(dbCommandWrapper, "@AprvDate", SqlDbType.NVarChar, billRecv.AprvDate);
                    db.AddInParameter(dbCommandWrapper, "@Operation", SqlDbType.NVarChar, billRecv.Operation);
                    db.AddInParameter(dbCommandWrapper, "@RAWO", SqlDbType.NVarChar, billRecv.RAWO);
                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, billRecv.Note);
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
