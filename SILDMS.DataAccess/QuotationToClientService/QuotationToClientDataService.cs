using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Web.UI;
using System.Data.Common;

namespace SILDMS.DataAccess.QuotationToClientService
{
    public class QuotationToClientDataService : IQuotationToClientDataService
    {

        private readonly string _spStatusParam;

        public QuotationToClientDataService()
        {
            _spStatusParam = "@p_Status";
        }

        public List<OBS_ClientInfo> AllAvailableCSVendorApprovalDataService(string UserId, int page, int itemsPerPage, string sortBy, bool reverse, string search, string type, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var AllAvailableClientsList = new List<OBS_ClientInfo>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailableClientInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@page", SqlDbType.Int, page);
                db.AddInParameter(dbCommandWrapper, "@itemsPerPage", SqlDbType.Int, itemsPerPage);
                db.AddInParameter(dbCommandWrapper, "@sortBy", SqlDbType.NVarChar, sortBy);
                db.AddInParameter(dbCommandWrapper, "@reverse", SqlDbType.Int, reverse ? 1 : 0);
                db.AddInParameter(dbCommandWrapper, "@search", SqlDbType.NVarChar, search);
                db.AddInParameter(dbCommandWrapper, "@type", SqlDbType.NVarChar, type.ToString());
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return AllAvailableClientsList;
                    var dt1 = ds.Tables[0];
                    AllAvailableClientsList = dt1.AsEnumerable().Select(reader => new OBS_ClientInfo
                    {
                        ProjectName = reader.GetString("ProjectName"),
                        UserFullName = reader.GetString("UserFullName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientCode = reader.GetString("ClientCode"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ReqType = reader.GetString("ReqTypeSummary"),
                    }).ToList();

                }
            }

            return AllAvailableClientsList;
        }

