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
    public class VendorCSAprvDataService : IVendorCSAprvDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_VendorCSAprvItem> GetVendorCSAprvDashBordData(string userID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSAprvItem> servicesCategoryList = new List<OBS_VendorCSAprvItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSAprvDashBord"))
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
                    servicesCategoryList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvItem
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
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSAprvClientInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.VarChar, ServiceCategoryID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorInfoList = dt1.AsEnumerable().Select(reader => new OBS_ClientReq
                    {
                        VendorCSRecmID = reader.GetString("VendorCSRecmID"),
                        VendorCSRecmItemID = reader.GetString("VendorCSRecmItemID"),
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

        public List<OBS_VendorCSAprv> GetVendorCSVendorsUsingClient(string ClientID, string VendorCSRecmID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSAprv> VendorCSAprvList = new List<OBS_VendorCSAprv>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSAprvVendorsUsingClient"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
                db.AddInParameter(dbCommandWrapper, "@VendorCSRecmID", SqlDbType.VarChar, VendorCSRecmID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSAprvList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprv
                    {
                        VendorCSRecmID = reader.GetString("VendorCSRecmID"),
                        VendorCSRecmItemID = reader.GetString("VendorCSRecmItemID"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        CSNo = reader.GetString("CSNo"),
                        CSRecDate = reader.GetString("CSRecDate"),
                        TolQnty = reader.GetString("QutnQnty")
                    }).ToList();
                }
            }
            return VendorCSAprvList;
        }

        public List<OBS_VendorCSAprvItem> GetVendorCSQuotationItem(string VendorID, string ClientID, string VendorCSRecmItemID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSAprvItem> VendorCSAprvItemList = new List<OBS_VendorCSAprvItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSAprvQuotationItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.VarChar, ClientID);
                db.AddInParameter(dbCommandWrapper, "@VendorCSRecmItemID", SqlDbType.VarChar, VendorCSRecmItemID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSAprvItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvItem
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
                    }).ToList();
                }
            }
            return VendorCSAprvItemList;
        }

        public List<OBS_VendorCSAprvTerms> GetVendorCSAprvTermList(string VendorCSRecmID, string VendorID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSAprvTerms> VendorCSAprvItemList = new List<OBS_VendorCSAprvTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSRecmID", SqlDbType.VarChar, VendorCSRecmID);
                db.AddInParameter(dbCommandWrapper, "@VendorID", SqlDbType.VarChar, VendorID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSAprvItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvTerms
                    {
                        //VendorCSAprvTermID = reader.GetString("VendorCSAprvTermID"),
                        //VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                        VendorID = reader.GetString("VendorID"),
                        VendorName = reader.GetString("VendorName"),
                     
                    }).ToList();
                }
            }
            return VendorCSAprvItemList;
        }


        public string SaveVendorCSAprv(OBS_VendorCSAprv vendorCSInfo, List<OBS_VendorCSAprvItem> vendorCSInfoItem, List<OBS_VendorCSAprvTerms> vendorCSInfoTerm)
        {
            DataTable VendorCSItem = new DataTable();
            VendorCSItem.Columns.Add("VendorQutnID");
            VendorCSItem.Columns.Add("ServiceCategoryID");
            VendorCSItem.Columns.Add("ServiceItemID");

            VendorCSItem.Columns.Add("Description");
            VendorCSItem.Columns.Add("DeliveryLocation");
            VendorCSItem.Columns.Add("DeliveryDate");
            VendorCSItem.Columns.Add("DeliveryMode");

            VendorCSItem.Columns.Add("ReqQnty");
            VendorCSItem.Columns.Add("ReqUnit");
            VendorCSItem.Columns.Add("QutnQnty");
            VendorCSItem.Columns.Add("QutnPrice");
            VendorCSItem.Columns.Add("QutnUnit");
            VendorCSItem.Columns.Add("QutnAmt");
            VendorCSItem.Columns.Add("VatPerc");
            VendorCSItem.Columns.Add("VatAmt");
            VendorCSItem.Columns.Add("TolAmt");
            VendorCSItem.Columns.Add("VendorID");
            VendorCSItem.Columns.Add("VendorName");
            VendorCSItem.Columns.Add("VendorCSRecmID");
            VendorCSItem.Columns.Add("VendorCSRecmItemID");

            foreach (var item in vendorCSInfoItem)
            {
                DataRow objDataRow = VendorCSItem.NewRow();
                objDataRow[0] = item.VendorQutnID;
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
                objDataRow[16] = item.VendorID;
                objDataRow[17] = item.VendorName;
                objDataRow[18] = item.VendorCSInfoID;
                objDataRow[19] = item.VendorCSInfoItemID;

                VendorCSItem.Rows.Add(objDataRow);
            }

            DataTable VendorCSTerm = new DataTable();
            VendorCSTerm.Columns.Add("TermsID");
            VendorCSTerm.Columns.Add("TermsCode");
            VendorCSTerm.Columns.Add("TermsName");
            VendorCSTerm.Columns.Add("VendorID");
            foreach (var item in vendorCSInfoTerm)
            {
                DataRow objDataRow = VendorCSTerm.NewRow();
                objDataRow[0] = item.TermsID;
                objDataRow[1] = item.TermsCode;
                objDataRow[2] = item.TermsName;
                objDataRow[3] = item.VendorID;
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

            if (string.IsNullOrEmpty(vendorCSInfo.VendorCSAprvID))
                vendorCSInfo.Action = "add";
            else
                vendorCSInfo.Action = "edit";
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorCSAprv"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSAprvID", SqlDbType.NVarChar, vendorCSInfo.VendorCSAprvID);
                    db.AddInParameter(dbCommandWrapper, "@VendorCSRecmID", SqlDbType.NVarChar, vendorCSInfo.VendorCSRecmID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceCategoryID", SqlDbType.BigInt, vendorCSInfo.ServiceCategoryID);
                    db.AddInParameter(dbCommandWrapper, "@ServiceItemID", SqlDbType.BigInt, vendorCSInfo.ServiceItemID);
                    db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, vendorCSInfo.ClientReqID);
                    db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.BigInt, vendorCSInfo.ClientID);
                    db.AddInParameter(dbCommandWrapper, "@VendorReqID", SqlDbType.NVarChar, vendorCSInfo.VendorReqID);
                    //db.AddInParameter(dbCommandWrapper, "@CSNo", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.CSNo));
                    db.AddInParameter(dbCommandWrapper, "@CSRecDate", SqlDbType.NVarChar, vendorCSInfo.CSRecDate);
                    db.AddInParameter(dbCommandWrapper, "@Operation", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Operation));
                    db.AddInParameter(dbCommandWrapper, "@RecommendedBy", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.RecommendedBy));

                    db.AddInParameter(dbCommandWrapper, "@Remarks", SqlDbType.NVarChar, DataValidation.TrimmedOrDefault(vendorCSInfo.Remarks));
                    db.AddInParameter(dbCommandWrapper, "@UserID ", SqlDbType.NVarChar, vendorCSInfo.SetBy);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSAprvItem", SqlDbType.Structured, VendorCSItem);
                    db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSAprvTerms", SqlDbType.Structured, VendorCSTerm);
                    //db.AddInParameter(dbCommandWrapper, "@OBS_VendorCSAprvVendors", SqlDbType.Structured, vendorCSVendors);
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


        public List<OBS_VendorCSAprvTerms> GetVendorCSAprvTermAgainstFormList(string TermsID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSAprvTerms> VendorCSAprvItemList = new List<OBS_VendorCSAprvTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetVendorCSAprvTermAgainstFormList"))
            {
                db.AddInParameter(dbCommandWrapper, "@TermsID", SqlDbType.VarChar, TermsID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSAprvItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvTerms
                    {
                        //VendorCSAprvTermID = reader.GetString("VendorCSAprvTermID"),
                        //VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName")
                    }).ToList();
                }
            }
            return VendorCSAprvItemList;
        }

        public List<OBS_VendorCSAprvVendors> GetReqWiseVendorList(string VendorCSAprvID)
        {
            string errorNumber = string.Empty;
            List<OBS_VendorCSAprvVendors> VendorCSAprvItemList = new List<OBS_VendorCSAprvVendors>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetReqWiseVendorList"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSAprvID", SqlDbType.VarChar, VendorCSAprvID);
                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSAprvItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvVendors
                    {
                        VendorCSAprvVendorsID = reader.GetString("VendorCSAprvVendorsID"),
                        VendorCSAprvItemID = reader.GetString("VendorCSAprvItemID"),
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
            return VendorCSAprvItemList;
        }

        public string DeleteVendorCSAprvItemAndTerm(string VendorCSAprvItemID, string VendorCSAprvTermID)
        {
            string errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;
                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_SetVendorAddress"))
                {
                    // Set parameters 
                    db.AddInParameter(dbCommandWrapper, "@VendorCSAprvItemID", SqlDbType.NVarChar, VendorCSAprvItemID);
                    db.AddInParameter(dbCommandWrapper, "@VendorCSAprvTermID", SqlDbType.NVarChar, VendorCSAprvTermID);
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
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_VendorReqForCSAprv"))
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

        public List<OBS_VendorCSAprvItem> GetMaterialByRequisition(string vendorRequisitionNumber)
        {
            var ReqWiseMaterialList = new List<OBS_VendorCSAprvItem>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetMaterialByRequisitionAprv"))
            {
                db.AddInParameter(dbCommandWrapper, "@vendorRequisitionNumber", SqlDbType.VarChar, vendorRequisitionNumber);

                // Execute SP.

                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];

                    ReqWiseMaterialList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvItem
                    {
                        VendorCSInfoID = reader.GetString("VendorCSRecmID"),
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

        public List<OBS_VendorCSAprvItem> GetVendorByMaterialData(string vendorReqID, string serviceItemID)
        {
            List<OBS_VendorCSAprvItem> VendorCSInfoItemList = new List<OBS_VendorCSAprvItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_VendorDetailsForCSAprv"))
            {
                db.AddInParameter(dbCommandWrapper, "@vendorReqID", SqlDbType.VarChar, vendorReqID);
                db.AddInParameter(dbCommandWrapper, "@serviceItemID", SqlDbType.VarChar, serviceItemID);

                // Execute SP. 
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    VendorCSInfoItemList = dt1.AsEnumerable().Select(reader => new OBS_VendorCSAprvItem
                    {


                        VendorCSInfoID = reader.GetString("VendorCSRecmItemID"),
                        VendorCSInfoItemID = reader.GetString("VendorCSRecmItemID"),
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
