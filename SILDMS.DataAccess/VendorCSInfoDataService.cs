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
using Ninject.Selection;

namespace SILDMS.DataAccess
{
    public class VendorCSInfoDataService : IVendorCSInfoDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorCSRecmItem> GetVendorCSInfoDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> servicesCategoryList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSInfoDashBord"))
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
                    servicesCategoryList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
                        ServiceCategoryName = reader.GetString("ServiceSCategoryName"),
                        ServicesCategoryCount = reader.GetString("ServicesCategoryCount")
                    }).ToList();
                }
                //    }
            }
            return servicesCategoryList;
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

        public List<OBS_ClientReq> GetVendorCSClientInfo(string ServiceCategoryID)
        {
            string errorNumber = string.Empty;
            List<OBS_ClientReq> VendorInfoList = new List<OBS_ClientReq>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSClientInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.VarChar, ServiceCategoryID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorQutnItemID = reader.GetString("VendorQutnItemID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        NoOfVendor = reader.GetString("NoOfVendor")
                    }).ToList();
                }
            }
            return VendorInfoList;
        }

        public List<OBS_VendorCSRecm> OBS_GetVendorCSVendorsUsingClient(string ClientID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSRecm> VendorCSInfoList = new List<OBS_VendorCSRecm>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSVendorsUsingClient"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecm
                    {
                        VendorQutnItemID = reader.GetString("VendorQutnItemID"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        QuotationDate = reader.GetString("QuotationDate"),
                        TolQnty = reader.GetString("QutnQnty")
                        //,
                        //LastDateofQuotation = reader.GetString("LastDateofQuotation"),
                        //Remarks = reader.GetString("Remarks"),
                        //Status = reader.GetString("Status")
                    }).ToList();
                }
            }
            return VendorCSInfoList;
        }

        public List<OBS_VendorCSRecmItem> OBS_GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorQutnItemID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSQuotationItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
                db.AddInParameter(dbCommandWrapper, "@VendorQutnItemID", SqlDbType.VarChar, VendorQutnItemID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
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
            return VendorCSInfoItemList;
        }

        public List<OBS_VendorCSRecmTerms> GetVendorCSInfoTermList(string VendorCSInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> VendorCSInfoItemList = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorQutnTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorQutnID", SqlDbType.VarChar, VendorCSInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        //VendorCSInfoTermID = reader.GetString("VendorCSInfoTermID"),
                        //VendorCSInfoID = reader.GetString("VendorCSInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }


        public string SaveVendorCSInfo(OBS_VendorCSRecm vendorCSInfo, List<OBS_VendorCSRecmItem> vendorCSInfoItem, List<OBS_VendorCSRecmTerms> vendorCSInfoTerm)
        {
            DataTable VendorCSItem = new DataTable();
            //VendorCSItem.Columns.Add("VendorReqID");
            VendorCSItem.Columns.Add("ServiceCategoryID");
            VendorCSItem.Columns.Add("ServiceItemID");
            VendorCSItem.Columns.Add("ReqQnty");
            VendorCSItem.Columns.Add("ReqUnit");
            VendorCSItem.Columns.Add("QutnQnty");
            VendorCSItem.Columns.Add("QutnPrice");
            VendorCSItem.Columns.Add("QutnUnit");
            VendorCSItem.Columns.Add("QutnAmt");
            VendorCSItem.Columns.Add("VatPerc");
            VendorCSItem.Columns.Add("VatAmt");
            VendorCSItem.Columns.Add("VendorID");
            VendorCSItem.Columns.Add("VendorQutnID");
            VendorCSItem.Columns.Add("TolQnty");
            VendorCSItem.Columns.Add("VendorName");
            foreach (var item in vendorCSInfoItem)
            {
                DataRow objDataRow = VendorCSItem.NewRow();
            
                objDataRow[0] = item.ServiceCategoryID;
                objDataRow[1] = item.ServiceItemID;
                objDataRow[2] = item.ReqQnty;
                objDataRow[3] = item.ReqUnit;
                objDataRow[4] = item.QutnQnty;
                objDataRow[5] = item.QutnPrice;
                objDataRow[6] = item.QutnUnit;
                objDataRow[7] = item.QutnAmt;
                objDataRow[8] = item.VatPerc;
                objDataRow[9] = item.VatAmt;
                objDataRow[10] = item.VendorID;
                objDataRow[11] = item.VendorQutnID;
                objDataRow[12] = item.TolAmt;
                objDataRow[13] = item.VendorName;
                VendorCSItem.Rows.Add(objDataRow);
            }

            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");
            foreach (var item in vendorCSInfoTerm)
            {
                DataRow objDataRow = VendorCSTerm.NewRow();
                objDataRow[0] = item.TermsID;
                objDataRow[1] = item.TermsCode;
                objDataRow[2] = item.TermsName;
                VendorCSTerm.Rows.Add(objDataRow);
            }

            //DataTable vendorCSVendors = new DataTable();
            //vendorCSVendors.Columns.Add("VendorID");
            //vendorCSVendors.Columns.Add("VendorQutnID");
            //vendorCSVendors.Columns.Add("TolQnty");
            //foreach (var item in vendorCSVendorsItemWise)
            //{
            //    DataRow objDataRow = vendorCSVendors.NewRow();
            //    objDataRow[0] = item.VendorID;
            //    objDataRow[1] = item.VendorQutnID;
            //    objDataRow[2] = item.TolQnty;
            //    vendorCSVendors.Rows.Add(objDataRow);
            //}

            if (string.IsNullOrEmpty(vendorCSInfo.VendorCSInfoID))
                vendorCSInfo.Action = "add";
            else
                vendorCSInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorCSInfo"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSRecmID", SqlDbType.NVarChar, vendorCSInfo.VendorCSInfoID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.BigInt, vendorCSInfo.ServiceCategoryID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.BigInt, vendorCSInfo.ServiceItemID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqNo", SqlDbType.NVarChar, vendorCSInfo.ClientReqNo);
                    db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.NVarChar, vendorCSInfo.VendorReqID);
                    db.AddInParameter(dbCommandWrapper, "@Description", SqlDbType.NVarChar, vendorCSInfo.Description);
                    db.AddInParameter(dbCommandWrapper, "@DeliveryDate", SqlDbType.NVarChar, vendorCSInfo.DeliveryDate);
                    db.AddInParameter(dbCommandWrapper, "@DeliveryLocation", SqlDbType.NVarChar, vendorCSInfo.DeliveryLocation);
                    db.AddInParameter(dbCommandWrapper, "@DeliveryMode", SqlDbType.NVarChar, vendorCSInfo.DeliveryMode);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.BigInt, vendorCSInfo.ClientID);
                    //db.AddInParameter(dbCommandWrapper, "@CSNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.CSNo));
                    db.AddInParameter(dbCommandWrapper, "@CSRecDate", SqlDbType.NVarChar, vendorCSInfo.CSRecDate);
                    db.AddInParameter(dbCommandWrapper, "@Operation", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Operation));
                    db.AddInParameter(dbCommandWrapper, "@RecommendedBy", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));

                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSRecmItem", SqlDbType.Structured, VendorCSItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSRecmTerms", SqlDbType.Structured, VendorCSTerm);
                    //db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSRecmVendors", SqlDbType.Structured, vendorCSVendors);
                    db.AddInParameter(dbCommandWrapper, "@Action", SqlDbType.VarChar, vendorCSInfo.Action);
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


        public List<OBS_VendorCSRecmTerms> GetVendorCSInfoTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> VendorCSInfoItemList = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSInfoTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        //VendorCSInfoTermID = reader.GetString("VendorCSInfoTermID"),
                        //VendorCSInfoID = reader.GetString("VendorCSInfoID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorCSInfoItemList;
        }

        public List<OBS_VendorCSRecmVendors> GetReqWiseVendorList(string VendorCSInfoID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSRecmVendors> VendorCSInfoItemList = new List<OBS_VendorCSRecmVendors>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetReqWiseVendorList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSInfoID", SqlDbType.VarChar, VendorCSInfoID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmVendors
                    {
                        VendorCSVendorsItemWiseID = reader.GetString("VendorCSInfoItemWiseID"),
                        VendorCSInfoItemID = reader.GetString("VendorCSInfoItemID"),
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
            return VendorCSInfoItemList;
        }

        public string DeleteVendorCSInfoItemAndTerm(string VendorCSInfoItemID, string VendorCSInfoTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSInfoItemID", SqlDbType.NVarChar, VendorCSInfoItemID);
                    db.AddInParameter(dbCommandWrapper, "@VendorCSInfoTermID", SqlDbType.NVarChar, VendorCSInfoTermID);
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

        public List<Invitation> GetAllRequisition(string userID)
        {
            var invitationList = new List<Invitation>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_VendorReqForCS"))
            {
                db.AddInParameter(dbCommandWrapper, "@UserId", SqlDbType.VarChar, userID);
                //db.AddOutParameter(dbCommandWrapper, "@p_Error", DbType.Int32, 10);
                // Execute SP.

                var ds = db.ExecuteDataSet(dbCommandWrapper);

               
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0];

                        invitationList = dt1.AsEnumerable().Select(reader => new Invitation
                        {
                            VendorRequisitionNumber = reader.GetString("VendorReqID"),
                            ClientRequisitionNumber = reader.GetString("ClientReqNo"),
                            ClientReqID = reader.GetString("ClientReqID"),
                            ClientID = reader.GetString("ClientID"),
                            ClientName = reader.GetString("ClientName"),
                            RequisitionDate = reader.GetString("RequisitionDate"),
                            LastDateofQuotation = reader.GetString("LastDateofQuotation")
                          


                        }).ToList();
                    
                }
            }
            return invitationList;
        }

        public List<OBS_VendorCSRecmItem> GetMaterialByRequisition(string vendorRequisitionNumber)
        {
            var ReqWiseMaterialList = new List<OBS_VendorCSRecmItem>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetMaterialByRequisition"))
            {
                db.AddInParameter(dbCommandWrapper, "@vendorRequisitionNumber", SqlDbType.VarChar, vendorRequisitionNumber);
              
                // Execute SP.

                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0];

                    ReqWiseMaterialList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
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
                          


                        }).ToList();
                    }
         
            }
            return ReqWiseMaterialList;
        }

        public List<OBS_VendorCSRecmItem> GetVendorByMaterialData(string vendorReqID, string serviceItemID)
        {
            List<OBS_VendorCSRecmItem> VendorCSInfoItemList = new List<OBS_VendorCSRecmItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_VendorDetailsForCS"))
            {
                db.AddInParameter(dbCommandWrapper, "@vendorReqID", SqlDbType.VarChar, vendorReqID);
                db.AddInParameter(dbCommandWrapper, "@serviceItemID", SqlDbType.VarChar, serviceItemID);
           
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmItem
                    {
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
                        VendorQutnNo = reader.GetString("VendorQutnNo"),
                        VendorQutnID = reader.GetString("VendorQutnID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        

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
            return VendorCSInfoItemList;
        }
    }
}
