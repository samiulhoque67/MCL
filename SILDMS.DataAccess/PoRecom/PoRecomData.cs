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

namespace SILDMS.DataAccess.PoRecom
{
    public class PoRecomData : IPoRecomData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientReq> GetPoRecomClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPORecomVendorCSClientInfo"))
            {

                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        POPreparationID = reader.GetString("POPreparationID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("PODate"),
                        VendorID = reader.GetString("VendorID"),

                        VendorName = reader.GetString("VendorName"),

                        ProcessStatus = reader.GetString("ProcessStatus"),   // ← add
                        PORecmID = reader.GetString("PORecmID"),        // ← add
                        RecommendedAmount = reader.GetString("RecommendedAmount"),// ← add
                        PORecDate = reader.GetString("PORecDate"),        // ← add
                        RecomRemarks = reader.GetString("RecomRemarks"),     // ← add


                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_VendorCSRecmItem> GetPORecomDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> servicesCategoryList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPORecomDashBoard"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                //db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.VarChar, "");
                //db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                //if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                //{
                //    // Get the error number, if error occurred.
                //    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                //}
                //else
                //{
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    servicesCategoryList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        ServiceCategoryID = reader.GetString("ServicesCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        ServicesCategoryCount = reader.GetString("ServiceCategoryCount")
                    }).ToList();
                }
                //    }
            }
            return servicesCategoryList;
        }

        public List<OBS_VendorCSRecmTerms> GetPORecomInfoTermList(string pOPreparationID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> VendorCSInfoItemList = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPOTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@pOPreparationID", SqlDbType.VarChar, pOPreparationID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        //VendorCSInfoTermID = reader.GetString("VendorCSInfoTermID"),
                        //VendorCSInfoID = reader.GetString("VendorCSInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public List<OBS_VendorCSRecmItem> GetVendorPORecomQuotationItem(string vendorID, string clientID, string pOPreparationID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPORecomItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, vendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, clientID);

                db.AddInParameter(dbCommandWrapper, "@pOPreparationID", SqlDbType.VarChar, pOPreparationID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorReqID = reader.GetString("VendorReqID"),
                        VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorName = reader.GetString("VendorName"),
                        POAmt = reader.GetString("POAmt"),
                        POCreatedBy = reader.GetString("POCreatedBy"),
                        PODate = (reader.GetString("PODate")),
                        PONo = reader.GetString("PONo"),
                        Remarks = reader.GetString("Remarks"),
                        BillType = reader.GetString("BillType"),
                        BillCategory = reader.GetString("BillCategory"),
                        Installment = reader.GetInt32("Installment"),
                        InstalledAmount = reader.GetDouble("InstallmentAmt"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        //ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),

                        QutnQnty = reader.GetString("QutnQnty"),
                        QutnPrice = reader.GetString("QutnPrice"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        QutnAmt = reader.GetString("QutnAmt"),

                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),
                        TolAmt = reader.GetString("TolAmt"),
                        NegoQty = reader.GetString("NegoQty"),
                        NegoPrice = reader.GetString("NegoPrice"),
                        NegoAmt = reader.GetString("NegoAmt"),
                        NegoVatAmt = reader.GetString("NegoVatAmt"),
                        NegoTolAmt = reader.GetString("NegoTolAmt"),
                        // ,

                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public string SaveVendorPORecomInfo(
           OBS_VendorCSRecm vendorCSInfo,
           List<OBS_VendorCSRecmItem> vendorCSItem,
           List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            // ── Build item DataTable ────────────────────────────────────────
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("VendorQutnID", typeof(string));
            dtItem.Columns.Add("ServiceItemID", typeof(int));
            dtItem.Columns.Add("QutnUnit", typeof(string));
            dtItem.Columns.Add("QutnQnty", typeof(decimal));
            dtItem.Columns.Add("QutnPrice", typeof(decimal));
            dtItem.Columns.Add("TolAmt", typeof(decimal));
            dtItem.Columns.Add("QutnAmt", typeof(decimal));
            dtItem.Columns.Add("VatPerc", typeof(decimal));
            dtItem.Columns.Add("VatAmt", typeof(decimal));
            dtItem.Columns.Add("ServiceItemName", typeof(string));
            dtItem.Columns.Add("Description", typeof(string));
            dtItem.Columns.Add("DeliveryLocation", typeof(string));
            dtItem.Columns.Add("DeliveryMode", typeof(string));
            dtItem.Columns.Add("DeliveryDate", typeof(string));
            dtItem.Columns.Add("ServiceCategoryID", typeof(string));
            dtItem.Columns.Add("VendorReqID", typeof(string));
            dtItem.Columns.Add("VendorCSAprvID", typeof(string));
            dtItem.Columns.Add("NegoQty");
            dtItem.Columns.Add("NegoPrice");
            dtItem.Columns.Add("NegoVatAmt");
            dtItem.Columns.Add("NegoAmt");
            dtItem.Columns.Add("NegoTolAmt");

            foreach (var item in vendorCSItem)
            {
                DataRow r = dtItem.NewRow();
                r[0] = item.VendorQutnID;
                r[1] = item.ServiceItemID;
                r[2] = item.QutnUnit;
                r[3] = item.QutnQnty;
                r[4] = item.QutnPrice;
                r[5] = item.TolAmt;
                r[6] = item.QutnAmt;
                r[7] = item.VatPerc;
                r[8] = item.VatAmt;
                r[9] = item.ServiceItemName;
                r[10] = item.Description;
                r[11] = item.DeliveryLocation;
                r[12] = item.DeliveryMode;
                r[13] = item.DeliveryDate;
                r[14] = item.ServiceCategoryID;
                r[15] = item.VendorReqID;
                r[16] = item.VendorCSAprvID;
                r[17] = item.NegoQty;
                r[18] = item.NegoPrice;
                r[19] = item.NegoVatAmt;
                r[20] = item.NegoAmt;
                r[21] = item.NegoTolAmt;
                dtItem.Rows.Add(r);
            }

            // ── Build term DataTable ────────────────────────────────────────
            DataTable dtTerm = new DataTable();
            dtTerm.Columns.Add("TermsID");
            dtTerm.Columns.Add("TermsCode");
            dtTerm.Columns.Add("TermsName");

            foreach (var term in vendorCSTerm)
            {
                DataRow r = dtTerm.NewRow();
                r[0] = term.TermsID;
                r[1] = term.TermsCode;
                r[2] = term.TermsName;
                dtTerm.Rows.Add(r);
            }

            string result = string.Empty;

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorPORecomInfo"))
                {
                    db.AddInParameter(cmd, "@POPreparationID", SqlDbType.NVarChar,
                        vendorCSInfo.POPreparationID);

                    // ── NEW: pass PORecmID so SP can UPDATE when non-empty ──
                    db.AddInParameter(cmd, "@PORecmID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorCSInfo.PORecmID));

                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, vendorCSInfo.ClientID);
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    db.AddInParameter(cmd, "@VendorID", SqlDbType.NVarChar,
                        vendorCSItem.Count > 0 ? vendorCSItem[0].VendorID : vendorCSInfo.VendorID);

                    db.AddInParameter(cmd, "@PoNo", SqlDbType.NVarChar, vendorCSInfo.AutoPoNo);
                    db.AddInParameter(cmd, "@PODate", SqlDbType.NVarChar, vendorCSInfo.PORecDate);
                    db.AddInParameter(cmd, "@PoAmount", SqlDbType.Decimal, vendorCSInfo.RecommendedAmount);
                    db.AddInParameter(cmd, "@Installment", SqlDbType.Int, vendorCSInfo.Installment);
                    db.AddInParameter(cmd, "@InstalledAmount", SqlDbType.Decimal, vendorCSInfo.InstalledAmount);
                    db.AddInParameter(cmd, "@BillType", SqlDbType.NVarChar, vendorCSInfo.BillType);
                    db.AddInParameter(cmd, "@Category", SqlDbType.NVarChar, vendorCSInfo.Category);
                    db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(cmd, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);

                    db.AddInParameter(cmd, "@VendorPOItemType", SqlDbType.Structured, dtItem);
                    db.AddInParameter(cmd, "@OBS_VendorCSAprvTerms", SqlDbType.Structured, dtTerm);

                    db.AddOutParameter(cmd, spStatusParam, SqlDbType.VarChar, 10);
                    db.ExecuteNonQuery(cmd);

                    if (!db.GetParameterValue(cmd, spStatusParam).IsNullOrZero())
                        result = db.GetParameterValue(cmd, spStatusParam).PrefixErrorCode();
                }
            }
            catch (Exception ex)
            {
                result = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
    }

}
