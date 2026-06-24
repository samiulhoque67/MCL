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

namespace SILDMS.DataAccess.POCreation
{
    public class POCreationData : IPOCreationData
    {
        private readonly string spStatusParam = "@p_Status";
        public List<OBS_VendorCSRecmItem> GetPOCreationDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> servicesCategoryList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPODashBoard"))
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

      

        public List<OBS_ClientReq> GetPoCreationClientInfo(out string errorNumber)
        {
             errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientRequest"))
            {
              
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {

                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ProjectName = reader.GetString("ProjectName"),
                        UserFullName = reader.GetString("UserFullName"),
                        WODate = reader.GetString("WODate"),
                        WIInfoID = reader.GetString("WOInfoID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),

                    }).ToList();
                }
            }
            return VendorInfoList;
        }


        

        public List<OBS_VendorCSRecm> OBS_GetPOVendorsUsingClient(string ClientReqId,string WIInfoID, out string errorNumber)
        {
             errorNumber = string.Empty;
            List<OBS_VendorCSRecm> VendorCSInfoList = new List<OBS_VendorCSRecm>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPOVendorsUsingClient"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, ClientReqId);
                db.AddInParameter(dbCommandWrapper, "@WIInfoID", SqlDbType.VarChar, WIInfoID);
       
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecm
                    {
                        ClientID= reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        Email = reader.GetString("Email"),
                        CurrentAddress = reader.GetString("CurrentAddress"),
                        ItemCount=reader.GetString("ServiceItemCount"),
                        WIInfoID=reader.GetString("WOInfoID"),


                        //,
                        //LastDateofQuotation = reader.GetString("LastDateofQuotation"),
                        //Remarks = reader.GetString("Remarks"),
                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoList;
        }

        public List<OBS_VendorCSRecmItem> GetVendorPOQuotationItem(string vendorID, string ClientReqID,string WIInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPOQuotationItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, vendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@WIInfoID", SqlDbType.VarChar, WIInfoID);

                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        
                        VendorID = reader.GetString("VendorID"),
                        VendorReqID = reader.GetString("VendorReqID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        //ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        ReqQnty = reader.GetString("ReqQnty"),
                        ReqUnit = reader.GetString("ReqUnit"),

                        QutnQnty = reader.GetString("QutnQnty"),
                        QutnPrice = reader.GetString("QutnPrice"),
                        QutnUnit = reader.GetString("QutnUnit"),

                        QutnAmt = reader.GetString("QutnAmt"),

                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),
                        TolAmt = reader.GetString("TolAmt"),
                        NegoQty = reader.GetString("RemainingVendorQty"),
                        NegoPrice = reader.GetString("QutnPrice"),
                        NegoAmt = reader.GetString("VendorAmount"),
                        NegoVatAmt = reader.GetString("VatAmt"),
                        NegoTolAmt = reader.GetString("VendorTotalAmount"),
                        RemainingQty = reader.GetString("RemainingQty"),
                        RemainingVendorQty= reader.GetString("RemainingVendorQty")
                        // ,

                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public List<OBS_VendorCSRecmTerms> GetVendorPOInfoTermList(string vendorID, string ClientReqID, string WIInfoID, out string errorNumber)
        {
             errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> VendorCSInfoItemList = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorActualAprvTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, vendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@WIInfoID", SqlDbType.VarChar, WIInfoID);
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
                        TermsName = reader.GetString("TermsName"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),

                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public string SaveVendorPOInfo(
    OBS_VendorCSRecm vendorCSInfo,
    List<OBS_VendorCSRecmItem> vendorCSInfoItem,
    List<OBS_VendorCSRecmTerms> vendorCSRecmTerms,
    List<OBS_VendorCSRecmVendors> vendorCSVendorsItemWise)
        {
            // ── Build item TVP ───────────────────────────────────────────
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
            VendorPOItem.Columns.Add("NegoQty", typeof(decimal));
            VendorPOItem.Columns.Add("NegoPrice", typeof(decimal));
            VendorPOItem.Columns.Add("NegoVatAmt", typeof(decimal));
            VendorPOItem.Columns.Add("NegoAmt", typeof(decimal));
            VendorPOItem.Columns.Add("NegoTolAmt", typeof(decimal));

            foreach (var item in vendorCSInfoItem)
            {
                DataRow row = VendorPOItem.NewRow();
                row[0] = item.VendorQutnID;
                row[1] = TryParseInt(item.ServiceItemID);
                row[2] = item.QutnUnit;
                row[3] = TryParseDecimal(item.QutnQnty);
                row[4] = TryParseDecimal(item.QutnPrice);
                row[5] = TryParseDecimal(item.TolAmt);
                row[6] = TryParseDecimal(item.QutnAmt);
                row[7] = TryParseDecimal(item.VatPerc);
                row[8] = TryParseDecimal(item.VatAmt);
                row[9] = item.ServiceItemName;
                row[10] = item.Description;
                row[11] = item.DeliveryLocation;
                row[12] = item.DeliveryMode;
                row[13] = item.DeliveryDate;
                row[14] = item.ServiceCategoryID;
                row[15] = item.VendorReqID;
                row[16] = item.VendorCSAprvID;
                row[17] = TryParseDecimal(item.NegoQty);
                row[18] = TryParseDecimal(item.NegoPrice);
                row[19] = TryParseDecimal(item.NegoVatAmt);
                row[20] = TryParseDecimal(item.NegoAmt);
                row[21] = TryParseDecimal(item.NegoTolAmt);
                VendorPOItem.Rows.Add(row);
            }

            // ── Build terms TVP ─────────────────────────────────────────
            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID", typeof(long));
            VendorCSTerm.Columns.Add("TermsCode", typeof(string));
            VendorCSTerm.Columns.Add("TermsName", typeof(string));

            foreach (var item in vendorCSRecmTerms)
            {
                DataRow row = VendorCSTerm.NewRow();
                row[0] = TryParseLong(item.TermsID);
                row[1] = item.TermsCode;
                row[2] = item.TermsName;
                VendorCSTerm.Rows.Add(row);
            }

            // ── Action ──────────────────────────────────────────────────
            // FIX: was only setting Action string but never passing VendorCSInfoID to SP
            bool isEdit = !string.IsNullOrEmpty(vendorCSInfo.VendorCSInfoID);
            vendorCSInfo.Action = isEdit ? "edit" : "add";

            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetVendorPOInfo"))
                {
                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, TryParseLong(vendorCSInfo.ClientID));
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    db.AddInParameter(cmd, "@WIInfoID", SqlDbType.NVarChar, vendorCSInfo.WIInfoID);
                    db.AddInParameter(cmd, "@VendorID", SqlDbType.NVarChar, vendorCSVendorsItemWise[0].VendorID);
                    db.AddInParameter(cmd, "@PODate", SqlDbType.NVarChar, vendorCSInfo.CSRecDate);

                    // FIX: was SqlDbType.Decimal on a string field → cast first
                    db.AddInParameter(cmd, "@PoAmount", SqlDbType.Decimal, TryParseDecimal(vendorCSInfo.PoAmount));
                    db.AddInParameter(cmd, "@InstalledAmount", SqlDbType.Decimal, TryParseDecimal(vendorCSInfo.InstalledAmount));
                    db.AddInParameter(cmd, "@Installment", SqlDbType.Int, TryParseInt(vendorCSInfo.Installment.ToString()));
                    db.AddInParameter(cmd, "@BillType", SqlDbType.NVarChar, vendorCSInfo.BillType);
                    db.AddInParameter(cmd, "@Category", SqlDbType.NVarChar, vendorCSInfo.Category);
                    db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(cmd, "@POCreatedBy", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));

                    // FIX: these two were never passed before
                    db.AddInParameter(cmd, "@VendorCSInfoID", SqlDbType.NVarChar, vendorCSInfo.VendorCSInfoID ?? "");
                    db.AddInParameter(cmd, "@Action", SqlDbType.VarChar, vendorCSInfo.Action);

                    db.AddInParameter(cmd, "@VendorPOItemType", SqlDbType.Structured, VendorPOItem);
                    db.AddInParameter(cmd, "@OBS_VendorPoAprvTerms", SqlDbType.Structured, VendorCSTerm);
                    db.AddOutParameter(cmd, spStatusParam, SqlDbType.VarChar, 10);

                    db.ExecuteNonQuery(cmd);

                    var statusVal = db.GetParameterValue(cmd, spStatusParam);
                    if (!statusVal.IsNullOrZero())
                        errorNumber = statusVal.PrefixErrorCode();
                }
            }
            catch (Exception ex)
            {
                errorNumber = ex.InnerException?.Message ?? ex.Message;
            }

            return errorNumber;
        }


