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

namespace SILDMS.DataAccess
{
    public class QuotationToClientRevisedDataService: IQuotationToClientRevisedDataService
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

        public List<OBS_QuotationToClientRevised> GetClientInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevised> ClientInfoList = new List<OBS_QuotationToClientRevised>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQutnAprvDataForRevisedAndWO")) 
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevised
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
        public List<OBS_QuotationToClientRevisedItem> GetQuotationToClientRevisedItemList(string ClientID)
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedItem>();
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
                    QuotationToClientRevisedItemList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevisedItem
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
            return QuotationToClientRevisedItemList;
        }

        public List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedTermList(string QuotationToClientRevisedID)
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQutnAprvTermsDataForRevisedAndWo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.VarChar, QuotationToClientRevisedID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    QuotationToClientRevisedItemList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevisedTerms
                    {
                        //QuotationToClientRevisedTermID = reader.GetString("QuotationToClientRevisedTermID"),
                        //QuotationToClientRevisedID = reader.GetString("QuotationToClientRevisedID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return QuotationToClientRevisedItemList;
        }

        public string SaveQuotationToClientRevised(OBS_QuotationToClientRevised woInfo, List<OBS_QuotationToClientRevisedItem> woInfoItem, List<OBS_QuotationToClientRevisedTerms> woInfoTerm)
        {
            DataTable dtReqItem = new DataTable();
            //dtReqItem.Columns.Add("QuotationToClientRevisedItemID");
            dtReqItem.Columns.Add("QuotationToClientRevisedID");
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
                //objDataRow[0] = item.QuotationToClientRevisedItemID;
                objDataRow[0] = item.QuotationToClientRevisedID;
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
            //dtReqItem.Columns.Add("QuotationToClientRevisedItemID");
            dtReqTerm.Columns.Add("QuotationToClientRevisedID");
            dtReqTerm.Columns.Add("TermsID");
            dtReqTerm.Columns.Add("TermsCode");
            dtReqTerm.Columns.Add("TermsName");
            foreach (var item in woInfoTerm)
            {
                DataRow objDataRow = dtReqTerm.NewRow();
                //objDataRow[0] = item.QuotationToClientRevisedItemID;
                objDataRow[0] = item.QuotationToClientRevisedID;
                objDataRow[1] = item.TermsID;
                objDataRow[2] = item.TermsCode;
                objDataRow[3] = item.TermsName;
                dtReqTerm.Rows.Add(objDataRow);
            }

            if (string.IsNullOrEmpty(woInfo.QuotationToClientRevisedID))
                woInfo.Action = "add";
            else
                woInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetQuotationToClientRevised"))
                {

                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@QuotationToClientRevisedID", SqlDbType.NVarChar, woInfo.QuotationToClientRevisedID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, woInfo.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientQutnAprvID", SqlDbType.NVarChar, woInfo.ClientQutnAprvID);
                    db.AddInParameter(dbCommandWrapper, "@RevisionNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.RevisionNo));
                    db.AddInParameter(dbCommandWrapper, "@RevisionDate", SqlDbType.NVarChar, woInfo.RevisionDate);
                    db.AddInParameter(dbCommandWrapper, "@RevisionAmt", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.RevisionAmt));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(woInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, woInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_QuotationToClientRevisedItem", SqlDbType.Structured, dtReqItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_QuotationToClientRevisedTerm", SqlDbType.Structured, dtReqTerm);
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

        public List<OBS_QuotationToClientRevised> GetQuotationToClientRevisedSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevised> QuotationToClientRevisedList = new List<OBS_QuotationToClientRevised>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetQuotationToClientRevisedSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    QuotationToClientRevisedList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevised
                    {
                        QuotationToClientRevisedID = reader.GetString("QuotationToClientRevisedID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        RevisionNo = reader.GetString("RevisionNo"),
                        RevisionDate = reader.GetString("RevisionDate"),
                        RevisionAmt = reader.GetString("RevisionAmt"),
                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return QuotationToClientRevisedList;
        }



        public List<OBS_QuotationToClientRevisedItem> GetQuotationToClientRevisedSearchItemList(string QuotationToClientRevisedID)
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetQuotationToClientRevisedSearchItemList"))
            {
                db.AddInParameter(dbCommandWrapper, "@QuotationToClientRevisedID", SqlDbType.VarChar, QuotationToClientRevisedID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    QuotationToClientRevisedItemList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevisedItem
                    {

                        QuotationToClientRevisedItemID = reader.GetString("QuotationToClientRevisedItemID"),
                        QuotationToClientRevisedID = reader.GetString("QuotationToClientRevisedID"),
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
            return QuotationToClientRevisedItemList;
        }

        public List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedSearchTermsList(string QuotationToClientRevisedID)
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetQuotationToClientRevisedSearchTermsList"))
            {
                db.AddInParameter(dbCommandWrapper, "@QuotationToClientRevisedID", SqlDbType.VarChar, QuotationToClientRevisedID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    QuotationToClientRevisedItemList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevisedTerms
                    {
                        QuotationToClientRevisedTermID = reader.GetString("QuotationToClientRevisedTermID"),
                        QuotationToClientRevisedID = reader.GetString("QuotationToClientRevisedID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return QuotationToClientRevisedItemList;
        }

        public List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedItemList = new List<OBS_QuotationToClientRevisedTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetQuotationToClientRevisedTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    QuotationToClientRevisedItemList = dt1.AsEnumerable().Select(reader => new OBS_QuotationToClientRevisedTerms
                    {
                        //QuotationToClientRevisedTermID = reader.GetString("QuotationToClientRevisedTermID"),
                        //QuotationToClientRevisedID = reader.GetString("QuotationToClientRevisedID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return QuotationToClientRevisedItemList;
        }
        public string DeleteQuotationToClientRevisedItemAndTerm(string QuotationToClientRevisedItemID, string QuotationToClientRevisedTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@QuotationToClientRevisedItemID", SqlDbType.NVarChar, QuotationToClientRevisedItemID);
                    db.AddInParameter(dbCommandWrapper, "@QuotationToClientRevisedTermID", SqlDbType.NVarChar, QuotationToClientRevisedTermID);
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
