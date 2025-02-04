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
using SILDMS.Utillity;

namespace SILDMS.DataAccess.VendorFinalBillReceived
{
    public class VendorFinalBillReceivedData : IVendorFinalBillReceivedData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<VendorBillRecvd> GetPOSearchList()
        {
            string errorNumber = string.Empty;
            List<VendorBillRecvd> vendorBillRecvdList = new List<VendorBillRecvd>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorBillRecvdList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new VendorBillRecvd
                    {
                        VendrFinalBilRcvdID = reader.IsNull("VendrFinalBilRcvdID") ? 0 : Convert.ToInt32(reader["VendrFinalBilRcvdID"]),
                        VendorID = reader.IsNull("VendorID") ? string.Empty : reader.GetString("VendorID"),
                        VendorName = reader.IsNull("VendorName") ? string.Empty : reader.GetString("VendorName"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        NetPayableAmount =  reader.GetToDecimal("NetPayableAmount"),
                        PoNo = reader.IsNull("PONo") ? string.Empty : reader.GetString("PONo"),
                        PoAprvDate = reader.IsNull("PoAprvDate") ? string.Empty : reader.GetString("PoAprvDate"),
                        POAmount = reader.GetToDecimal("PoAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        VendorBillNo = reader.IsNull("VendorBillNo") ? string.Empty : reader.GetString("VendorBillNo"),
                        BillReceiveDate = reader.IsNull("BillReceiveDate") ? string.Empty : reader.GetString("BillReceiveDate"),
                        AdvancePaidAmount = reader.GetToDecimal("AdvncPaidAmt"),
                        WOAmt = reader.GetToDecimal("WOAmount"),
                        AdvancePaidDate = reader.IsNull("AdvncPaidDate") ? string.Empty : reader.GetString("AdvncPaidDate"),
                        WONo = reader.IsNull("WONo") ? string.Empty : reader.GetString("WONo"),
                  
                        BillAmount = reader.GetToDecimal("VendorBillAmount"),
                        TotalBillAmount = reader.GetToDecimal("VendorTotalBillAmount"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                 
                        VendorBillDate = reader.IsNull("VendorBillDate") ? string.Empty : reader.GetString("VendorBillDate"),
                        Note= reader.IsNull("Note") ? string.Empty : reader.GetString("Note")
                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public List<OBS_VendorQutn> GetShowVendorReqList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutn> ClientInfoList = new List<OBS_VendorQutn>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetShowPoList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutn
                    {
                        ClientID = reader.GetString("ClientID"),
                        VendorID = reader.GetString("VendorID"),
                        ClientName = reader.GetString("ClientName"),
                        VendorName = reader.GetString("VendorName"),
                        PoNo = reader.GetString("PoNo"),
                        POAmount = reader.GetString("AprvAmnt"),
                        PoAprvDate = reader.GetString("PoAprvDate"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WONo = reader.GetString("WONo"),
                        WOAmt = reader.GetToDecimal("WOAmt"),
                        RemainingAmnt = reader.GetToDecimal("RemainingAmt"),
                        AdvancClaimRcvAmt = reader.GetToDecimal("VendorAdvncAprvAmt"),
                        AdvancRecvID = reader.GetString("VendorAdvancAprvID"),
                        AdvancClaimRcvdDate = reader.GetString("VendorAdvncAprvDate"),
                        POInstallmentNo= reader.GetInt32("POInstallmentNo"),
                        POInstallmentID= reader.GetInt32("POInstallmentID"),
                        POInstallmentAmt=reader.GetDouble("POInstallmentAmt"),
                        BillType = reader.GetString("BillType"),
                        BillCategory = reader.GetString("BillCategory"),
                        //VendorQutnID = reader.GetString("VendorQutnID")



                    }).ToList();
                }
            }
            return ClientInfoList;
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

                if ((billRecv.VendrFinalBilRcvdID)==0)
                    billRecv.Action = "add";
                else
                    billRecv.Action = "edit";

                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorFinalBill"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendrFinalBilRcvdID", SqlDbType.Int, billRecv.VendrFinalBilRcvdID);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, billRecv.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@PoNo", SqlDbType.NVarChar, billRecv.RequisitionNo);
                    //db.AddInParameter(dbCommandWrapper, "@VendorName", SqlDbType.NVarChar, billRecv.VendorName);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, billRecv.ClientID);
                    //db.AddInParameter(dbCommandWrapper, "@ClientName", SqlDbType.NVarChar, billRecv.ClientName);
                    db.AddInParameter(dbCommandWrapper, "@WONo", SqlDbType.NVarChar, billRecv.WONo);
                    db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, billRecv.WOInfoID);
                    db.AddInParameter(dbCommandWrapper, "@WOAmt", SqlDbType.Decimal, billRecv.WOAmt);
                    //db.AddInParameter(dbCommandWrapper, "@VendorQutnNo", SqlDbType.NVarChar, (billRecv.VendorQutnNo));
                    db.AddInParameter(dbCommandWrapper, "@PoDate", SqlDbType.NVarChar, billRecv.RequisitionDate);
                    //db.AddInParameter(dbCommandWrapper, "@QuotationDate", SqlDbType.DateTime, billRecv.QuotationDate);
                    //db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(billRecv.Remarks));
                    //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.NVarChar, billRecv.Status);
                    db.AddInParameter(dbCommandWrapper, "@POAmount", SqlDbType.Decimal, billRecv.POAmount);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentNo", SqlDbType.Int, billRecv.POInstallmentNo);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentID", SqlDbType.Int, billRecv.POInstallmentID);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentAmt", SqlDbType.Decimal, billRecv.POInstallmentAmt);
                    db.AddInParameter(dbCommandWrapper, "@BillType", SqlDbType.NVarChar, billRecv.BillType);
                    db.AddInParameter(dbCommandWrapper, "@BillCategory", SqlDbType.NVarChar, billRecv.BillCategory);
                    db.AddInParameter(dbCommandWrapper, "@POAmount", SqlDbType.Decimal, billRecv.POAmount);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidAmount", SqlDbType.Decimal, billRecv.AdvancePaidAmount);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidID", SqlDbType.NVarChar, billRecv.AdvancePaidID);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidDate", SqlDbType.NVarChar, billRecv.AdvancePaidDate);
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
                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, billRecv.Note);
                    db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.NVarChar, billRecv.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, billRecv.Action);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 25);
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