        private static decimal TryParseDecimal(object value)
        {
            if (value == null) return 0m;
            return decimal.TryParse(value.ToString(), out decimal result) ? result : 0m;
        }

        private static int TryParseInt(object value)
        {
            if (value == null) return 0;
            return int.TryParse(value.ToString(), out int result) ? result : 0;
        }

        private static long TryParseLong(object value)
        {
            if (value == null) return 0L;
            return long.TryParse(value.ToString(), out long result) ? result : 0L;
        }



        public List<Invitation> SearchPOData(string userID)
        {
            var invitationList = new List<Invitation>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_SearchForPO"))
            {
                db.AddInParameter(dbCommandWrapper, "@UserId", SqlDbType.VarChar, userID);

                // Execute SP.

                var ds = db.ExecuteDataSet(dbCommandWrapper);



                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];

                    invitationList = dt1.AsEnumerable().Select(reader => new Invitation
                    {
                        //Invitation_Number = reader.GetString("InvitationNumber"),
                        PoPreparationID = reader.GetString("PoPreparationID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        VendorID = reader.GetString("VendorID"),
                        ClientID = reader.GetString("ClientID"),
                        VendorName = reader.GetString("VendorName"),
                        PoDate= reader.GetString("PoDate"),
                        ProcessStatus= reader.GetString("ProcessStatus")






                    }).ToList();

                }
            }
            return invitationList;
        }




        public POPreparationHeader GetPOHeaderDetails(string poPreparationID, out string errorNumber)
        {
            errorNumber = string.Empty;
            POPreparationHeader header = null;

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;

            using (DbCommand cmd = db.GetStoredProcCommand("OBS_GetPOHeaderDetails"))
            {
                db.AddInParameter(cmd, "@PoPreparationID", SqlDbType.NVarChar, poPreparationID);
                DataSet ds = db.ExecuteDataSet(cmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    var reader = ds.Tables[0].AsEnumerable().First();
                    header = new POPreparationHeader
                    {
                        PoPreparationID = reader.GetString("PoPreparationID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        VendorID = reader.GetString("VendorID"),
                        PODate = reader.GetString("PODate"),
                        POAmt = reader.GetString("POAmt"),
                        BillType = reader.GetString("BillType"),
                        BillCategory = reader.GetString("BillCategory"),
                        Installment = reader.GetString("Installment"),
                        InstallmentAmt = reader.GetString("InstallmentAmt"),
                        Remarks = reader.GetString("Remarks"),
                    };
                }
            }
            return header;
        }






        //List<OBS_VendorCSRecm> GetVendorPOQuotationItem(string vendorID, string clientID, string serviceCategoryID, out string errorNumber)
        //{

        //}

        //public List<OBS_VendorCSRecm> GetVendorPOQuotationItem(string vendorID, string clientID, string serviceCategoryID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
