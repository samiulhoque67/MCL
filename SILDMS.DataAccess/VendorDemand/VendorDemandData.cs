using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorDemand
{
    public class VendorDemandData : IVendorDemandData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorDemand> GetVendorDemandClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorDemand> list = new List<OBS_VendorDemand>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorDemandClientInfo"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorDemand
                    {
                        POAprvID = reader.GetString("POAprvID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        PoNo = reader.GetString("PoNo"),
                        TotalPOAmount = reader.GetString("TotalPOAmount"),
                        PoAprvDate = reader.GetString("PoAprvDate"),
                        BillCategory = reader.GetString("BillCategory"),

                        VendorDemandID = reader.GetString("VendorDemandID"),
                        DemandAmount = reader.GetString("DemandAmount"),
                        RemainingPOAmount = reader.GetString("RemainingPOAmount"),
                        DemandDate = reader.GetString("DemandDate"),
                        DemandRemarks = reader.GetString("DemandRemarks"),
                        DemandedBy = reader.GetString("DemandedBy"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorDemandItem> GetVendorDemandItem(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorDemandItem> list = new List<OBS_VendorDemandItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorDemandItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorDemandItem
                    {
                        POAprvItemID = reader.GetString("POAprvItemID"),
                        PoAprvID = reader.GetString("PoAprvID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServiceCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        Quantity = reader.GetString("Quantity"),
                        UnitPrice = reader.GetString("UnitPrice"),
                        UnitPriceWithoutVat = reader.GetString("UnitPriceWithoutVat"),
                        VatPerc = reader.GetString("VatPerc"),
                        VatAmount = reader.GetString("VatAmount"),
                        TotalAmountWithVat = reader.GetString("TotalAmountWithVat"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorCSRecmTerms> GetVendorDemandTermList(string pOAprvID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> list = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorDemandTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@POAprvID", SqlDbType.NVarChar, pOAprvID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        TermsID = reader.GetString("POAprvTermID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                    }).ToList();
                }
            }
            return list;
        }

        public string SaveVendorDemandInfo(OBS_VendorDemand vendorDemand)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorDemandInfo"))
                {
                    db.AddInParameter(cmd, "@POAprvID", SqlDbType.NVarChar, vendorDemand.POAprvID);
                    db.AddInParameter(cmd, "@VendorDemandID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorDemand.VendorDemandID));
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, vendorDemand.ClientReqID);
                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, vendorDemand.ClientID);
                    db.AddInParameter(cmd, "@VendorID", SqlDbType.NVarChar, vendorDemand.VendorID);
                    db.AddInParameter(cmd, "@DemandAmount", SqlDbType.Decimal, vendorDemand.DemandAmount);
                    db.AddInParameter(cmd, "@DemandDate", SqlDbType.NVarChar, vendorDemand.DemandDate);
                    db.AddInParameter(cmd, "@Remarks", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorDemand.Remarks));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, vendorDemand.SetBy);

                    db.AddInParameter(cmd, "@IsFullPayment", SqlDbType.NVarChar,
    string.IsNullOrEmpty(vendorDemand.IsFullPayment) ? "0" : vendorDemand.IsFullPayment);
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
    }
}
