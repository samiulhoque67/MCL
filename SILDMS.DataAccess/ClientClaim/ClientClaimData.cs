using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientClaim
{
    public class ClientClaimData : IClientClaimData
    {
        private readonly string spStatusParam = "@p_Status";

        public List<OBS_ClientClaim> GetClientClaimClientInfo(out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientClaim> list = new List<OBS_ClientClaim>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientClaimClientInfo"))
            {
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientClaim
                    {
                        WOInfoID = reader.GetString("WOInfoID"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientID = reader.GetString("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        WONo = reader.GetString("WONo"),
                        TotalWOAmount = reader.GetString("TotalWOAmount"),

                        ClientClaimID = reader.GetString("ClientClaimID"),
                        ClaimAmount = reader.GetString("ClaimAmount"),
                        RemainingWOAmount = reader.GetString("RemainingWOAmount"),
                        ClaimDate = reader.GetString("ClaimDate"),
                        ClaimRemarks = reader.GetString("ClaimRemarks"),
                        ClaimedBy = reader.GetString("ClaimedBy"),
                        IsFullPayment = reader.GetString("IsFullPayment"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_ClientClaimItem> GetClientClaimItem(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_ClientClaimItem> list = new List<OBS_ClientClaimItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientClaimItem"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_ClientClaimItem
                    {
                        WOInfoItemID = reader.GetString("WOInfoItemID"),
                        WOInfoID = reader.GetString("WOInfoID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ServicesCategoryName = reader.GetString("ServicesCategoryName"),
                        Description = reader.GetString("Description"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryMode = reader.GetString("DeliveryMode"),
                        QutnUnit = reader.GetString("QutnUnit"),
                        WOQnty = reader.GetString("WOQnty"),
                        WOPrice = reader.GetString("WOPrice"),
                        WOAmtWithoutASFVat = reader.GetString("WOAmtWithoutASFVat"),
                        ASFPerc = reader.GetString("ASFPerc"),
                        TotGivenASFAmt = reader.GetString("TotGivenASFAmt"),
                        VatPerc = reader.GetString("VatPerc"),
                        VatAmt = reader.GetString("VatAmt"),
                        WOAmtWithASFVat = reader.GetString("WOAmtWithASFVat"),
                    }).ToList();
                }
            }
            return list;
        }

        public List<OBS_VendorCSRecmTerms> GetClientClaimTermList(string wOInfoID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<OBS_VendorCSRecmTerms> list = new List<OBS_VendorCSRecmTerms>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("OBS_GetClientClaimTermList"))
            {
                db.AddInParameter(dbCommandWrapper, "@WOInfoID", SqlDbType.NVarChar, wOInfoID);
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    list = dt1.AsEnumerable().Select(reader => new OBS_VendorCSRecmTerms
                    {
                        TermsID = reader.GetString("WOInfoTermID"),
                        TermsCode = reader.GetString("TermsCode"),
                        TermsName = reader.GetString("TermsName"),
                    }).ToList();
                }
            }
            return list;
        }

        public string SaveClientClaimInfo(OBS_ClientClaim clientClaim)
        {
            string errorNumber = string.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                using (DbCommand cmd = db.GetStoredProcCommand("OBS_SetClientClaimInfo"))
                {
                    db.AddInParameter(cmd, "@WOInfoID", SqlDbType.NVarChar, clientClaim.WOInfoID);
                    db.AddInParameter(cmd, "@ClientClaimID", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(clientClaim.ClientClaimID));
                    db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientClaim.ClientReqID);
                    db.AddInParameter(cmd, "@ClientID", SqlDbType.BigInt, clientClaim.ClientID);
                    db.AddInParameter(cmd, "@ClaimAmount", SqlDbType.Decimal, clientClaim.ClaimAmount);
                    db.AddInParameter(cmd, "@ClaimDate", SqlDbType.NVarChar, clientClaim.ClaimDate);
                    db.AddInParameter(cmd, "@Remarks", SqlDbType.NVarChar,
                        DataValidation.TrimmedOrDefault(clientClaim.Remarks));
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, clientClaim.SetBy);
                    db.AddInParameter(cmd, "@IsFullPayment", SqlDbType.NVarChar,
                        string.IsNullOrEmpty(clientClaim.IsFullPayment) ? "0" : clientClaim.IsFullPayment);

                    db.AddOutParameter(cmd, spStatusParam, SqlDbType.VarChar, 10);
                    db.ExecuteNonQuery(cmd);

                    if (!db.GetParameterValue(cmd, spStatusParam).IsNullOrZero())
                        errorNumber = db.GetParameterValue(cmd, spStatusParam).PrefixErrorCode();
                }
            }
            catch (Exception ex)
            {
                errorNumber = ex.InnerException?.Message ?? ex.Message;
            }

            return errorNumber;
        }
    }
}