        public List<OBS_ClientDetails> AvailableClientDetailInfoDataService(string ClientID, string ClientReqID, string ReqType, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var ClientDetails = new List<OBS_ClientDetails>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailableClientDetailedInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, ClientID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@ReqType", SqlDbType.NVarChar, ReqType);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return ClientDetails;
                    var dt1 = ds.Tables[0];
                    ClientDetails = dt1.AsEnumerable().Select(reader => new OBS_ClientDetails
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientTinNo = reader.GetString("ClientTinNo"),
                        ClientBinNo = reader.GetString("ClientBinNo"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        Remarks = reader.GetString("Remarks"),
                        DocumentID = reader.GetString("DocumentID")
                    }).ToList();

                }
            }

            return ClientDetails;
        }

        public List<OBS_ClientDetails> AvailableClientAprvInfo(string ClientID, string ClientReqID, string ReqType, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var ClientDetails = new List<OBS_ClientDetails>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetAvailableClientAprvInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", SqlDbType.NVarChar, ClientID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@ReqType", SqlDbType.NVarChar, ReqType);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return ClientDetails;
                    var dt1 = ds.Tables[0];
                    ClientDetails = dt1.AsEnumerable().Select(reader => new OBS_ClientDetails
                    {
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientTinNo = reader.GetString("ClientTinNo"),
                        ClientBinNo = reader.GetString("ClientBinNo"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Address = reader.GetString("Address"),
                        ContactPerson = reader.GetString("ContactPerson"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        Remarks = reader.GetString("Remarks"),
                        DocumentID = reader.GetString("DocumentID"),
                        RecmRemarks = reader.GetString("RecmRemarks"),
                        ClientQutnAprvID = reader.GetString("ClientQutnAprvID"),
                        AprvRemarks = reader.GetString("AprvRemarks")
                    }).ToList();

                }
            }

            return ClientDetails;
        }

        public List<ClientReqData> GetClientReqDataInfoDataService(string ClientID, string ClientReqID, string ReqType, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var GetClientReqDetails = new List<ClientReqData>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientReqDataInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientID", DbType.String, ClientID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", DbType.String, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@ReqType", SqlDbType.NVarChar, ReqType);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 1000);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return GetClientReqDetails;
                    var dt1 = ds.Tables[0];
                    GetClientReqDetails = dt1.AsEnumerable().Select(reader => new ClientReqData
                    {
                        VendorCSAprvItemID = reader.GetString("VendorCSAprvID"),
                        VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        ClientID = reader.GetString("ClientID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ReqType = reader.GetString("ReqType"),
                        TermsID = reader.GetString("TermsID"),
                        ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
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

            return GetClientReqDetails;
        }

        public List<ClientReqData> GetClientReqDataItemPopupDataService(string VendorCSAprvID, string ServiceItemID, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var GetClientReqDataItemPopup = new List<ClientReqData>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("GetClientReqDataItemPopup"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSAprvID", DbType.String, VendorCSAprvID);
                db.AddInParameter(dbCommandWrapper, "@ServiceItemID", DbType.String, ServiceItemID);/*
                db.AddInParameter(dbCommandWrapper, "@ReqType", SqlDbType.NVarChar, ReqType);*/
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return GetClientReqDataItemPopup;
                    var dt1 = ds.Tables[0];
                    GetClientReqDataItemPopup = dt1.AsEnumerable().Select(reader => new ClientReqData
                    {
                        VendorCSAprvItemID = reader.GetString("VendorCSAprvID"),
                        VendorCSAprvID = reader.GetString("VendorCSAprvID"),
                        ClientID = reader.GetString("ClientID"),
                        VendorName = reader.GetString("VendorName"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        TermsID = reader.GetString("TermsID"),
                        ServiceItemCode = reader.GetString("ServiceItemCode"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServiceCategoryID = reader.GetString("ServiceCategoryID"),
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

            return GetClientReqDataItemPopup;
        }

        public List<OBS_TermsItem> GetTermsConditionsListServiceData(string VendorCSAprvID, string ClientReqID, string ReqType, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var VendorTermTermList = new List<OBS_TermsItem>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;
            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientQtnTermData"))
            {
                db.AddInParameter(dbCommandWrapper, "@VendorCSAprvID", DbType.String, VendorCSAprvID);
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", DbType.String, ClientReqID);
                db.AddInParameter(dbCommandWrapper, "@ReqType", DbType.String, ReqType);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;
                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return VendorTermTermList;
                    var dt1 = ds.Tables[0];
                    VendorTermTermList = dt1.AsEnumerable().Select(reader => new OBS_TermsItem
                    {
                        //VendorCSAprvTermID = reader.GetString("VendorCSAprvTermID"),
                        TermsItemID = reader.GetString("TermsItemID"),
                        TermsID = reader.GetString("TermsID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                        ReqType = reader.GetString("ReqType")
                    }).ToList();

                }
            }

            return VendorTermTermList;
        }


        public string SaveQuotToClientServiceData(
    string UserID,
    string action,
    List<OBS_QutntoClientMaster> MasterData,
    List<ClientReqData> DetailData,
    List<OBS_TermsItem> AllTermsDtl,
    string ReqType,
    out string errorNumber)
        {
            errorNumber = string.Empty;
            string message = string.Empty;

            /* ── Master DataTable ──────────────────────────────────────── */
            DataTable masterDataTable = new DataTable();
            masterDataTable.Columns.Add("ClientID", typeof(string));
            masterDataTable.Columns.Add("ClientReqID", typeof(string));
            masterDataTable.Columns.Add("ClientQuotationID", typeof(string)); // required for edit
            masterDataTable.Columns.Add("ClientQutnRecmID", typeof(string));
            masterDataTable.Columns.Add("ClientQutnAprvID", typeof(string));
            masterDataTable.Columns.Add("Operation", typeof(string));
            masterDataTable.Columns.Add("BriefingDate", typeof(string));
            masterDataTable.Columns.Add("QuotationNote", typeof(string));

            if (MasterData != null)
            {
                foreach (var m in MasterData)
                {
                    DataRow r = masterDataTable.NewRow();
                    r["ClientID"] = NullIfEmpty(m.ClientID);
                    r["ClientReqID"] = NullIfEmpty(m.ClientReqID);
                    r["ClientQuotationID"] = NullIfEmpty(m.ClientQuotationID); // <-- must be set in editmode
                    r["ClientQutnRecmID"] = NullIfEmpty(m.ClientQutnRecmID);
                    r["ClientQutnAprvID"] = NullIfEmpty(m.ClientQutnAprvID);
                    r["Operation"] = NullIfEmpty(m.Operation);
                    r["BriefingDate"] = NullIfEmpty(m.BriefingDate);
                    r["QuotationNote"] = NullIfEmpty(m.QuotationNote);
                    masterDataTable.Rows.Add(r);
                }
            }

            /* ── Detail DataTable ──────────────────────────────────────── */
            DataTable detailDataTable = new DataTable();
            detailDataTable.Columns.Add("ClientQuotationID", typeof(string));
            detailDataTable.Columns.Add("ClientQutnRecmID", typeof(string));
            detailDataTable.Columns.Add("ClientQutnAprvID", typeof(string));
            detailDataTable.Columns.Add("ServiceCategoryID", typeof(string));
            detailDataTable.Columns.Add("TermsID", typeof(string));
            detailDataTable.Columns.Add("ServiceItemID", typeof(string));
            detailDataTable.Columns.Add("Description", typeof(string));
            detailDataTable.Columns.Add("DeliveryLocation", typeof(string));
            detailDataTable.Columns.Add("DeliveryDate", typeof(string));
            detailDataTable.Columns.Add("DeliveryMode", typeof(string));
            detailDataTable.Columns.Add("QutnQnty", typeof(string));
            detailDataTable.Columns.Add("QutnUnit", typeof(string));
            detailDataTable.Columns.Add("VenPrice", typeof(string));
            detailDataTable.Columns.Add("MclPrice", typeof(string));
            detailDataTable.Columns.Add("QutnAmt", typeof(string));
            detailDataTable.Columns.Add("ASFPerc", typeof(string));
            detailDataTable.Columns.Add("ASFAmt", typeof(string));
            detailDataTable.Columns.Add("VatPerc", typeof(string));
            detailDataTable.Columns.Add("VatAmt", typeof(string));
            detailDataTable.Columns.Add("TolAmt", typeof(string));

            if (DetailData != null)
            {
                foreach (var d in DetailData)
                {
                    DataRow r = detailDataTable.NewRow();
                    r["ClientQuotationID"] = NullIfEmpty(d.ClientQuotationID);
                    r["ClientQutnRecmID"] = NullIfEmpty(d.ClientQutnRecmID);
                    r["ClientQutnAprvID"] = NullIfEmpty(d.ClientQutnAprvID);
                    r["ServiceCategoryID"] = NullIfEmpty(d.ServiceCategoryID);
                    r["TermsID"] = NullIfEmpty(d.TermsID);
                    r["ServiceItemID"] = NullIfEmpty(d.ServiceItemID);
                    r["Description"] = NullIfEmpty(d.Description);
                    r["DeliveryLocation"] = NullIfEmpty(d.DeliveryLocation);
                    r["DeliveryDate"] = NullIfEmpty(d.DeliveryDate);
                    r["DeliveryMode"] = NullIfEmpty(d.DeliveryMode);
                    r["QutnQnty"] = NullIfEmpty(d.QutnQnty);
                    r["QutnUnit"] = NullIfEmpty(d.QutnUnit);
                    r["VenPrice"] = NullIfEmpty(d.VenPrice);
                    // FIX: JS sends numbers; convert to string and guard "null" strings
                    r["MclPrice"] = NullIfNullString(d.MclPrice);
                    r["QutnAmt"] = NullIfNullString(d.QutnAmt);
                    r["ASFPerc"] = NullIfNullString(d.ASFPerc);
                    r["ASFAmt"] = NullIfNullString(d.ASFAmt);
                    r["VatPerc"] = NullIfNullString(d.VatPerc);
                    r["VatAmt"] = NullIfNullString(d.VatAmt);
                    r["TolAmt"] = NullIfNullString(d.TolAmt);
                    detailDataTable.Rows.Add(r);
                }
            }

            /* ── Terms DataTable ───────────────────────────────────────── */
            DataTable termsDtlTable = new DataTable();
            termsDtlTable.Columns.Add("TermsID", typeof(string));
            termsDtlTable.Columns.Add("TermsCode", typeof(string));
            termsDtlTable.Columns.Add("TermsName", typeof(string));

            if (AllTermsDtl != null)
            {
                foreach (var t in AllTermsDtl)
                {
                    DataRow r = termsDtlTable.NewRow();
                    r["TermsID"] = NullIfEmpty(t.TermsID);
                    r["TermsCode"] = NullIfEmpty(t.TermsCode);
                    r["TermsName"] = NullIfEmpty(t.TermsName);
                    termsDtlTable.Rows.Add(r);
                }
            }

            /* ── Execute SP ────────────────────────────────────────────── */
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;

            using (DbCommand cmd = db.GetStoredProcCommand("OBS_SaveQuotToClient"))
            {
                db.AddInParameter(cmd, "@action", SqlDbType.VarChar, action);
                db.AddInParameter(cmd, "@OBS_QtC_MasterType", SqlDbType.Structured, masterDataTable);
                db.AddInParameter(cmd, "@OBS_QtC_DetailType", SqlDbType.Structured, detailDataTable);
                db.AddInParameter(cmd, "@OBS_Qtc_TermsDtl", SqlDbType.Structured, termsDtlTable);
                db.AddInParameter(cmd, "@BriefingDate", SqlDbType.NVarChar, MasterData[0].BriefingDate);
                db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, MasterData[0].ClientReqID);
                db.AddInParameter(cmd, "@ReqType", SqlDbType.NVarChar, ReqType);
                db.AddInParameter(cmd, "@SetBy", SqlDbType.VarChar, UserID);
                db.AddOutParameter(cmd, "@p_Status", DbType.String, 1200);

                var ds = db.ExecuteDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    message = ds.Tables[0].Rows[0]["Status"].ToString();
            }

            return message;
        }

        /* ── Private helpers ───────────────────────────────────────────────
           Put these as private methods in the same class.
           ─────────────────────────────────────────────────────────────────── */

        /// <summary>Returns DBNull for null/empty strings.</summary>
        private static object NullIfEmpty(string value)
            => string.IsNullOrEmpty(value) ? (object)DBNull.Value : value;

        /// <summary>
        /// Returns DBNull for null, empty, or the literal string "null"
        /// (which AngularJS/JSON serialiser can produce for JS null numbers).
        /// </summary>
        private static object NullIfNullString(string value)
            => (string.IsNullOrEmpty(value) || value.Equals("null", StringComparison.OrdinalIgnoreCase))
               ? (object)DBNull.Value
               : value;


    }
}
