using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccessInterface;
using System.Globalization;
using SILDMS.Model.DocScanningModule;
using SILDMS.Utillity;
using static System.Collections.Specialized.BitVector32;

namespace SILDMS.DataAccess
{
    public class ClientInfoDataService : IClientInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public string SaveClientInfoMst(OBS_ClientInfo modelClientInfoMst)
        {

            if (string.IsNullOrEmpty(modelClientInfoMst.ClientID))
                modelClientInfoMst.Action = "add";
            else
                modelClientInfoMst.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, modelClientInfoMst.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ClientCode", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelClientInfoMst.ClientCode));
                    db.AddInParameter(dbCommandWrapper, "@ClientName", SqlDbType.NVarChar, modelClientInfoMst.ClientName);
                    db.AddInParameter(dbCommandWrapper, "@ClientType", SqlDbType.NVarChar,modelClientInfoMst.ClientType);
                    //db.AddInParameter(dbCommandWrapper, "@ClientTinNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelClientInfoMst.ClientTinNo));
                    //db.AddInParameter(dbCommandWrapper, "@ClientBinNo", SqlDbType.NVarChar, modelClientInfoMst.ClientBinNo);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelClientInfoMst.SetBy);
                    //db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelClientInfoMst.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelClientInfoMst.Status);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelClientInfoMst.Action);
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

        public string SaveClientAddress(OBS_ClientAddressInfo modelClientAddress)
        {
            if (modelClientAddress.Action == "delete")
                modelClientAddress.Action = "delete";
            else
            {
                if (string.IsNullOrEmpty(modelClientAddress.ClientAddressID))
                    modelClientAddress.Action = "add";
                else
                    modelClientAddress.Action = "edit";
            }
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetClientAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@ClientAddressID", SqlDbType.NVarChar, modelClientAddress.ClientAddressID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, modelClientAddress.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@ContactPerson", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelClientAddress.ContactPerson));
                    db.AddInParameter(dbCommandWrapper, "@ContactNumber", SqlDbType.NVarChar, modelClientAddress.ContactNumber);
                    db.AddInParameter(dbCommandWrapper, "@PhoneNumber", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelClientAddress.PhoneNumber));

                    db.AddInParameter(dbCommandWrapper, "@ClientTinNo", SqlDbType.NVarChar, modelClientAddress.ClientTinNo);
                    db.AddInParameter(dbCommandWrapper, "@ClientBinNo", SqlDbType.NVarChar, modelClientAddress.ClientBinNo);

                    db.AddInParameter(dbCommandWrapper, "@Address", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelClientAddress.Address));
                    db.AddInParameter(dbCommandWrapper, "@Email", SqlDbType.NVarChar, modelClientAddress.Email);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelClientAddress.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelClientAddress.AddressStatus);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelClientAddress.Action);
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

        public List<OBS_ClientInfo> GetClientInfoSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_ClientInfo> ClientInfoSearchList = new List<OBS_ClientInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientInfo"))
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
                    ClientInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_ClientInfo
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientName = reader.GetString("ClientName"),
                        //ClientCategoryID = reader.GetString("ServicesCategoryID"),
                        ClientType = reader.GetString("ClientType"),
                        //ClientTinNo = reader.GetString("ClientTinNo"),
                        //ClientBinNo = reader.GetString("ClientBinNo"),
                        Status = reader.GetString("Status")
                        //SetOn = reader.GetString("SetOn"),
                        //SetBy = reader.GetString("SetBy"),
                        //ModifiedOn = reader.GetString("ModifiedOn"),
                        //ModifiedBy = reader.GetString("ModifiedBy"),
                        //Status = reader.GetInt32("Status")
                    }).ToList();
                }
                //}
            }
            return ClientInfoSearchList;
        }

        public List<OBS_ClientAddressInfo> GetClientAddressList(string ClientID)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientAddressInfo> ClientInfoSearchList = new List<OBS_ClientAddressInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientAddress"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
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
                    ClientInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_ClientAddressInfo
                    {
                        ClientAddressID = reader.GetString("ClientAddressID"),
                        ClientID = reader.GetString("ClientID"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        PhoneNumber = reader.GetString("PhoneNumber"),
                        Address = reader.GetString("Address"),
                        Email = reader.GetString("Email"),
                        AddressStatus = reader.GetString("Status")
                        //AddressStatus = reader.GetInt32("Status")
                    }).ToList();
                }
                //}
            }
            return ClientInfoSearchList;
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
