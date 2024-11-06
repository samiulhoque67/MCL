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
using System.Runtime.Remoting.Messaging;
using System.Data.SqlClient;

namespace SILDMS.DataAccess
{
    public class VendorInfoDataService : IVendorInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public string SaveVendorwithMatDataService(string UserID, string VendorCode, string VendorName, string ContactPerson, string ContactNumber, string Email,
            string VendorTinNo, string VendorBinNo, string VAddress, List<OBS_ServicesCategory> ServiceItemInfo, int VendorStatus, string TempVendorID, out string errorNumber)
        {
            errorNumber = string.Empty;
            string message = "";

           
            // ################# Detail Data ####################

            DataTable detailDataTable = new DataTable();

            detailDataTable.Columns.Add("ServiceItemID", typeof(string));
            detailDataTable.Columns.Add("ServiceItemName", typeof(string));

            if (ServiceItemInfo != null)
            {
                foreach (var item in ServiceItemInfo)
                {
                    DataRow objDataRow = detailDataTable.NewRow();
                    objDataRow["ServiceItemID"] = string.IsNullOrEmpty(item.ServiceItemID) ? DBNull.Value : (object)item.ServiceItemID;
                    objDataRow["ServiceItemName"] = string.IsNullOrEmpty(item.ServiceItemName) ? DBNull.Value : (object)item.ServiceItemName;
                    
                    detailDataTable.Rows.Add(objDataRow);
                }
            }


            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SaveVendorWiseSer"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCode", SqlDbType.VarChar, VendorCode);
                db.AddInParameter(dbCommandWrapper, "@VendorName", SqlDbType.VarChar, VendorName);
                db.AddInParameter(dbCommandWrapper, "@ContactPerson", SqlDbType.VarChar, ContactPerson);
                db.AddInParameter(dbCommandWrapper, "@ContactNumber", SqlDbType.VarChar, ContactNumber);
                db.AddInParameter(dbCommandWrapper, "@Email", SqlDbType.VarChar, Email);
                db.AddInParameter(dbCommandWrapper, "@VendorTinNo", SqlDbType.VarChar, VendorTinNo);
                db.AddInParameter(dbCommandWrapper, "@VendorBinNo", SqlDbType.VarChar, VendorBinNo);
                db.AddInParameter(dbCommandWrapper, "@VAddress", SqlDbType.VarChar, VAddress);

                db.AddInParameter(dbCommandWrapper, "@OBS_QtC_ServiceItemInfolType", SqlDbType.Structured, detailDataTable);
                db.AddInParameter(dbCommandWrapper, "@VendorStatus", SqlDbType.Int, VendorStatus);
                db.AddInParameter(dbCommandWrapper, "@TempVendorID", SqlDbType.VarChar, TempVendorID);
                db.AddInParameter(dbCommandWrapper, "@SetBy", SqlDbType.VarChar, UserID);
                db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.String, 1200);


                try
                {
                    var ds = db.ExecuteDataSet(dbCommandWrapper);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var dt = ds.Tables[0];
                        var dr = dt.Rows[0];

                        if (dr["Status"].ToString() == "Successfully Submitted")
                        {
                            message = "Success";
                        }
                        else
                        {
                            message = "Error Found";
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Log SQL exception details
                    errorNumber = sqlEx.Number.ToString();
                    message = "Database error occurred: " + sqlEx.Message;
                    Console.WriteLine("SQL Exception: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Log general exception details
                    errorNumber = "General Exception";
                    message = "An error occurred: " + ex.Message;
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            return message;
        }


        public List<OBS_ServicesCategory> GetAllMaterialData(out string errorNumber)
        {
            errorNumber = string.Empty;
            var materialList = new List<OBS_ServicesCategory>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAllServicesCategoryList"))
            {
                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10); // Correct parameter name here
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];

                        materialList = dt1.AsEnumerable().Select(reader => new OBS_ServicesCategory
                        {
                            ServicesCategoryCode = reader.GetString("ServicesCategoryCode"),
                            ServicesCategoryName = reader.GetString("ServicesCategoryName"),
                            ServicesCategoryID = reader.GetString("ServicesCategoryID"),
                            ServiceItemName = reader.GetString("ServiceItemName"),
                            ServiceItemCode = reader.GetString("ServiceItemCode"),
                            ServiceItemID = reader.GetString("ServiceItemID")
                        }).ToList();
                    }
                }
            }
            return materialList;
        }

        public List<OBS_VendorInfo> GetAllListedVendorsDataService(string UserID, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var ListedVendorsList = new List<OBS_VendorInfo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("VCMS_GetAllListedVendor"))
            {
                db.AddInParameter(dbCommandWrapper, "@page", SqlDbType.Int, page);
                db.AddInParameter(dbCommandWrapper, "@itemsPerPage", SqlDbType.Int, itemsPerPage);
                db.AddInParameter(dbCommandWrapper, "@sortBy", SqlDbType.NVarChar, sortBy);
                db.AddInParameter(dbCommandWrapper, "@reverse", SqlDbType.Int, reverse ? 1 : 0);
                db.AddInParameter(dbCommandWrapper, "@search", SqlDbType.NVarChar, search);
                db.AddInParameter(dbCommandWrapper, "@type", SqlDbType.NVarChar, type.ToString());
                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        ListedVendorsList = dt1.AsEnumerable().Select(reader => new OBS_VendorInfo
                        {
                            VendorCode = reader.GetString("VendorCode"),
                            VendorID = reader.GetString("VendorID"),
                            VendorName = reader.GetString("VendorName"),
                            ContactPerson = reader.GetString("ContactPerson"),
                            VendorTinNo = reader.GetString("VendorTinNo"),
                            VendorBinNo = reader.GetString("VendorBinNo"),
                            Email = reader.GetString("Email"),
                            ContactNumber = reader.GetString("ContactNumber"),
                            Address = reader.GetString("CurrentAddress"),
                            ServiceItemName = reader.GetString("ServiceItemName"),
                            ServiceItemID = reader.GetString("ServiceItemID"),
                            Status = reader.GetString("Status"),
                            TotalPages = reader.GetString("TotalPages")
                        }).ToList();
                    }
                }
            }

            return ListedVendorsList;
        }



    }
}