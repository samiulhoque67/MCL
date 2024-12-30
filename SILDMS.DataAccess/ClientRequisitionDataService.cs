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
using SILDMS.DataAccessInterface;
using SILDMS.Utillity;
using Ninject.Selection;

namespace SILDMS.DataAccess
{
    public class ClientRequisitionDataService : IClientRequisitionDataService
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

        public List<OBS_ClientAndAddressInfo> GetClientInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_ClientAndAddressInfo> ClientInfoList = new List<OBS_ClientAndAddressInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientAndAddressInfo"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientAndAddressInfo
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        //ClientCategoryID = reader.GetString("ServicesCategoryID"),
                        ClientType = reader.GetString("ClientType"),
                        //ClientTinNo = reader.GetString("ClientTinNo"),
                        //ClientBinNo = reader.GetString("ClientBinNo"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),

                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return ClientInfoList;
        }

        public string SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm)
        {
            DataTable dtReqItem = new DataTable();
            //dtReqItem.Columns.Add("ClientReqItemID");
            dtReqItem.Columns.Add("ClientReqID");
            dtReqItem.Columns.Add("ServiceCategoryID");
            dtReqItem.Columns.Add("ServiceItemID");
            dtReqItem.Columns.Add("Description");
            dtReqItem.Columns.Add("DeliveryLocation");
            dtReqItem.Columns.Add("DeliveryDate");
            dtReqItem.Columns.Add("DeliveryMode");
            dtReqItem.Columns.Add("ReqType");
            dtReqItem.Columns.Add("ReqQnty");
            dtReqItem.Columns.Add("ReqUnit");
            foreach (var item in clientReqItem)
            {
                DataRow objDataRow = dtReqItem.NewRow();
                //objDataRow[0] = item.ClientReqItemID;
                objDataRow[0] = item.ClientReqID;
                objDataRow[1] = item.ServiceCategoryID;
                objDataRow[2] = item.ServiceItemID;
                objDataRow[3] = item.Description;
                objDataRow[4] = item.DeliveryLocation;
                objDataRow[5] = item.DeliveryDate;
                objDataRow[6] = item.DeliveryMode;
                objDataRow[7] = item.ReqType;
                objDataRow[8] = item.ReqQnty;
                objDataRow[9] = item.ReqUnit;
                dtReqItem.Rows.Add(objDataRow);
            }

            DataTable dtReqTerm = new DataTable();
            //dtReqItem.Columns.Add("ClientReqItemID");
            dtReqTerm.Columns.Add("ClientReqID");
            dtReqTerm.Columns.Add("TermsID");
            dtReqTerm.Columns.Add("TermsCode");
            dtReqTerm.Columns.Add("TermsName");
            foreach (var item in clientReqTerm)
            {
                DataRow objDataRow = dtReqTerm.NewRow();
                //objDataRow[0] = item.ClientReqItemID;
                objDataRow[0] = item.ClientReqID;
                objDataRow[1] = item.TermsID;
                objDataRow[2] = item.TermsCode;
                objDataRow[3] = item.TermsName;
                dtReqTerm.Rows.Add(objDataRow);
            }

            if (string.IsNullOrEmpty(clientReq.ClientReqID))
                clientReq.Action = "add";
            else
                clientReq.Action = "edit";
            string errorNumber = String.Empty;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientRequisition"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, clientReq.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, clientReq.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(clientReq.ClientReqNo));
                    db.AddInParameter(dbCommandWrapper, "@RequisitionDate", SqlDbType.NVarChar, clientReq.RequisitionDate);
                    db.AddInParameter(dbCommandWrapper, "@SubmissionDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(clientReq.SubmissionDate));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(clientReq.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, clientReq.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_ClientReqItem", SqlDbType.Structured, dtReqItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_ClientReqTerm", SqlDbType.Structured, dtReqTerm);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, clientReq.Action);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 50);
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

        public List<OBS_ClientReq> GetClientReqSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReq> ClientReqList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientReqList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        SubmissionDate = reader.GetString("SubmissionDate"),
                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return ClientReqList;
        }

        public List<OBS_ClientReqItem> GetClientReqItemList(string ClientReqID, string ReqType)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReqItem> ClientReqItemList = new List<OBS_ClientReqItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqItemList"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@ReqType", SqlDbType.VarChar, ReqType);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientReqItemList = dt1.AsEnumerable().Select(reader => new OBS_ClientReqItem
                    {
                        ClientReqItemID = reader.GetString("ClientReqItemID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServicesCategoryName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        ReqType = reader.GetString("ReqType"),
                        ReqQnty = reader.GetString("ReqQnty"),
                        ReqUnit = reader.GetString("ReqUnit"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return ClientReqItemList;
        }

        public List<OBS_ClientReqTerms> GetClientReqTermList(string ClientReqID)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReqTerms> ClientReqItemList = new List<OBS_ClientReqTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.VarChar, ClientReqID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientReqItemList = dt1.AsEnumerable().Select(reader => new OBS_ClientReqTerms
                    {
                        ClientReqTermID = reader.GetString("ClientReqTermID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return ClientReqItemList;
        }

        public List<OBS_ClientReqTerms> GetClientReqTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReqTerms> ClientReqItemList = new List<OBS_ClientReqTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    ClientReqItemList = dt1.AsEnumerable().Select(reader => new OBS_ClientReqTerms
                    {
                        //ClientReqTermID = reader.GetString("ClientReqTermID"),
                        //ClientReqID = reader.GetString("ClientReqID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return ClientReqItemList;
        }
        public string DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientReqItemID", SqlDbType.NVarChar, ClientReqItemID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqTermID", SqlDbType.NVarChar, ClientReqTermID);
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
