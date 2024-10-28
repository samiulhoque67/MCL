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

        public List<OBS_ClientReq> GetPoAprvClientInfo(string serviceCategoryID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPOAprvVendorCSClientInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.VarChar, serviceCategoryID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        PORecmID = reader.GetString("PORecmID"),
                        POPreparationID = reader.GetString("POPreparationID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        VendorID = reader.GetString("VendorID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                   
                        VendorName = reader.GetString("VendorName"),


                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_VendorCSRecmItem> GetPOAprvDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> servicesCategoryList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetPOAprvDashBoard"))
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

        public List<OBS_VendorCSRecmItem> GetVendorPOAprvQuotationItem(string vendorID, string clientID, string serviceCategoryID, string PORecmID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorPOAprvItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, vendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, clientID);
                db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.VarChar, serviceCategoryID);
                db.AddInParameter(dbCommandWrapper, "@PORecmID", SqlDbType.VarChar, PORecmID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        POAmt = reader.GetString("RecomAmnt"),
                        POCreatedBy = reader.GetString("RecommendedBy"),
                        PODate =reader.GetString("PORecomDate"),
                        PONo = reader.GetString("PONo"),
                        Remarks = reader.GetString("Remarks"),

                        ServiceItemID = reader.GetString("ServiceItemID"),
                        //ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        //Description = reader.GetString("Description"),
                        //DeliveryLocation = reader.GetString("DeliveryLocation"),
                        //DeliveryDate = reader.GetDateTime("DeliveryDate"),
                        //DeliveryMode = reader.GetString("DeliveryMode"),

                        QutnQnty = reader.GetString("QutnQnty"),
                        QutnPrice = reader.GetString("QutnPrice"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        QutnAmt = reader.GetString("QutnAmt"),

                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),
                        TolAmt = reader.GetString("TolAmt")
                        // ,

                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

   

        public string SaveVendorPOAprvInfo(OBS_VendorCSRecm vendorCSInfo, List<OBS_VendorCSRecmItem> vendorCSItem, List<OBS_VendorCSRecmTerms> vendorCSTerm)
        {
            string errorNumber = String.Empty;
            DataTable VendorPOItem = new DataTable();
            VendorPOItem.Columns.Add("VendorID", typeof(string)); // Assuming VendorID is an integer
            VendorPOItem.Columns.Add("ServiceItemID", typeof(int)); // Assuming ServiceItemID is an integer
                                                                    //VendorPOItem.Columns.Add("ReqQnty", typeof(int)); // Uncomment and define type if needed
            VendorPOItem.Columns.Add("QutnUnit", typeof(string)); // Assuming QutnUnit is a string
            VendorPOItem.Columns.Add("QutnQnty", typeof(decimal)); // Assuming QutnQnty is a decimal
            VendorPOItem.Columns.Add("QutnPrice", typeof(decimal)); // Assuming QutnPrice is a decimal
            VendorPOItem.Columns.Add("TolAmt", typeof(decimal)); // Assuming TolAmt is a decimal
            VendorPOItem.Columns.Add("QutnAmt", typeof(decimal)); // Assuming QutnAmt is a decimal
            VendorPOItem.Columns.Add("VatPerc", typeof(decimal)); // Assuming VatPerc is a decimal
            VendorPOItem.Columns.Add("VatAmt", typeof(decimal));
            foreach (var item in vendorCSItem)
            {
                DataRow objDataRow = VendorPOItem.NewRow();
                objDataRow[0] = item.VendorID;
                objDataRow[1] = item.ServiceItemID;
                objDataRow[2] = item.QutnUnit;
                objDataRow[3] = item.QutnQnty;
                objDataRow[4] = item.QutnPrice;
                objDataRow[5] = item.TolAmt;
                objDataRow[6] = item.QutnAmt;
                objDataRow[7] = item.VatPerc;
                objDataRow[8] = item.VatAmt;

                VendorPOItem.Rows.Add(objDataRow);
            }

            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");
            foreach (var item in vendorCSTerm)
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

            //if (string.IsNullOrEmpty(vendorCSInfo.VendorCSInfoID))
            //    vendorCSInfo.Action = "add";
            //else
            //    vendorCSInfo.Action = "edit";
            //string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorPOAprvInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@PoRecmID", SqlDbType.NVarChar, vendorCSInfo.PORecmID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.BigInt, vendorCSInfo.ServiceCategoryID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.BigInt, vendorCSInfo.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    //db.AddInParameter(dbCommandWrapper, "@TolAmt", SqlDbType.NVarChar, vendorCSVendorsItemWise[0].TolAmt);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, vendorCSItem[0].VendorID);
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.NVarChar, vendorCSInfo.VendorQutnID);
                    db.AddInParameter(dbCommandWrapper, "@PoNo", SqlDbType.NVarChar, vendorCSInfo.AutoPoNo);
                    //db.AddInParameter(dbCommandWrapper, "@CSNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.CSNo));
                    db.AddInParameter(dbCommandWrapper, "@PODate", SqlDbType.NVarChar, vendorCSInfo.PORecDate);
                    db.AddInParameter(dbCommandWrapper, "@PoAmount", SqlDbType.Decimal, vendorCSInfo.RecommendedAmount);
                    db.AddInParameter(dbCommandWrapper, "@RecommendedBy", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));

                    db.AddInParameter(dbCommandWrapper, "@Note", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@VendorPOItemType", SqlDbType.Structured, VendorPOItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSAprvTerms", SqlDbType.Structured, VendorCSTerm);
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
    }
}
