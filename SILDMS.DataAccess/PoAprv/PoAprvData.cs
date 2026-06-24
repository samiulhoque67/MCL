using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.PoAprv
{
    public class PoAprvData : IPoAprvData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientReq> GetPoAprvClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPOAprvVendorCSClientInfo"))
            {

                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        PORecmID = reader.GetString("PORecmID"),
                        POPreparationID = reader.GetString("POPreparationID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        VendorID = reader.GetString("VendorID"),


                        VendorName = reader.GetString("VendorName"),


                        ProcessStatus = reader.GetString("ProcessStatus"),    // ← add
                        PoAprvID = reader.GetString("PoAprvID"),         // ← add
                        ApprovedAmount = reader.GetString("ApprovedAmount"),   // ← add
                        POAprvDate = reader.GetString("POAprvDate"),       // ← add
                        RecommendedBy = reader.GetString("RecommendedBy"),    // ← add
                        AprvRemarks = reader.GetString("AprvRemarks"),      // ← add


                    }).ToList();
                }
            }
            return VendorInfoList;
        }


        public List<OBS_ClientReq> GetPoAprvClientInfo_Saved(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPOAprvVendorCSClientInfo_Saved"))
            {

                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        PORecmID = reader.GetString("PORecmID"),
                        POPreparationID = reader.GetString("POPreparationID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        VendorID = reader.GetString("VendorID"),


                        VendorName = reader.GetString("VendorName"),


                        ProcessStatus = reader.GetString("ProcessStatus"),    // ← add
                        PoAprvID = reader.GetString("PoAprvID"),         // ← add
                        ApprovedAmount = reader.GetString("ApprovedAmount"),   // ← add
                        POAprvDate = reader.GetString("POAprvDate"),       // ← add
                        RecommendedBy = reader.GetString("RecommendedBy"),    // ← add
                        AprvRemarks = reader.GetString("AprvRemarks"),      // ← add


                    }).ToList();
                }
            }
            return VendorInfoList;
        }
        public List<OBS_VendorCSRecmTerms> GetPOAprvInfoTermList(string PORecmID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> VendorCSInfoItemList = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPORecmTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@PORecmID", SqlDbType.VarChar, PORecmID);
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

        public List<OBS_VendorCSRecmItem> GetVendorPOAprvQuotationItem(string vendorID, string clientID, string PORecmID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPOAprvItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, vendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, clientID);

                db.AddInParameter(dbCommandWrapper, "@PORecmID", SqlDbType.VarChar, PORecmID);
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
                        VendorName = reader.GetString("VendorName"),
                        POAmt = reader.GetString("RecomAmnt"),
                        POCreatedBy = reader.GetString("RecommendedBy"),
                        PODate = reader.GetString("PORecomDate"),
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

        public List<OBS_VendorCSRecmItem> PoPrint(string pORecmID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_PoPrintItem"))
            {


                db.AddInParameter(dbCommandWrapper, "@PORecmID", SqlDbType.VarChar, pORecmID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        VendorID = reader.GetString("VendorID"),
                        WIInfoID = reader.GetString("WOInfoID"),
                        VendorReqID = reader.GetString("VendorReqID"),
                        VendorAddress = reader.GetString("CurrentAddress"),
                        VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        VendorName = reader.GetString("VendorName"),
                        POAmt = reader.GetString("AprvAmnt"),
                        POCreatedBy = reader.GetString("RecommendedBy"),
                        PODate = reader.GetString("PoAprvDate"),
                        PONo = reader.GetString("PONo"),
                        Remarks = reader.GetString("Remarks"),

                        ServiceItemID = reader.GetString("ServiceItemID"),
                        //ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        ReqUnit = reader.GetString("QutnUnit"),
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

        public string SaveVendorPOAprvInfo(
    OBS_VendorCSRecm vendorCSInfo,
    List<OBS_VendorCSRecmItem> vendorCSItem,
    List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            string errorNumber = string.Empty;

            // ── Build item DataTable (unchanged from original) ──────────────
            DataTable VendorPOItem = new DataTable();
            VendorPOItem.Columns.Add("VendorQutnID", typeof(string));
            VendorPOItem.Columns.Add("ServiceItemID", typeof(int));
            VendorPOItem.Columns.Add("QutnUnit", typeof(string));
            VendorPOItem.Columns.Add("QutnQnty", typeof(decimal));
            VendorPOItem.Columns.Add("QutnPrice", typeof(decimal));
            VendorPOItem.Columns.Add("TolAmt", typeof(decimal));
            VendorPOItem.Columns.Add("QutnAmt", typeof(decimal));
            VendorPOItem.Columns.Add("VatPerc", typeof(decimal));
            VendorPOItem.Columns.Add("VatAmt", typeof(decimal));
            VendorPOItem.Columns.Add("ServiceItemName", typeof(string));
            VendorPOItem.Columns.Add("Description", typeof(string));
            VendorPOItem.Columns.Add("DeliveryLocation", typeof(string));
            VendorPOItem.Columns.Add("DeliveryMode", typeof(string));
            VendorPOItem.Columns.Add("DeliveryDate", typeof(string));
            VendorPOItem.Columns.Add("ServiceCategoryID", typeof(string));
            VendorPOItem.Columns.Add("VendorReqID", typeof(string));
            VendorPOItem.Columns.Add("VendorCSAprvID", typeof(string));
            VendorPOItem.Columns.Add("NegoQty");
            VendorPOItem.Columns.Add("NegoPrice");
            VendorPOItem.Columns.Add("NegoVatAmt");
            VendorPOItem.Columns.Add("NegoAmt");
            VendorPOItem.Columns.Add("NegoTolAmt");

            foreach (var item in vendorCSItem)
            {
                DataRow r = VendorPOItem.NewRow();
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
                VendorPOItem.Rows.Add(r);
            }

            // ── Build term DataTable (unchanged) ────────────────────────────
            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");

            foreach (var item in vendorCSTerm)
            {
                DataRow r = VendorCSTerm.NewRow();
                r[0] = item.TermsID;
                r[1] = item.TermsCode;
                r[2] = item.TermsName;
                VendorCSTerm.Rows.Add(r);
            }

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorPOAprvInfo"))
                {
                    db.AddInParameter(cmd, "@PoRecmID", SqlDbType.NVarChar,
                        vendorCSInfo.PORecmID);

                    // ── NEW: pass PoAprvID so SP can UPDATE when non-empty ──
                    db.AddInParameter(cmd, "@PoAprvID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorCSInfo.PoAprvID));

                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt,
                        vendorCSInfo.ClientID);
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar,
                        vendorCSInfo.ClientReqID);
                    db.AddInParameter(cmd, "@VendorID", SqlDbType.NVarChar,
                        vendorCSItem.Count > 0 ? vendorCSItem[0].VendorID : vendorCSInfo.VendorID);

                    db.AddInParameter(cmd, "@PoNo", SqlDbType.NVarChar,
                        vendorCSInfo.AutoPoNo);
                    db.AddInParameter(cmd, "@PODate", SqlDbType.NVarChar,
                        vendorCSInfo.PORecDate);
                    db.AddInParameter(cmd, "@PoAmount", SqlDbType.Decimal,
                        vendorCSInfo.RecommendedAmount);
                    db.AddInParameter(cmd, "@RecommendedBy", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));
                    db.AddInParameter(cmd, "@Installment", SqlDbType.Int,
                        vendorCSInfo.Installment);
                    db.AddInParameter(cmd, "@InstalledAmount", SqlDbType.Decimal,
                        vendorCSInfo.InstalledAmount);
                    db.AddInParameter(cmd, "@BillType", SqlDbType.NVarChar,
                        vendorCSInfo.BillType);
                    db.AddInParameter(cmd, "@Category", SqlDbType.NVarChar,
                        vendorCSInfo.Category);
                    db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(cmd, "@UserID ", SqlDbType.NVarChar,
                        vendorCSInfo.SetBy);

                    db.AddInParameter(cmd, "@VendorPOItemType", SqlDbType.Structured, VendorPOItem);
                    db.AddInParameter(cmd, "@OBS_VendorCSAprvTerms", SqlDbType.Structured, VendorCSTerm);

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
