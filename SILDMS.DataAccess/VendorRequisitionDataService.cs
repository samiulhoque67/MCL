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
    public class VendorRequisitionDataService : IVendorRequisitionDataService 
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

        public List<OBS_VendorAndAddressInfo> GetVendorInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorAndAddressInfo> VendorInfoList = new List<OBS_VendorAndAddressInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorAndAddressInfo"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorAndAddressInfo
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorCode = reader.GetString("VendorCode"),
                        VendorName = reader.GetString("VendorName"),
                        VendorCategoryID = reader.GetString("ServicesCategoryID"),
                        VendorCategoryName = reader.GetString("ServicesCategoryName"),
                        VendorTinNo = reader.GetString("VendorTinNo"),
                        VendorBinNo = reader.GetString("VendorBinNo"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),

                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_ClientReq> GetClientReqInfoList()
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        //VendorTinNo = reader.GetString("VendorTinNo"),
                        //VendorBinNo = reader.GetString("VendorBinNo"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_ClientReq> GetClientListForVendorRequisition()
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientListForVendorRequisition"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public string SaveVendorRequisition(OBS_VendorReq VendorReq, List<OBS_VendorReqItem> VendorReqItem, List<OBS_VendorReqTerms> VendorReqTerm, List<OBS_VendorReqItemWise> vendorReqItemWise)
        {
            DataTable dtReqItem = new DataTable();
            //dtReqItem.Columns.Add("VendorReqItemID");
            dtReqItem.Columns.Add("VendorReqID");
            dtReqItem.Columns.Add("ServiceCategoryID");
            dtReqItem.Columns.Add("ServiceItemID");
            dtReqItem.Columns.Add("Description");
            dtReqItem.Columns.Add("DeliveryLocation");
            dtReqItem.Columns.Add("DeliveryDate");
            dtReqItem.Columns.Add("DeliveryMode");
            dtReqItem.Columns.Add("ReqQnty");
            dtReqItem.Columns.Add("ReqUnit");
            dtReqItem.Columns.Add("ClientReqItemID");
            foreach (var item in VendorReqItem)
            {
                DataRow objDataRow = dtReqItem.NewRow();
                //objDataRow[0] = item.VendorReqItemID;
                objDataRow[0] = item.VendorReqID;
                objDataRow[1] = item.ServiceCategoryID;
                objDataRow[2] = item.ServiceItemID;
                objDataRow[3] = item.Description;
                objDataRow[4] = item.DeliveryLocation;
                objDataRow[5] = item.DeliveryDate;
                objDataRow[6] = item.DeliveryMode;
                objDataRow[7] = item.ReqQnty;
                objDataRow[8] = item.ReqUnit;
                objDataRow[9] = item.ClientReqItemID;
                dtReqItem.Rows.Add(objDataRow);
            }

            DataTable dtReqTerm = new DataTable();
            //dtReqItem.Columns.Add("VendorReqItemID");
            dtReqTerm.Columns.Add("VendorReqID");
            dtReqTerm.Columns.Add("TermsID");
            dtReqTerm.Columns.Add("TermsCode");
            dtReqTerm.Columns.Add("TermsName");
            foreach (var item in VendorReqTerm)
            {
                DataRow objDataRow = dtReqTerm.NewRow();
                //objDataRow[0] = item.VendorReqItemID;
                objDataRow[0] = item.VendorReqID;
                objDataRow[1] = item.TermsID;
                objDataRow[2] = item.TermsCode;
                objDataRow[3] = item.TermsName;
                dtReqTerm.Rows.Add(objDataRow);
            }

            DataTable dtVenReqItemWise = new DataTable();
            dtVenReqItemWise.Columns.Add("VendorReqItemID");
            dtVenReqItemWise.Columns.Add("VendorID");
            dtVenReqItemWise.Columns.Add("VenReqQnty");
            dtVenReqItemWise.Columns.Add("VenReqUnit");
            dtVenReqItemWise.Columns.Add("ServiceItemID");
            foreach (var item in vendorReqItemWise)
            {
                DataRow objDataRow = dtVenReqItemWise.NewRow();
                objDataRow[0] = item.VendorReqItemID;
                objDataRow[1] = item.VendorID;
                objDataRow[2] = item.VenReqQnty;
                objDataRow[3] = item.VenReqUnit;
                objDataRow[4] = item.ServiceItemID;

                dtVenReqItemWise.Rows.Add(objDataRow);
            }


            if (string.IsNullOrEmpty(VendorReq.VendorReqID))
                VendorReq.Action = "add";
            else
                VendorReq.Action = "edit";
            string errorNumber = String.Empty;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorRequisition"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.NVarChar, VendorReq.VendorReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, VendorReq.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, VendorReq.ClientID);

                    db.AddInParameter(dbCommandWrapper, "@CsStatus", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorReq.CsStatus));
                    db.AddInParameter(dbCommandWrapper, "@RequisitionDate", SqlDbType.NVarChar, VendorReq.RequisitionDate);
                    db.AddInParameter(dbCommandWrapper, "@SubmissionDate", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorReq.SubmissionDate));
                    db.AddInParameter(dbCommandWrapper, "@LastDateofQuotation", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorReq.LastDateofQuotation));
                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(VendorReq.Remarks));

                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, VendorReq.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorReqItem", SqlDbType.Structured, dtReqItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorReqTerm", SqlDbType.Structured, dtReqTerm);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorReqItemWise", SqlDbType.Structured, dtVenReqItemWise);
                    db.AddInParameter(dbCommandWrapper, "@Action ", SqlDbType.NVarChar, VendorReq.Action);
                    db.AddInParameter(dbCommandWrapper, "@TolalItem", SqlDbType.Int, VendorReq.TolalItem);
                    db.AddInParameter(dbCommandWrapper, "@SelectedItem", SqlDbType.Int, VendorReq.SelectedItem);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 50);
                    //db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 10);
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
        public DataSet rptRequisitionToVendorReport(string userID,string action)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetRptRequisitionToVendorReport"))
            {
                //db.AddInParameter(dbCommandWrapper, "@RptUserID", SqlDbType.VarChar, userID);
                //db.AddInParameter(dbCommandWrapper, "@FromDate", SqlDbType.VarChar, FromDate);
                ////db.AddInParameter(dbCommandWrapper, "@ToDate", SqlDbType.VarChar, ToDate);
                //db.AddInParameter(dbCommandWrapper, "@Status", SqlDbType.VarChar, Status);
                ////db.AddOutParameter(dbCommandWrapper, spErrorParam, DbType.Int32, 10);

                var ds = db.ExecuteDataSet(dbCommandWrapper);
                //DataTable dt1 = ds.Tables[0];
                return ds;
            }
        }

        public List<OBS_VendorReq> GetVendorReqSearchList()
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReq> VendorReqList = new List<OBS_VendorReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorReqSearchList"))
            {
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqList = dt1.AsEnumerable().Select(reader => new OBS_VendorReq
                    {
                        VendorReqID = reader.GetString("VendorReqID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        RequisitionNo = reader.GetString("RequisitionNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        SubmissionDate = reader.GetString("SubmissionDate"),
                        LastDateofQuotation = reader.GetString("LastDateofQuotation"),
                        Remarks = reader.GetString("Remarks"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorReqList;
        }

        public List<OBS_VendorInfo> GetVendorWiseItemList(string ServiceItemID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorInfo> VendorReqList = new List<OBS_VendorInfo>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetVendorWiseItemList"))
            {
                db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.VarChar, ServiceItemID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqList = dt1.AsEnumerable().Select(reader => new OBS_VendorInfo
                    {
                        VendorID = reader.GetString("VendorID"),
                        VendorWiseItemID = reader.GetString("VendorWiseItemID"),
                        VendorCode = reader.GetString("VendorCode"),
                        VendorName = reader.GetString("VendorName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        //VendorCategoryName = reader.GetString("ServicesCategoryName"),
                        VendorTinNo = reader.GetString("VendorTinNo"),
                        VendorBinNo = reader.GetString("VendorBinNo"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),
                        Email = reader.GetString("Email"),
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorReqList;
        }

        public List<OBS_VendorReqItem> GetVendorReqItemList(string VendorReqID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReqItem> VendorReqItemList = new List<OBS_VendorReqItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorReqItemList"))
            {
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
                        Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorReqItemList;
        }

        public List<OBS_VendorReqTerms> GetVendorReqTermList(string VendorReqID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReqTerms> VendorReqItemList = new List<OBS_VendorReqTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorReqTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorReqTerms
                    {
                        VendorReqTermID = reader.GetString("VendorReqTermID"),
                        VendorReqID = reader.GetString("VendorReqID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorReqItemList;
        }

        public List<OBS_VendorReqTerms> GetVendorReqTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReqTerms> VendorReqItemList = new List<OBS_VendorReqTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorReqTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorReqTerms
                    {
                        //VendorReqTermID = reader.GetString("VendorReqTermID"),
                        //VendorReqID = reader.GetString("VendorReqID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorReqItemList;
        }

        public List<OBS_VendorReqItemWise> GetReqWiseVendorList(string VendorReqID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorReqItemWise> VendorReqItemList = new List<OBS_VendorReqItemWise>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetReqWiseVendorList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.VarChar, VendorReqID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorReqItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorReqItemWise
                    {
                        VendorReqItemWiseID = reader.GetString("VendorReqItemWiseID"),
                        VendorReqItemID = reader.GetString("VendorReqItemID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),

                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Email = reader.GetString("Email"),
                        Address = reader.GetString("Address"),

                        VenReqQnty = reader.GetString("VenReqQnty"),
                        VenReqUnit = reader.GetString("VenReqUnit")
                    }).ToList();
                }
            }
            return VendorReqItemList;
        }

        public string DeleteVendorReqItemAndTerm(string VendorReqItemID, string VendorReqTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorReqItemID", SqlDbType.NVarChar, VendorReqItemID);
                    db.AddInParameter(dbCommandWrapper, "@VendorReqTermID", SqlDbType.NVarChar, VendorReqTermID);
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