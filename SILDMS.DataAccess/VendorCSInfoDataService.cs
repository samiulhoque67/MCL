using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Selection;

namespace SILDMS.DataAccess
{
    public class VendorCSInfoDataService : IVendorCSInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorCSInfoItem> GetVendorCSInfoDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSInfoItem> servicesCategoryList = new List<OBS_VendorCSInfoItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSInfoDashBord"))
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
                    servicesCategoryList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSInfoItem
                    {
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServiceSCategoryName"),
                        ServicesCategoryCount = reader.GetString("ServicesCategoryCount")
                    }).ToList();
                }
                //    }
            }
            return servicesCategoryList;
        }
        public List<OBS_ServicesCategory> GetServicesCategory(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ServicesCategory> servicesCategoryList = new List<OBS_ServicesCategory>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetServicesCategory"))
            {
                // Set parameters 
                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.VarChar, "");
                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    // Get the error number, if error occurred.
                    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        servicesCategoryList = dt1.AsEnumerable().Select(reader => new OBS_ServicesCategory
                        {
                            ServicesCategoryID = reader.GetString("ServicesCategoryID"),
                            ServicesCategoryCode = reader.GetString("ServicesCategoryCode"),
                            ServicesCategoryName = reader.GetString("ServicesCategoryName"),
                            SetOn = reader.GetString("SetOn"),
                            SetBy = reader.GetString("SetBy"),
                            ModifiedOn = reader.GetString("ModifiedOn"),
                            ModifiedBy = reader.GetString("ModifiedBy"),
                            Status = reader.GetInt32("Status")
                        }).ToList();
                    }
                }
            }
            return servicesCategoryList;
        }

        public List<OBS_VendorAndAddressInfo> GetVendorInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorAndAddressInfo> VendorInfoList = new List<OBS_VendorAndAddressInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorAndAddressInfo"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorAndAddressInfo
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorCode = reader.GetString("VendorCode"),
                        VendorName = reader.GetString("VendorName"),
                        VendorCategoryID = reader.GetString("ServicesCategoryID"),
                        VendorCategoryName = reader.GetString("ServicesCategoryName"),
                        VendorTinNo = reader.GetString("VendorTinNo"),
                        VendorBinNo = reader.GetString("VendorBinNo"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),

                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_ClientReq> GetVendorCSClientInfo(string ServiceCategoryID)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSClientInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.VarChar, ServiceCategoryID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        NoOfVendor = reader.GetString("NoOfVendor")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_VendorCSInfo> OBS_GetVendorCSVendorsUsingClient(string ClientID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSInfo> VendorCSInfoList = new List<OBS_VendorCSInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSVendorsUsingClient"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSInfo
                    {
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                        TolQnty = reader.GetString("QutnQnty")
                        //,
                        //LastDateofQuotation = reader.GetString("LastDateofQuotation"),
                        //Remarks = reader.GetString("Remarks"),
                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoList;
        }

        public List<OBS_VendorCSInfoItem> OBS_GetVendorCSQuotationItem(string VendorID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSInfoItem> VendorCSInfoItemList = new List<OBS_VendorCSInfoItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSQuotationItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSInfoItem
                    {
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
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
                        TolAmt = reader.GetString("TolAmt")
                        // ,

                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public List<OBS_VendorCSInfoTerms> GetVendorCSInfoTermList(string VendorCSInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSInfoTerms> VendorCSInfoItemList = new List<OBS_VendorCSInfoTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.VarChar, VendorCSInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSInfoTerms
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


        public string SaveVendorCSInfo(OBS_VendorCSInfo vendorCSInfo, List<OBS_VendorCSInfoItem> vendorCSInfoItem, List<OBS_VendorCSInfoTerms> vendorCSInfoTerm, List<OBS_VendorCSVendorsItemWise> vendorCSVendorsItemWise)
        {
            DataTable VendorCSItem = new DataTable();
            VendorCSItem.Columns.Add("VendorReqID");
            VendorCSItem.Columns.Add("ServiceCategoryID");
            VendorCSItem.Columns.Add("ServiceItemID");
            VendorCSItem.Columns.Add("ReqQnty");
            VendorCSItem.Columns.Add("ReqUnit");
            VendorCSItem.Columns.Add("QutnQnty");
            VendorCSItem.Columns.Add("QutnPrice");
            VendorCSItem.Columns.Add("QutnUnit");
            VendorCSItem.Columns.Add("QutnAmt");
            VendorCSItem.Columns.Add("VatPerc");
            VendorCSItem.Columns.Add("VatAmt");
            foreach (var item in vendorCSInfoItem)
            {
                DataRow objDataRow = VendorCSItem.NewRow();
                objDataRow[0] = item.VendorReqID;
                objDataRow[1] = item.ServiceCategoryID;
                objDataRow[2] = item.ServiceItemID;
                objDataRow[3] = item.ReqQnty;
                objDataRow[4] = item.ReqUnit;
                objDataRow[5] = item.QutnQnty;
                objDataRow[6] = item.QutnPrice;
                objDataRow[7] = item.QutnUnit;
                objDataRow[8] = item.QutnAmt;
                objDataRow[9] = item.VatPerc;
                objDataRow[10] = item.VatAmt;
                VendorCSItem.Rows.Add(objDataRow);
            }

            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");
            foreach (var item in vendorCSInfoTerm)
            {
                DataRow objDataRow = VendorCSTerm.NewRow();
                objDataRow[0] = item.TermsID;
                objDataRow[1] = item.TermsCode;
                objDataRow[2] = item.TermsName;
                VendorCSTerm.Rows.Add(objDataRow);
            }

            DataTable vendorCSVendors = new DataTable();
            vendorCSVendors.Columns.Add("VendorID");
            vendorCSVendors.Columns.Add("VendorQutnID");
            vendorCSVendors.Columns.Add("TolQnty");
            foreach (var item in vendorCSVendorsItemWise)
            {
                DataRow objDataRow = vendorCSVendors.NewRow();
                objDataRow[0] = item.VendorID;
                objDataRow[1] = item.VendorQutnID;
                objDataRow[2] = item.TolQnty;
                vendorCSVendors.Rows.Add(objDataRow);
            }

            if (string.IsNullOrEmpty(vendorCSInfo.VendorCSInfoID))
                vendorCSInfo.Action = "add";
            else
                vendorCSInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorCSInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSInfoID", SqlDbType.NVarChar, vendorCSInfo.VendorCSInfoID);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, vendorCSInfo.VendorID);
                    //db.AddInParameter(dbCommandWrapper, "@RequisitionNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RequisitionNo));
                    //db.AddInParameter(dbCommandWrapper, "@RequisitionDate", SqlDbType.NVarChar, vendorCSInfo.RequisitionDate);
                    //db.AddInParameter(dbCommandWrapper, "@SubmissionDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.SubmissionDate));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSInfoItem", SqlDbType.Structured, VendorCSItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSInfoTerm", SqlDbType.Structured, VendorCSTerm);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSVendorsItemWise", SqlDbType.Structured, vendorCSVendors);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, vendorCSInfo.Action);
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


        public List<OBS_VendorCSInfoTerms> GetVendorCSInfoTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSInfoTerms> VendorCSInfoItemList = new List<OBS_VendorCSInfoTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSInfoTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSInfoTerms
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

        public List<OBS_VendorCSVendorsItemWise> GetReqWiseVendorList(string VendorCSInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSVendorsItemWise> VendorCSInfoItemList = new List<OBS_VendorCSVendorsItemWise>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetReqWiseVendorList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSInfoID", SqlDbType.VarChar, VendorCSInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSVendorsItemWise
                    {
                        VendorCSVendorsItemWiseID = reader.GetString("VendorCSInfoItemWiseID"),
                        VendorCSInfoItemID = reader.GetString("VendorCSInfoItemID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),

                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Email = reader.GetString("Email"),
                        Address = reader.GetString("Address"),

                        VenReqQnty = reader.GetString("VenReqQnty"),
                        VenReqUnit = reader.GetString("VenReqUnit")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSInfoItemID", SqlDbType.NVarChar, VendorCSInfoItemID);
                    db.AddInParameter(dbCommandWrapper, "@VendorCSInfoTermID", SqlDbType.NVarChar, VendorCSInfoTermID);
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

        public List<OBS_Terms> GetTermsConditionsList()
        {
            string errorNumber = string.Empty;
            List<OBS_Terms> TermsConditionsList = new List<OBS_Terms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetTermsConditionsList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    TermsConditionsList = dt1.AsEnumerable().Select(reader => new OBS_Terms
                    {
                        TermsID = reader.GetString("TermsID"),
                        FormCode = reader.GetString("FormCode"),
                        FormName = reader.GetString("FormName"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return TermsConditionsList;
        }
    }
}
