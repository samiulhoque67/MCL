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
                        WIInfoID = reader.GetString("WOInfoID"),
                        //ClientReqNo = reader.GetString("ClientReqNo"),
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


        public string SaveVendorPOInfo(OBS_VendorCSRecm vendorCSInfo, List<OBS_VendorCSRecmItem> vendorCSInfoItem, List<OBS_VendorCSRecmTerms> vendorCSRecmTerms, List<OBS_VendorCSRecmVendors> vendorCSVendorsItemWise)
        {
            DataTable VendorPOItem = new DataTable();
            VendorPOItem.Columns.Add("VendorQutnID", typeof(string)); // Assuming VendorID is an integer
            VendorPOItem.Columns.Add("ServiceItemID", typeof(int)); // Assuming ServiceItemID is an integer
                                                             //VendorPOItem.Columns.Add("ReqQnty", typeof(int)); // Uncomment and define type if needed
            VendorPOItem.Columns.Add("QutnUnit", typeof(string)); // Assuming QutnUnit is a string
            VendorPOItem.Columns.Add("QutnQnty", typeof(decimal)); // Assuming QutnQnty is a decimal
            VendorPOItem.Columns.Add("QutnPrice", typeof(decimal)); // Assuming QutnPrice is a decimal
            VendorPOItem.Columns.Add("TolAmt", typeof(decimal)); // Assuming TolAmt is a decimal
            VendorPOItem.Columns.Add("QutnAmt", typeof(decimal)); // Assuming QutnAmt is a decimal
            VendorPOItem.Columns.Add("VatPerc", typeof(decimal)); // Assuming VatPerc is a decimal
            VendorPOItem.Columns.Add("VatAmt", typeof(decimal));
            VendorPOItem.Columns.Add("ServiceItemName", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("Description", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("DeliveryLocation", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("DeliveryMode", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("DeliveryDate", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("ServiceCategoryID", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("VendorReqID", typeof(string)); // Assuming ServiceItemID is an integer
            VendorPOItem.Columns.Add("VendorCSAprvID", typeof(string));
            VendorPOItem.Columns.Add("NegoQty");
            VendorPOItem.Columns.Add("NegoPrice");
            VendorPOItem.Columns.Add("NegoVatAmt");
            VendorPOItem.Columns.Add("NegoAmt");
            VendorPOItem.Columns.Add("NegoTolAmt");
            // Assuming ServiceItemID is an integer
            // Assuming ServiceItemID is an integer

            foreach (var item in vendorCSInfoItem)
            {
                DataRow objDataRow = VendorPOItem.NewRow();
                objDataRow[0] = item.VendorQutnID;
                objDataRow[1] = item.ServiceItemID;
                objDataRow[2] = item.QutnUnit;
                objDataRow[3] = item.QutnQnty;
                objDataRow[4] = item.QutnPrice;
                objDataRow[5] = item.TolAmt;
                objDataRow[6] = item.QutnAmt;
                objDataRow[7] = item.VatPerc;
                objDataRow[8] = item.VatAmt;
                objDataRow[9] = item.ServiceItemName;
                objDataRow[10] = item.Description;
                objDataRow[11] = item.DeliveryLocation;
                objDataRow[12] = item.DeliveryMode;
                objDataRow[13] = item.DeliveryDate;
                objDataRow[14] = item.ServiceCategoryID;
                objDataRow[15] = item.VendorReqID;
                objDataRow[16] = item.VendorCSAprvID;
                objDataRow[17] = item.NegoQty;
                objDataRow[18] = item.NegoPrice;
                objDataRow[19] = item.NegoVatAmt;
                objDataRow[20] = item.NegoAmt;
                objDataRow[21] = item.NegoTolAmt;

                VendorPOItem.Rows.Add(objDataRow);
            }

            DataTable VendorCSTerm = new DataTable();
          
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");
           
            foreach (var item in vendorCSRecmTerms)
            {
                DataRow objDataRow = VendorCSTerm.NewRow();
           
                objDataRow[0] = item.TermsID;
                objDataRow[1] = item.TermsCode;
                objDataRow[2] = item.TermsName;
             
                VendorCSTerm.Rows.Add(objDataRow);
            }

            //DataTable vendorCSVendors = new DataTable();
            //vendorCSVendors.Columns.Add("VendorID");
            //vendorCSVendors.Columns.Add("VendorQutnID");
            //vendorCSVendors.Columns.Add("TolQnty");
            //foreach (var item in vendorCSVendorsItemWise)
            //{
            //    DataRow objDataRow = vendorCSVendors.NewRow();
            //    objDataRow[0] = item.VendorID;
            //    objDataRow[1] = item.VendorQutnID;
            //    objDataRow[2] = item.TolQnty;
            //    vendorCSVendors.Rows.Add(objDataRow);
            //}

            if (string.IsNullOrEmpty(vendorCSInfo.VendorCSInfoID))
                vendorCSInfo.Action = "add";
            else
                vendorCSInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorPOInfo"))
                {
                    // Set parameters 
                    //db.AddInParameter(dbCommandWrapper, "@VendorCSAprvID", SqlDbType.NVarChar, vendorCSInfo.VendorCSAprvID);
                    //db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.BigInt, vendorCSInfo.ServiceCategoryID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.BigInt, vendorCSInfo.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@WIInfoID", SqlDbType.NVarChar, vendorCSInfo.WIInfoID);
                    //db.AddInParameter(dbCommandWrapper, "@TolAmt", SqlDbType.NVarChar, vendorCSVendorsItemWise[0].TolAmt);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, vendorCSVendorsItemWise[0].VendorID);
                    //db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.NVarChar, vendorCSVendorsItemWise[0].VendorQutnID);
                    //db.AddInParameter(dbCommandWrapper, "@CSNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.CSNo));
                    db.AddInParameter(dbCommandWrapper, "@PODate", SqlDbType.NVarChar, vendorCSInfo.CSRecDate);
                    db.AddInParameter(dbCommandWrapper, "@PoAmount", SqlDbType.Decimal, vendorCSInfo.PoAmount);
                    db.AddInParameter(dbCommandWrapper, "@Installment", SqlDbType.Int, vendorCSInfo.Installment);
                    db.AddInParameter(dbCommandWrapper, "@InstalledAmount", SqlDbType.Decimal, vendorCSInfo.InstalledAmount);
                    db.AddInParameter(dbCommandWrapper, "@BillType", SqlDbType.NVarChar, vendorCSInfo.BillType);
                    db.AddInParameter(dbCommandWrapper, "@Category", SqlDbType.NVarChar, vendorCSInfo.Category);
                    db.AddInParameter(dbCommandWrapper, "@POCreatedBy", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));

                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    //db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@VendorPOItemType", SqlDbType.Structured, VendorPOItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorPoAprvTerms", SqlDbType.Structured, VendorCSTerm);
                    //db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSRecmVendors", SqlDbType.Structured, vendorCSVendors);
                    //db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, vendorCSInfo.Action);
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
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        PoDate= reader.GetString("PoDate")






                    }).ToList();

                }
            }
            return invitationList;
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
