using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model.CBPSModule;
using SILDMS.Model.DocScanningModule;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccessInterface;
using SILDMS.Model;

namespace SILDMS.DataAccess
{
    public class ServicesCategoryDataService : IServicesCategoryDataService 
    {
        private readonly string spStatusParam = "@p_Status";
        public List<OBS_ServicesCategory> GetServicesCategory(string ServicesCategoryId, string userID, out string CRUDNumber)
        {
            CRUDNumber = string.Empty;
            List<OBS_ServicesCategory> servicesCategoryList = new List<OBS_ServicesCategory>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetServicesCategory"))
            {
                // Set parameters 
                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.VarChar, ServicesCategoryId);
                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    // Get the error number, if error occurred.
                    CRUDNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
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
                            //LevelAccess = reader.GetString("LevelAccess"),
                            //LevelSL = reader.GetString("LevelSL"),
                            //UserLevel = reader.GetInt32("UserLevel"),
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

        public string AddServicesCategory(OBS_ServicesCategory servicesCategory, string action, out string CRUDNumber)
        {
            CRUDNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetServicesCategory"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.NVarChar, servicesCategory.ServicesCategoryID);
                    db.AddInParameter(dbCommandWrapper, "@ServicesCategoryCode", SqlDbType.NVarChar, servicesCategory.ServicesCategoryCode.Trim());
                    db.AddInParameter(dbCommandWrapper, "@ServicesCategoryName", SqlDbType.NVarChar, servicesCategory.ServicesCategoryName.Trim());
                    //db.AddInParameter(dbCommandWrapper, "@LevelSL", SqlDbType.NVarChar, servicesCategory.LevelSL);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, servicesCategory.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, servicesCategory.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, servicesCategory.Status);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, action);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 10);
                    // Execute SP.
                    db.ExecuteNonQuery(dbCommandWrapper);
                    // Getting output parameters and setting response details.
                    if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                    {
                        // Get the error number, if error occurred.
                        CRUDNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                    }
                }
            }
            catch (Exception ex)
            {
                CRUDNumber = "E404"; // Log ex.Message  Insert Log Table               
            }
            return CRUDNumber;
        }

        public List<Sys_MasterData> GetJobLocation(string UserID, out string CRUDNumber)
        {
            CRUDNumber = string.Empty;
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
                    CRUDNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
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