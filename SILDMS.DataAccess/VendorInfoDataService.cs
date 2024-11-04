using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.DataAccessInterface;
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

namespace SILDMS.DataAccess
{
    public class VendorInfoDataService : IVendorInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public string SaveVendorInfoMst(OBS_VendorInfo modelVendorInfoMst)
        {

            if (string.IsNullOrEmpty(modelVendorInfoMst.VendorID))
                modelVendorInfoMst.Action = "add";
            else
                modelVendorInfoMst.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, modelVendorInfoMst.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@VendorCode", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorInfoMst.VendorCode));
                    db.AddInParameter(dbCommandWrapper, "@VendorName", SqlDbType.NVarChar, modelVendorInfoMst.VendorName);
                    db.AddInParameter(dbCommandWrapper, "@ServicesCategoryID", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorInfoMst.VendorCategoryID));
                    db.AddInParameter(dbCommandWrapper, "@VendorTinNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorInfoMst.VendorTinNo));
                    db.AddInParameter(dbCommandWrapper, "@VendorBinNo", SqlDbType.NVarChar, modelVendorInfoMst.VendorBinNo);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelVendorInfoMst.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelVendorInfoMst.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelVendorInfoMst.Status);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelVendorInfoMst.Action);
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

        public string SaveVendorAddress(OBS_VendorAddressInfo modelVendorAddress)
        {
            if (modelVendorAddress.Action == "delete")
                modelVendorAddress.Action = "delete";
            else
            {
                if (string.IsNullOrEmpty(modelVendorAddress.VendorAddressID))
                    modelVendorAddress.Action = "add";
                else
                    modelVendorAddress.Action = "edit";
            }
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorAddressID", SqlDbType.NVarChar, modelVendorAddress.VendorAddressID);
                    db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.NVarChar, modelVendorAddress.VendorID);
                    db.AddInParameter(dbCommandWrapper, "@ContactPerson", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorAddress.ContactPerson));
                    db.AddInParameter(dbCommandWrapper, "@ContactNumber", SqlDbType.NVarChar, modelVendorAddress.ContactNumber);
                    db.AddInParameter(dbCommandWrapper, "@PhoneNumber", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorAddress.PhoneNumber));
                    db.AddInParameter(dbCommandWrapper, "@Address", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(modelVendorAddress.Address));
                    db.AddInParameter(dbCommandWrapper, "@Email", SqlDbType.NVarChar, modelVendorAddress.Email);
                    db.AddInParameter(dbCommandWrapper, "@SetBy ", SqlDbType.NVarChar, modelVendorAddress.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@ModifiedBy", SqlDbType.NVarChar, modelVendorAddress.ModifiedBy);
                    db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.Int, modelVendorAddress.AddressStatus);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, modelVendorAddress.Action);
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

        public List<OBS_VendorInfo> GetVendorInfoSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorInfo> VendorInfoSearchList = new List<OBS_VendorInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorInfo"))
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
                    VendorInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_VendorInfo
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorCode = reader.GetString("VendorCode"),
                        VendorName = reader.GetString("VendorName"),
                        //VendorCategoryID = reader.GetString("ServicesCategoryID"),
                        //VendorCategoryName = reader.GetString("ServicesCategoryName"),
                        VendorTinNo = reader.GetString("VendorTinNo"),
                        VendorBinNo = reader.GetString("VendorBinNo"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        //PhoneNumber = reader.GetString("PhoneNumber"),
                        Address = reader.GetString("Address"),
                        Email = reader.GetString("Email"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
                //}
            }
            return VendorInfoSearchList;
        }

        public List<OBS_VendorAddressInfo> GetVendorAddressList(string VendorID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorAddressInfo> VendorInfoSearchList = new List<OBS_VendorAddressInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorAddress"))
            {
                // Set parameters 
                //db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.VarChar, userID);
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
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
                    VendorInfoSearchList = dt1.AsEnumerable().Select(reader => new OBS_VendorAddressInfo
                    {
                        VendorAddressID = reader.GetString("VendorAddressID"),
                        VendorID = reader.GetString("VendorID"),
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
            return VendorInfoSearchList;
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