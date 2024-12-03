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
using SILDMS.DataAccessInterface;

namespace SILDMS.DataAccess
{
    public class VendorQuotationDataService : IVendorQuotationDataService
    {
        private readonly string spStatusParam = "@p_Status";
        public List<OBS_ServicesCategory> GetServicesCategory(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ServicesCategory> servicesCategoryList = new List<OBS_ServicesCategory>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetServicesCategory"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                //db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.VarChar, "");
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
                            ServicesCategoryName = reader.GetString("ServicesCategoryName")
                            //,
                            //SetOn = reader.GetString("SetOn"),
                            //SetBy = reader.GetString("SetBy"),
                            //ModifiedOn = reader.GetString("ModifiedOn"),
                            //ModifiedBy = reader.GetString("ModifiedBy"),
                            //Status = reader.GetInt32("Status")
                        }).ToList();
                    }
                }
            }
            return servicesCategoryList;
        }

        public List<OBS_VendorReqItem> GetVendorReqItemListForVenQutn(string VendorID, string VendorReqID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReqItem> VendorReqItemList = new List<OBS_VendorReqItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorReqItemListForVenQutn"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorReqItem
                    {
                        VendorReqItemID = reader.GetString("VendorReqItemID"),
                        VendorReqID = reader.GetString("VendorReqID"),
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
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorReqItemList;
        }
        public List<OBS_VendorQutn> GetShowVendorReqList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutn> ClientInfoList = new List<OBS_VendorQutn>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetShowVendorReqList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutn
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        VendorID = reader.GetString("VendorID"),
                        VendorReqID = reader.GetString("VendorReqID"),
                        ClientName = reader.GetString("ClientName"),
                        VendorName = reader.GetString("VendorName"),
                        RequisitionNo = reader.GetString("RequisitionNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        SubmissionDate = reader.GetString("SubmissionDate"),
                        LastDateofQuotation = reader.GetString("LastDateofQuotation"),
                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return ClientInfoList;
        }

        public string SaveVendorQuotation(OBS_VendorQutn VendorQutn, List<OBS_VendorQutnItem> VendorQutnItem, List<OBS_VendorQutnTerms> VendorQutnTerm)
        {
            DataTable dtVendorQutnItem = new DataTable();
            //dtVendorQutnItem.Columns.Add("VendorQutnItemID");
            dtVendorQutnItem.Columns.Add("VendorQutnID");
            dtVendorQutnItem.Columns.Add("ServiceCategoryID");
            dtVendorQutnItem.Columns.Add("ServiceItemID");
            dtVendorQutnItem.Columns.Add("Description");
            dtVendorQutnItem.Columns.Add("DeliveryLocation");
            dtVendorQutnItem.Columns.Add("DeliveryDate");
            dtVendorQutnItem.Columns.Add("DeliveryMode");
            dtVendorQutnItem.Columns.Add("ReqQnty");
            dtVendorQutnItem.Columns.Add("ReqUnit");
            dtVendorQutnItem.Columns.Add("QutnQnty");
            dtVendorQutnItem.Columns.Add("QutnPrice");
            dtVendorQutnItem.Columns.Add("QutnUnit");
            dtVendorQutnItem.Columns.Add("QutnAmt");
            dtVendorQutnItem.Columns.Add("VatPerc");
            dtVendorQutnItem.Columns.Add("VatAmt");

            foreach (var item in VendorQutnItem)
            {
                DataRow objDataRow = dtVendorQutnItem.NewRow();
                //objDataRow[0] = item.VendorQutnItemID;
                objDataRow[0] = item.VendorQutnID;
                objDataRow[1] = item.ServiceCategoryID;
                objDataRow[2] = item.ServiceItemID;
                objDataRow[3] = item.Description;
                objDataRow[4] = item.DeliveryLocation;
                objDataRow[5] = item.DeliveryDate;
                objDataRow[6] = item.DeliveryMode;
                objDataRow[7] = item.ReqQnty;
                objDataRow[8] = item.ReqUnit;
                objDataRow[9] = item.QutnQnty;
                objDataRow[10] = item.QutnPrice;
                objDataRow[11] = item.QutnUnit;
                objDataRow[12] = item.QutnAmt;
                objDataRow[13] = item.VatPerc;
                objDataRow[14] = item.VatAmt;

                dtVendorQutnItem.Rows.Add(objDataRow);
            }

            DataTable dtVendorQutnTerm = new DataTable();
            //dtVendorQutnItem.Columns.Add("VendorQutnItemID");
            dtVendorQutnTerm.Columns.Add("VendorQutnID");
            dtVendorQutnTerm.Columns.Add("TermsID");
            dtVendorQutnTerm.Columns.Add("TermsCode");
            dtVendorQutnTerm.Columns.Add("TermsName");
            foreach (var item in VendorQutnTerm)
            {
                DataRow objDataRow = dtVendorQutnTerm.NewRow();
                //objDataRow[0] = item.VendorQutnItemID;
                objDataRow[0] = item.VendorQutnID;
                objDataRow[1] = item.TermsID;
                objDataRow[2] = item.TermsCode;
                objDataRow[3] = item.TermsName;
                dtVendorQutnTerm.Rows.Add(objDataRow);
            }

            if (string.IsNullOrEmpty(VendorQutn.VendorQutnID))
                VendorQutn.Action = "add";
            else
                VendorQutn.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorQuotation"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.NVarChar, VendorQutn.VendorQutnID);
                    db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.NVarChar, VendorQutn.VendorReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, VendorQutn.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, VendorQutn.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqNo", SqlDbType.NVarChar, VendorQutn.ClientReqNo);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, VendorQutn.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorQutn.VendorQutnNo));
                    db.AddInParameter(dbCommandWrapper, "@QuotationDate", SqlDbType.NVarChar, VendorQutn.QuotationDate);
                    db.AddInParameter(dbCommandWrapper, "@QutnReceivedDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorQutn.QutnReceivedDate));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorQutn.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, VendorQutn.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorQutnItem", SqlDbType.Structured, dtVendorQutnItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorQutnTerm", SqlDbType.Structured, dtVendorQutnTerm);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, VendorQutn.Action);
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

        public List<OBS_VendorQutn> GetVendorQutnSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutn> VendorQutnList = new List<OBS_VendorQutn>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorQutnList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutn
                    {
                        VendorQutnID = reader.GetString("VendorQutnID"),

                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),

                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),

                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                        QutnReceivedDate = reader.GetString("QutnReceivedDate"),

                        VendorReqID = reader.GetString("VendorReqID"),
                        RequisitionNo = reader.GetString("RequisitionNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        //QutnReceivedDate = reader.GetString("QutnReceivedDate"),


                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorQutnList;
        }

        public List<OBS_VendorQutnItem> GetVendorQutnItemList(string VendorQutnID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutnItem> VendorQutnItemList = new List<OBS_VendorQutnItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnItemList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.VarChar, VendorQutnID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorQutnItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutnItem
                    {
                        VendorQutnItemID = reader.GetString("VendorQutnItemID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
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

                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorQutnItemList;
        }

        public List<OBS_VendorQutnTerms> GetVendorQutnTermList(string VendorQutnID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutnTerms> VendorQutnItemList = new List<OBS_VendorQutnTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.VarChar, VendorQutnID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorQutnItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutnTerms
                    {
                        VendorQutnTermID = reader.GetString("VendorQutnTermID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorQutnItemList;
        }

        public List<OBS_VendorQutnTerms> GetVendorQutnTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorQutnTerms> VendorQutnItemList = new List<OBS_VendorQutnTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorQutnItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorQutnTerms
                    {
                        //VendorQutnTermID = reader.GetString("VendorQutnTermID"),
                        //VendorQutnID = reader.GetString("VendorQutnID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorQutnItemList;
        }
        public string DeleteVendorQutnItemAndTerm(string VendorQutnItemID, string VendorQutnTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnItemID", SqlDbType.NVarChar, VendorQutnItemID);
                    db.AddInParameter(dbCommandWrapper, "@VendorQutnTermID", SqlDbType.NVarChar, VendorQutnTermID);
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

