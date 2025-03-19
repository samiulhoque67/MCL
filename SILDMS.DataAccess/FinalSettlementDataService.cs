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
using System.Reflection;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace SILDMS.DataAccess
{
    public class FinalSettlementDataService : IFinalSettlementDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_FinalSettlement> GetfinalSettlementSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_FinalSettlement> vendorBillRecvdList = new List<OBS_FinalSettlement>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientBillRecvdList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    vendorBillRecvdList = dt1.AsEnumerable().Select(reader => new OBS_FinalSettlement
                    {
                        //ClientFinalBilPreprID = reader.IsNull("ClientFinalBilPreprID") ? 0 : Convert.ToInt32(reader["ClientFinalBilPreprID"]),
                        //AdvancRecvID = reader.IsNull("AdvancClaimRcvdID") ? string.Empty : reader.GetString("AdvancClaimRcvdID"),
                        //ClientID = reader.IsNull("ClientID") ? string.Empty : reader.GetString("ClientID"),
                        //WOInfoID = reader.IsNull("WOInfoID") ? string.Empty : reader.GetString("WOInfoID"),
                        //WONo = reader.IsNull("WOInfoNo") ? string.Empty : reader.GetString("WOInfoNo"),
                        //WODate = reader.IsNull("WODate") ? string.Empty : reader.GetString("WODate"),
                        //VendorQutnID = reader.IsNull("ClientQuotationID") ? string.Empty : reader.GetString("ClientQuotationID"),
                        ////RequisitionNo = reader.IsNull("ClientReqID") ? string.Empty : reader.GetString("ClientReqID"),
                        //WOAmt = reader.GetString("WOAmount"),
                        //AdvancClaimRcvAmt = reader.GetString("AdvancReceivedAmt"),
                        //AdvancClaimRcvdDate = reader.IsNull("AdvancReceivedDate") ? string.Empty : reader.GetString("AdvancReceivedDate"),
                        //VendorBillNo = reader.IsNull("BillRefNoNo") ? string.Empty : reader.GetString("BillRefNoNo"),
                        //VendorBillDate = reader.IsNull("BillDate") ? string.Empty : reader.GetString("BillDate"),
                        //BillReceiveDate = reader.IsNull("BillSubmitOn") ? string.Empty : reader.GetString("BillSubmitOn"),
                        //RemainingAmnt = reader.GetString("DueAmountToBill"),
                        //VATPercentage = reader.GetString("VatPercnt"),
                        //VATAmount = reader.GetString("VatAmount"),
                        //CommissionPercentage = reader.GetString("CommissionPercnt"),
                        //CommissionAmount = reader.GetString("CommissionAmount"),
                        //TotalBillAmount = reader.GetString("TotalBillAmount"),
                        //NetPayableAmount = reader.GetString("NetReceivableAmount"),
                        //Note = reader.IsNull("Note") ? string.Empty : reader.GetString("Note"),
                        ClientName = reader.IsNull("ClientName") ? string.Empty : reader.GetString("ClientName")
                        //,
                        //QuotatDate = reader.IsNull("QuotationDate") ? string.Empty : reader.GetString("QuotationDate"),
                        //RequiDate = reader.IsNull("RequisitionDate") ? string.Empty : reader.GetString("RequisitionDate")
                    }).ToList();
                }
            }
            return vendorBillRecvdList;
        }


        public List<OBS_FinalSettlement> GetShowfinalSettlementList()
        {
            string errorNumber = string.Empty;
            List<OBS_FinalSettlement> ClientInfoList = new List<OBS_FinalSettlement>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetFinalSettlementList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_FinalSettlement
                    {
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        WONo = reader.GetString("WONo"),
                        WOAmt = reader.GetString("WOAmt"),

                        ClaimReceivedAmt = reader.GetString("ClaimReceivedAmt"),
                        ClientBillAmount = reader.GetString("ClientBillAmount"),
                        CiCommissionAmount = reader.GetString("CiCommissionAmount"),
                        CiBaseAmount = reader.GetString("CiBaseAmount"),
                        AITAmount = reader.GetString("AITAmount"),
                        VatAmount = reader.GetString("VatAmount"),
                        CiReceivedAmount = reader.GetString("CiReceivedAmount"),

                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),

                        POInfoID = reader.GetString("POInfoID"),
                        PONo = reader.GetString("PONo"),
                        POAmt = reader.GetString("POAmt"),
                        VenAdvncPaidAmt = reader.GetString("AdvncPaidAmt"),
                        VendorBillAmount = reader.GetString("VendorBillAmount"),
                        VenCommissionAmount = reader.GetString("VenCommissionAmount"),
                        VenBaseAmount = reader.GetString("VenBaseAmount"),
                        VenVatAmount = reader.GetString("VenVatAmount"),
                        VenTDSAmount = reader.GetString("VenTDSAmount"),
                        VenNetPayableAmount = reader.GetString("VenNetPayableAmount")
                    }).ToList();
                }
            }
            return ClientInfoList;
        }
        public string SavefinalSettlement(OBS_FinalSettlement billRecv)
        {
            DataTable dtVendorQutnItem = new DataTable();

            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                billRecv.Action = "add";
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetFinalSettlement"))
                {


                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, billRecv.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, billRecv.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, billRecv.WOInfoID);

                    db.AddInParameter(dbCommandWrapper, "@WOAmt", SqlDbType.Decimal, billRecv.WOAmt);
                    db.AddInParameter(dbCommandWrapper, "@ClaimReceivedAmt", SqlDbType.Decimal, billRecv.ClaimReceivedAmt);
                    db.AddInParameter(dbCommandWrapper, "@ClientBillAmount", SqlDbType.Decimal, billRecv.ClientBillAmount);
                    db.AddInParameter(dbCommandWrapper, "@CiCommissionAmount", SqlDbType.Decimal, billRecv.CiCommissionAmount);
                    db.AddInParameter(dbCommandWrapper, "@CiBaseAmount", SqlDbType.Decimal, billRecv.CiBaseAmount);
                    db.AddInParameter(dbCommandWrapper, "@AITAmount", SqlDbType.Decimal, billRecv.AITAmount);
                    db.AddInParameter(dbCommandWrapper, "@VatAmount", SqlDbType.Decimal, billRecv.VatAmount);
                    db.AddInParameter(dbCommandWrapper, "@CiReceivedAmount", SqlDbType.Decimal, billRecv.CiReceivedAmount);


                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, billRecv.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@POInfoID", SqlDbType.Decimal, billRecv.POInfoID);

                    db.AddInParameter(dbCommandWrapper, "@POAmount", SqlDbType.Decimal, billRecv.POAmt);
                    db.AddInParameter(dbCommandWrapper, "@VendorBillAmount", SqlDbType.Decimal, billRecv.VendorBillAmount);
                    db.AddInParameter(dbCommandWrapper, "@AdvncPaidAmt", SqlDbType.Decimal, billRecv.VenAdvncPaidAmt);
                    db.AddInParameter(dbCommandWrapper, "@VenCommissionAmount", SqlDbType.Decimal, billRecv.VenCommissionAmount);
                    db.AddInParameter(dbCommandWrapper, "@VenBaseAmount", SqlDbType.Decimal, billRecv.VenBaseAmount);
                    db.AddInParameter(dbCommandWrapper, "@VenVatAmount", SqlDbType.Decimal, billRecv.VenVatAmount);
                    db.AddInParameter(dbCommandWrapper, "@VenTDSAmount", SqlDbType.Decimal, billRecv.VenTDSAmount);
                    db.AddInParameter(dbCommandWrapper, "@VenNetPayableAmount", SqlDbType.Decimal, billRecv.VenNetPayableAmount);

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

