using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model.CBPSModule;
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
    public class TermsDataService: ITermsDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public string SaveTermsMst(OBS_Terms modelTermsInfoMst)
        {

            if (string.IsNullOrEmpty(modelTermsInfoMst.TermsID))
                modelTermsInfoMst.Action = "add";
            else
                modelTermsInfoMst.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetTerms"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.NVarChar, modelTermsInfoMst.TermsID);
                    //db.AddInParameter(dbCommandWrapper, "@FormCode", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelTermsInfoMst.FormCode));
                    db.AddInParameter(dbCommandWrapper, "@FormName", SqlDbType.NVarChar, modelTermsInfoMst.FormName);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelTermsInfoMst.SetBy);
                    //db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelTermsInfoMst.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelTermsInfoMst.Status);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelTermsInfoMst.Action);
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

        public string SaveTermsItem(OBS_TermsItem modelTermsItem)
        {
            if (modelTermsItem.Action == "delete")
                modelTermsItem.Action = "delete";
            else
            {
                if (string.IsNullOrEmpty(modelTermsItem.TermsItemID))
                    modelTermsItem.Action = "add";
                else
                    modelTermsItem.Action = "edit";
            }
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetTermsItem"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@TermsItemID", SqlDbType.NVarChar, modelTermsItem.TermsItemID);
                    db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.NVarChar, modelTermsItem.TermsID);
                    //db.AddInParameter(dbCommandWrapper, "@TermsCode", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelTermsItem.TermsCode));
                    db.AddInParameter(dbCommandWrapper, "@TermsName", SqlDbType.NVarChar, modelTermsItem.TermsName);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelTermsItem.SetBy);
                    //db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelTermsItem.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelTermsItem.ItemStatus);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelTermsItem.Action);
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

        public List<OBS_Terms> GetTermsSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_Terms> TermsInfoSearchList = new List<OBS_Terms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetTerms"))
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
                    TermsInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_Terms
                    {
                        TermsID = reader.GetString("TermsID"),
                        FormCode = reader.GetString("FormCode"),
                        FormName = reader.GetString("FormName"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
                //}
            }
            return TermsInfoSearchList;
        }

        public List<OBS_TermsItem> GetTermsItemList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_TermsItem> TermsInfoSearchList = new List<OBS_TermsItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetTermsItem"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
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
                    TermsInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_TermsItem
                    {
                        TermsItemID = reader.GetString("TermsItemID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                        ItemStatus = reader.GetString("Status")
                    }).ToList();
                }
                //}
            }
            return TermsInfoSearchList;
        }

        public List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<Sys_MasterData> masterDataList = new List<Sys_MasterData>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_GetAllLocations"))
            {
                // Set parameters 

                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);
                // Execute SP.
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        masterDataList = dt1.AsEnumerable().Select(reader => new Sys_MasterData
                        {
                            //MasterDataID = reader.GetString("MasterDataID"),
                            //MasterDataValue = reader.GetString("MasterDataValue"),
                            //MasterDataTypeID = reader.GetString("MasterDataTypeID"),
                            //OwnerID = reader.GetString("OwnerID"),
                            //UserLevel = reader.GetString("UserLevel"),
                            //SetBy = reader.GetString("SetBy"),
                            //ModifiedBy = reader.GetString("ModifiedBy"),
                            //Status = reader.GetInt32("Status"),
                            //IsDefault = reader.GetString("IsDefault"),
                            //SerialNo = reader.GetString("SerialNo"),
                            //UDCode = reader.GetString("UDCode"),
                            //ServicesCategoryID = reader.GetString("ServicesCategoryID")

                        }).ToList();
                    }
                }
            }
            return masterDataList;
        }
    }
}

