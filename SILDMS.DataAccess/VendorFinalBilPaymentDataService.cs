using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.DataAccessInterface;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace SILDMS.DataAccess
{
    public class VendorFinalBilPaymentDataService : IVendorFinalBilPaymentDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorFinalBilPayment> GetPOSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorFinalBilPayment> vendorBillRecvdList = new List<OBS_VendorFinalBilPayment>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorFinalBillPaymentList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new OBS_VendorFinalBilPayment
                    {
                        VendorFinalBilPaymentID = reader.GetInt64("VendorFinalBilPaymentID"),
                  
                        VendrFinalBilAprvID = reader.IsNull("VendrFinalBilAprvID") ? 0 : Convert.ToInt32(reader["VendrFinalBilAprvID"]),
                        VendorFinalBilRecmID = reader.IsNull("VendrFinalBilRecmID") ? 0 : Convert.ToInt32(reader["VendrFinalBilRecmID"]),
                        VendrFinalBilRcvdID = reader.IsNull("VendrFinalBilRcvdID") ? 0 : Convert.ToInt32(reader["VendrFinalBilRcvdID"]),
                        VendorID = reader.IsNull("VendorID") ? string.Empty : reader.GetString("VendorID"),
                        VendorName = reader.IsNull("VendorName") ? string.Empty : reader.GetString("VendorName"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        NetActualPayableAmount = reader.GetToDecimal("NetPayableAmount"),
                        PoNo = reader.IsNull("PONo") ? string.Empty : reader.GetString("PONo"),
                        PoAprvDate = reader.IsNull("PoAprvDate") ? string.Empty : reader.GetString("PoAprvDate"),
                        POAmount = reader.GetToDecimal("PoAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionPercentage1 = reader.GetToDecimal("ActualCommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        CommissionAmount1 = reader.GetToDecimal("ActualCommissionAmount"),
                        VendorBillNo = reader.IsNull("VendorBillNo") ? string.Empty : reader.GetString("VendorBillNo"),
                        BillReceiveDate = reader.IsNull("BillReceiveDate") ? string.Empty : reader.GetString("BillReceiveDate"),
                        //RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        TotalBillAmount = reader.GetToDecimal("VendorTotalBillAmount"),
                        NetPayableAmount = reader.GetToDecimal("NetAmount"),
                        PaymentAmount = reader.GetToDecimal("PaymentAmount"),
                        BillAmount = reader.GetToDecimal("VendorBillAmount"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VendorBillDate = reader.IsNull("VendorBillDate") ? string.Empty : reader.GetString("VendorBillDate"),
                        Note = reader.IsNull("Note") ? string.Empty : reader.GetString("Note"),
                        Operation = reader.IsNull("Operation") ? string.Empty : reader.GetString("Operation"),
                        RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        RecommendDate = reader.IsNull("RecommendDate") ? string.Empty : reader.GetString("RecommendDate"),
                        AprvAmnt = reader.GetToDecimal("AprvAmnt"),
                        AprvDate = reader.IsNull("AprvDate") ? string.Empty : reader.GetString("AprvDate"),
                        RAWO = reader.GetToDecimal("RAWO"),
                        AdvancePaidAmount = reader.GetToDecimal("AdvncPaidAmt"),
                        WOAmt = reader.GetToDecimal("WOAmount"),
                        AdvancePaidDate = reader.IsNull("AdvncPaidDate") ? string.Empty : reader.GetString("AdvncPaidDate"),
                        WONo = reader.IsNull("WOInfoNo") ? string.Empty : reader.GetString("WOInfoNo"),
                        AdvancePaidID = reader.IsNull("VendrAdvncPaymntID") ? string.Empty : reader.GetString("VendrAdvncPaymntID"),
                        POInstallmentNo = reader.GetInt32("POInstallmentNo"),
                        POInstallmentID = reader.GetInt32("POInstallmentID"),
                        POInstallmentAmt = reader.GetDouble("POInstallmentAmt"),
                        BillType = reader.GetString("BillType"),
                        BillCategory = reader.GetString("BillCategory"),
                        WOInstallmentID=reader.GetInt64("WOInstallmentID"),
                        WOInstallmentNo=reader.GetInt32("WOInstallmentNo"),
                        WOInstallmentAmt=reader.GetInt32("WOInstallmentAmt"),
                        WOInfoID=reader.GetString("WOInfoID"),
                        MoneyRecNo=reader.GetString("MoneyRecNo"),
                        ChequeEftNo=reader.GetString("ChequeEftNo"),
                        PaymentMode=reader.GetString("PaymentMode"),
                        PayDate=reader.GetString("PayDate"),
                        TDSPercentage=reader.GetToDecimal("TDSPercnt"),
                        TDSAmount = reader.GetToDecimal("TDSAmount"),
                       
                        TDS = reader.GetString("TDS"),
                        VDS = reader.GetString("VDS"),
                        ContractNo = reader.GetString("ContractNo"),
                        ContractDate = reader.GetString("ContractDate"),






                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public List<OBS_VendorFinalBilPayment> GetShowVendorReqList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorFinalBilPayment> vendorBillRecvdList = new List<OBS_VendorFinalBilPayment>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetShowVendorFinalBilPaymentList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new OBS_VendorFinalBilPayment
                    {
                       VendrFinalBilAprvID  =  reader.GetInt32("VendrFinalBilAprvID"),
                        VendorFinalBilRecmID =  reader.GetInt32("VendrFinalBilRecmID"),
                        VendrFinalBilRcvdID =reader.GetInt32("VendrFinalBilRcvdID"),
                        VendorID = reader.IsNull("VendorID") ? string.Empty : reader.GetString("VendorID"),
                        VendorName = reader.IsNull("VendorName") ? string.Empty : reader.GetString("VendorName"),
                        ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName"),
                        NetPayableAmount = reader.GetToDecimal("NetPayableAmount"),
                        PoNo = reader.IsNull("PONo") ? string.Empty : reader.GetString("PONo"),
                        PoAprvDate = reader.IsNull("PoAprvDate") ? string.Empty : reader.GetString("PoAprvDate"),
                        POAmount = reader.GetToDecimal("PoAmount"),
                        CommissionPercentage = reader.GetToDecimal("CommissionPercnt"),
                        CommissionPercentage1 = reader.GetToDecimal("ActualCommissionPercnt"),
                        CommissionAmount = reader.GetToDecimal("CommissionAmount"),
                        CommissionAmount1 = reader.GetToDecimal("ActualCommissionAmount"),
                        VendorBillNo = reader.IsNull("VendorBillNo") ? string.Empty : reader.GetString("VendorBillNo"),
                        BillReceiveDate = reader.IsNull("BillReceiveDate") ? string.Empty : reader.GetString("BillReceiveDate"),
                        BillAmount = reader.GetToDecimal("VendorBillAmount"),
                        TotalBillAmount = reader.GetToDecimal("VendorTotalBillAmount"),
                        VATAmount = reader.GetToDecimal("VatAmount"),
                        VATPercentage = reader.GetToDecimal("VatPercnt"),
                        VendorBillDate = reader.IsNull("VendorBillDate") ? string.Empty : reader.GetString("VendorBillDate"),
                        RecommendedAmnt = reader.GetToDecimal("RecommendedAmnt"),
                        RecommendDate = reader.IsNull("RecommendDate") ? string.Empty : reader.GetString("RecommendDate"),
                        AprvAmnt = reader.GetToDecimal("AprvAmnt"),
                        AprvDate = reader.IsNull("AprvDate") ? string.Empty : reader.GetString("AprvDate"),
                        RAWO = reader.GetToDecimal("RAWO"),
                        AdvancePaidAmount = reader.GetToDecimal("AdvncPaidAmt"),
                        WOAmt = reader.GetToDecimal("WOAmount"),
                        AdvancePaidDate = reader.IsNull("AdvncPaidDate") ? string.Empty : reader.GetString("AdvncPaidDate"),
                        WONo = reader.IsNull("WOInfoNo") ? string.Empty : reader.GetString("WOInfoNo"),
                        WOInfoID = reader.IsNull("WOInfoID") ? string.Empty : reader.GetString("WOInfoID"),
                        AdvancePaidID = reader.IsNull("VendrAdvncPaymntID") ? string.Empty : reader.GetString("VendrAdvncPaymntID"),
                        POInstallmentNo = reader.GetInt32("POInstallmentNo"),
                        POInstallmentID = reader.GetInt32("POInstallmentID"),
                        POInstallmentAmt = reader.GetDouble("POInstallmentAmt"),
                        WOInstallmentNo = reader.GetInt32("WOInstallmentNo"),
                        WOInstallmentID = reader.GetInt64("WOInstallmentID"),
                        WOInstallmentAmt = reader.GetDouble("WOInstallmentAmt"),
                        BillType = reader.GetString("BillType"),
                        BillCategory = reader.GetString("BillCategory"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ContractNo = reader.GetString("ContractNo"),
                        ContractDate = reader.GetString("ContractDate"),



                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }

        public string SaveVendorFinalBill(OBS_VendorFinalBilPayment billRecv)
        {
            DataTable dtVendorQutnItem = new DataTable();
            //dtVendorQutnItem.Columns.Add("VendorQutnItemID");

            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                if ((billRecv.VendorFinalBilPaymentID) == 0)
                    billRecv.Action = "add";
                else
                    billRecv.Action = "edit";

                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorFinalBillPayment"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorFinalBilPaymentID", SqlDbType.BigInt, billRecv.VendorFinalBilPaymentID);
                    db.AddInParameter(dbCommandWrapper, "@VendrFinalBilAprvID", SqlDbType.Int, billRecv.VendrFinalBilAprvID);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, billRecv.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, billRecv.ClientID);

                    db.AddInParameter(dbCommandWrapper, "@WONo", SqlDbType.NVarChar, billRecv.WONo);
                    db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, billRecv.WOInfoID);
                    db.AddInParameter(dbCommandWrapper, "@WOAmt", SqlDbType.Decimal, billRecv.WOAmt);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidID", SqlDbType.Decimal, billRecv.AdvancePaidID);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidDate", SqlDbType.NVarChar, billRecv.AdvancePaidDate);

                    db.AddInParameter(dbCommandWrapper, "@PoNo", SqlDbType.NVarChar, billRecv.RequisitionNo);
                    db.AddInParameter(dbCommandWrapper, "@PoDate", SqlDbType.NVarChar, billRecv.RequisitionDate);
                    db.AddInParameter(dbCommandWrapper, "@POAmount", SqlDbType.Decimal, billRecv.POAmount);

                    db.AddInParameter(dbCommandWrapper, "@RecommendedAmnt", SqlDbType.Decimal, billRecv.RecommendedAmnt);
                    db.AddInParameter(dbCommandWrapper, "@RecommendDate", SqlDbType.NVarChar, billRecv.RecommendDate);
                    db.AddInParameter(dbCommandWrapper, "@AdvancePaidAmount", SqlDbType.Decimal, billRecv.AdvancePaidAmount);
                    db.AddInParameter(dbCommandWrapper, "@BillAmount", SqlDbType.Decimal, billRecv.BillAmount);

                    db.AddInParameter(dbCommandWrapper, "@AprvAmnt", SqlDbType.Decimal, billRecv.AprvAmnt);
                    db.AddInParameter(dbCommandWrapper, "@AprvDate", SqlDbType.NVarChar, billRecv.AprvDate);

                    db.AddInParameter(dbCommandWrapper, "@VendorBillNo", SqlDbType.NVarChar, billRecv.VendorBillNo);
                    db.AddInParameter(dbCommandWrapper, "@VendorBillDate", SqlDbType.NVarChar, billRecv.VendorBillDate);
                    db.AddInParameter(dbCommandWrapper, "@BillReceiveDate", SqlDbType.NVarChar, billRecv.BillReceiveDate);

                    db.AddInParameter(dbCommandWrapper, "@VATPercentage", SqlDbType.Decimal, billRecv.VATPercentage);
                    db.AddInParameter(dbCommandWrapper, "@VATAmount", SqlDbType.Decimal, billRecv.VATAmount);
                    db.AddInParameter(dbCommandWrapper, "@CommissionPercentage", SqlDbType.Decimal, billRecv.CommissionPercentage);
                    db.AddInParameter(dbCommandWrapper, "@CommissionPercentage1", SqlDbType.Decimal, billRecv.CommissionPercentage1);
                    db.AddInParameter(dbCommandWrapper, "@CommissionAmount", SqlDbType.Decimal, billRecv.CommissionAmount);
                    db.AddInParameter(dbCommandWrapper, "@CommissionAmount1", SqlDbType.Decimal, billRecv.CommissionAmount1);
                    db.AddInParameter(dbCommandWrapper, "@MoneyRecNo", SqlDbType.NVarChar, billRecv.MoneyRecNo);
                    db.AddInParameter(dbCommandWrapper, "@ChequeEftNo", SqlDbType.NVarChar, billRecv.ChequeEftNo);
                    db.AddInParameter(dbCommandWrapper, "@PaymentMode", SqlDbType.NVarChar, billRecv.PaymentMode);
                    db.AddInParameter(dbCommandWrapper, "@PayDate", SqlDbType.NVarChar, billRecv.PayDate);
                    db.AddInParameter(dbCommandWrapper, "@PaymentID", SqlDbType.NVarChar, billRecv.PaymentID);
                    db.AddInParameter(dbCommandWrapper, "@BillType", SqlDbType.NVarChar, billRecv.BillType);
                    db.AddInParameter(dbCommandWrapper, "@BillCategory", SqlDbType.NVarChar, billRecv.BillCategory);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentNo", SqlDbType.Int, billRecv.POInstallmentNo);
                    db.AddInParameter(dbCommandWrapper, "@WOInstallmentNo", SqlDbType.Int, billRecv.WOInstallmentNo);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentID", SqlDbType.Int, billRecv.POInstallmentID);
                    db.AddInParameter(dbCommandWrapper, "@WOInstallmentID", SqlDbType.BigInt, billRecv.WOInstallmentID);
                    db.AddInParameter(dbCommandWrapper, "@POInstallmentAmt", SqlDbType.Decimal, billRecv.POInstallmentAmt);
                    db.AddInParameter(dbCommandWrapper, "@WOInstallmentAmt", SqlDbType.Decimal, billRecv.WOInstallmentAmt);
             

                    db.AddInParameter(dbCommandWrapper, "@TDSPercentage", SqlDbType.Decimal, billRecv.TDSPercentage);
                    db.AddInParameter(dbCommandWrapper, "@TDSAmount", SqlDbType.Decimal, billRecv.TDSAmount);
                    db.AddInParameter(dbCommandWrapper, "@TDS", SqlDbType.NVarChar, billRecv.TDS);
                    db.AddInParameter(dbCommandWrapper, "@VDS", SqlDbType.NVarChar, billRecv.VDS);
                
                    //db.AddInParameter(dbCommandWrapper, "@VDSPercentage", SqlDbType.Decimal, billRecv.VDSPercentage);
                    //db.AddInParameter(dbCommandWrapper, "@VDSAmount", SqlDbType.Decimal, billRecv.VDSAmount);

                    db.AddInParameter(dbCommandWrapper, "@TotalBillAmount", SqlDbType.Decimal, billRecv.TotalBillAmount);
                    db.AddInParameter(dbCommandWrapper, "@PaymentAmount", SqlDbType.Decimal, billRecv.PaymentAmount);
                    db.AddInParameter(dbCommandWrapper, "@NetPayableAmount", SqlDbType.Decimal, billRecv.NetPayableAmount);
                    db.AddInParameter(dbCommandWrapper, "@NetActualPayableAmount", SqlDbType.Decimal, billRecv.NetActualPayableAmount);

                    //db.AddInParameter(dbCommandWrapper, "@VendorFinalBilRecmID", SqlDbType.Int, billRecv.VendorFinalBilRecmID);

                    db.AddInParameter(dbCommandWrapper, "@Operation", SqlDbType.NVarChar, billRecv.Operation);
                    db.AddInParameter(dbCommandWrapper, "@RAWO", SqlDbType.NVarChar, billRecv.RAWO);
                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, billRecv.Note);

                    db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.NVarChar, billRecv.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, billRecv.Action);
                    db.AddInParameter(dbCommandWrapper, "@ContractNo", SqlDbType.NVarChar, billRecv.ContractNo);
                    db.AddInParameter(dbCommandWrapper, "@ContractDate", SqlDbType.NVarChar, billRecv.ContractDate);
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