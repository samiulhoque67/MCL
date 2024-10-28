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
using SILDMS.Utillity;
using System.Net.NetworkInformation;
using Ninject.Selection;

namespace SILDMS.DataAccess
{
    public class WorkOrderInfoDataService : IWorkOrderInfoDataService
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

        public List<OBS_WOInfo> GetClientInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfo> ClientInfoList = new List<OBS_WOInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQutnAprvDataForRevisedAndWO"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_WOInfo
                    {
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        AprvNo = reader.GetString("AprvNo"),
                        AprvDate = reader.GetString("AprvDate"),
                        //,
                        //ContactPerson = reader.GetString("ContactPerson"),
                        //ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),

                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return ClientInfoList;
        }
        public List<OBS_WOInfoItem> GetWOInfoItemList(string ClientID)
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfoItem> WOInfoItemList = new List<OBS_WOInfoItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQutnAprvItemDataForRevisedAndWO"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, ClientID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_WOInfoItem
                    {
                        //CQAI.ClientQutnAprvItemID,CQAI.ClientQutnAprvID,CQAI.VendorQutnID,

                        //ClientQutnAprvItemID = reader.GetString("ClientQutnAprvItemID"),
                        //ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),

                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),

                        //ReqQnty = reader.GetString("ReqQnty"),
                        //ReqUnit = reader.GetString("ReqUnit"),

                        QutnUnit = reader.GetString("QutnUnit"),


                        QutnQnty = reader.GetString("QutnQnty"),
                        QutnPrice = reader.GetString("QutnPrice"),
                        QutnAmt = reader.GetString("QutnAmt"),

                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),

                        TolAmt = reader.GetString("TolAmt")
                        //,

                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return WOInfoItemList;
        }

        public List<OBS_WOInfoTerms> GetWOInfoTermList(string WOInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfoTerms> WOInfoItemList = new List<OBS_WOInfoTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQutnAprvTermsDataForRevisedAndWo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, WOInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_WOInfoTerms
                    {
                        //WOInfoTermID = reader.GetString("WOInfoTermID"),
                        //WOInfoID = reader.GetString("WOInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return WOInfoItemList;
        }

        public string SaveWorkOrderInfo(OBS_WOInfo woInfo, List<OBS_WOInfoItem> woInfoItem, List<OBS_WOInfoTerms> woInfoTerm)
        {
            DataTable dtReqItem = new DataTable();
            //dtReqItem.Columns.Add("WOInfoItemID");
            dtReqItem.Columns.Add("WOInfoID");
            dtReqItem.Columns.Add("ServiceCategoryID");
            dtReqItem.Columns.Add("ServiceItemID");
            dtReqItem.Columns.Add("Description");
            dtReqItem.Columns.Add("DeliveryLocation");
            dtReqItem.Columns.Add("DeliveryDate");
            dtReqItem.Columns.Add("DeliveryMode");
            dtReqItem.Columns.Add("ReqQnty");
            dtReqItem.Columns.Add("ReqUnit");

            dtReqItem.Columns.Add("QutnQnty");
            dtReqItem.Columns.Add("QutnPrice");
            dtReqItem.Columns.Add("QutnUnit");

            dtReqItem.Columns.Add("QutnAmt");
            dtReqItem.Columns.Add("VatPerc");
            dtReqItem.Columns.Add("VatAmt");

            dtReqItem.Columns.Add("TolAmt");

            foreach (var item in woInfoItem)
            {
                DataRow objDataRow = dtReqItem.NewRow();
                //objDataRow[0] = item.WOInfoItemID;
                objDataRow[0] = item.WOInfoID;
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

                objDataRow[15] = item.TolAmt;

                dtReqItem.Rows.Add(objDataRow);
            }

            DataTable dtReqTerm = new DataTable();
            //dtReqItem.Columns.Add("WOInfoItemID");
            dtReqTerm.Columns.Add("WOInfoID");
            dtReqTerm.Columns.Add("TermsID");
            dtReqTerm.Columns.Add("TermsCode");
            dtReqTerm.Columns.Add("TermsName");
            foreach (var item in woInfoTerm)
            {
                DataRow objDataRow = dtReqTerm.NewRow();
                //objDataRow[0] = item.WOInfoItemID;
                objDataRow[0] = item.WOInfoID;
                objDataRow[1] = item.TermsID;
                objDataRow[2] = item.TermsCode;
                objDataRow[3] = item.TermsName;
                dtReqTerm.Rows.Add(objDataRow);
            }

            if (string.IsNullOrEmpty(woInfo.WOInfoID))
                woInfo.Action = "add";
            else
                woInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetWOInfo"))
                {

                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, woInfo.WOInfoID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, woInfo.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.NVarChar, woInfo.ClientQutnAprvID);
                    db.AddInParameter(dbCommandWrapper, "@WONo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.WONo));
                    db.AddInParameter(dbCommandWrapper, "@WODate", SqlDbType.NVarChar, woInfo.WODate);
                    db.AddInParameter(dbCommandWrapper, "@WOAmt", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.WOAmt));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, woInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_WOInfoItem", SqlDbType.Structured, dtReqItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_WOInfoTerm", SqlDbType.Structured, dtReqTerm);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, woInfo.Action);
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

        public List<OBS_WOInfo> GetWOInfoSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfo> WOInfoList = new List<OBS_WOInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOInfoSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoList = dt1.AsEnumerable().Select(reader => new OBS_WOInfo
                    {
                        WOInfoID = reader.GetString("WOInfoID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        WONo = reader.GetString("WONo"),
                        WODate = reader.GetString("WODate"),
                        WOAmt = reader.GetString("WOAmt"),
                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return WOInfoList;
        }



        public List<OBS_WOInfoItem> GetWOInfoSearchItemList(string WOInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfoItem> WOInfoItemList = new List<OBS_WOInfoItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOInfoSearchItemList"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.VarChar, WOInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_WOInfoItem
                    {

                        WOInfoItemID = reader.GetString("WOInfoItemID"),
                        WOInfoID = reader.GetString("WOInfoID"),
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
            return WOInfoItemList;
        }

        public List<OBS_WOInfoTerms> GetWOInfoSearchTermsList(string WOInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfoTerms> WOInfoItemList = new List<OBS_WOInfoTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOInfoSearchTermsList"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.VarChar, WOInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_WOInfoTerms
                    {
                        WOInfoTermID = reader.GetString("WOInfoTermID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return WOInfoItemList;
        }

        public List<OBS_WOInfoTerms> GetWOInfoTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_WOInfoTerms> WOInfoItemList = new List<OBS_WOInfoTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetWOInfoTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    WOInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_WOInfoTerms
                    {
                        //WOInfoTermID = reader.GetString("WOInfoTermID"),
                        //WOInfoID = reader.GetString("WOInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return WOInfoItemList;
        }
        public string DeleteWOInfoItemAndTerm(string WOInfoItemID, string WOInfoTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@WOInfoItemID", SqlDbType.NVarChar, WOInfoItemID);
                    db.AddInParameter(dbCommandWrapper, "@WOInfoTermID", SqlDbType.NVarChar, WOInfoTermID);
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