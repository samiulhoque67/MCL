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
    public class ServiceItemInfoDataService : IServiceItemInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public string SaveServiceItemInfo(OBS_ServiceItemInfo modelServiceItemInfo)
        {
            if (modelServiceItemInfo.Action == "delete")
                modelServiceItemInfo.Action = "delete";
            else
            {
                if (string.IsNullOrEmpty(modelServiceItemInfo.ServiceItemID))
                    modelServiceItemInfo.Action = "add";
                else
                    modelServiceItemInfo.Action = "edit";
            }


            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetServiceItemInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.NVarChar, modelServiceItemInfo.ServiceItemID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceItemCode", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelServiceItemInfo.ServiceItemCode));
                    db.AddInParameter(dbCommandWrapper, "@ServiceItemName", SqlDbType.NVarChar, modelServiceItemInfo.ServiceItemName);
                    db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelServiceItemInfo.ServiceItemCategoryID));
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelServiceItemInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelServiceItemInfo.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelServiceItemInfo.Status);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelServiceItemInfo.Action);
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

        public List<OBS_ServiceItemInfo> GetServiceItemInfoSearchList(string ServicesCategoryID)
        {
            string errorNumber = string.Empty;
            List<OBS_ServiceItemInfo> ServiceItemInfoSearchList = new List<OBS_ServiceItemInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetServiceItemInfo"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.VarChar, ServicesCategoryID);
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
                    ServiceItemInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_ServiceItemInfo
                    {
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceItemCategoryID = reader.GetString("ServicesCategoryID"),
                        ServiceItemCategoryName = reader.GetString("ServicesCategoryName"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
                //}
            }
            return ServiceItemInfoSearchList;
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