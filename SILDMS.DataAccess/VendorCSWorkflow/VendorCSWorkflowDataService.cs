using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorCSWorkflow
{

    public class VendorCSWorkflowDataService : IVendorCSWorkflowDataService
    {
        private const string spStatusParam = "@Status";

        public string Act(string stage, string docID, string action, string reason, string userID)
        {
            string errorNumber = string.Empty;
            try
            {
                var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
                using (DbCommand cmd = db.GetStoredProcCommand("OBS_CSWorkflow_Act"))
                {
                    db.AddInParameter(cmd, "@Stage", SqlDbType.NVarChar, stage);
                    db.AddInParameter(cmd, "@DocID", SqlDbType.NVarChar, docID);
                    db.AddInParameter(cmd, "@Action", SqlDbType.NVarChar, action);
                    db.AddInParameter(cmd, "@Reason", SqlDbType.NVarChar, reason);
                    db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, userID);
                    db.AddOutParameter(cmd, spStatusParam, SqlDbType.NVarChar, 10);
                    db.ExecuteNonQuery(cmd);
                    errorNumber = Convert.ToString(db.GetParameterValue(cmd, spStatusParam));
                }
            }
            catch (Exception ex) { errorNumber = ex.InnerException != null ? ex.InnerException.Message : "E404"; }
            return errorNumber;
        }

        public Dictionary<string, object> GetState(string stage, string docID)
        {
            var rows = Read("OBS_CSWorkflow_GetState", stage, docID);
            return rows.Count > 0 ? rows[0] : null;
        }

        public List<Dictionary<string, object>> GetHistory(string stage, string docID)
        {
            return Read("OBS_CSWorkflow_GetHistory", stage, docID);
        }

        public List<Dictionary<string, object>> GetRequisitionStates(string vendorReqIDs)
        {
            var list = new List<Dictionary<string, object>>();
            var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_CSWorkflow_GetRequisitionStates"))
            {
                db.AddInParameter(cmd, "@VendorReqIDs", SqlDbType.NVarChar, vendorReqIDs);
                DataSet ds = db.ExecuteDataSet(cmd);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    var d = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns) d[col.ColumnName] = r[col] == DBNull.Value ? null : r[col];
                    list.Add(d);
                }
            }
            return list;
        }

        // Marks any leftover live copy of this CS (OBS_VendorCSAprv*) as dead. Call right before a CS is recommended.
        public void RetireStale(string vendorCSRecmID)
        {
            var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_CSWorkflow_RetireStale"))
            {
                db.AddInParameter(cmd, "@VendorCSRecmID", SqlDbType.NVarChar, vendorCSRecmID);
                db.ExecuteNonQuery(cmd);
            }
        }

        public bool IsBlocked(string stage, string docID, out string message)
        {
            message = string.Empty;
            var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_CSWorkflow_Guard"))
            {
                db.AddInParameter(cmd, "@Stage", SqlDbType.NVarChar, stage);
                db.AddInParameter(cmd, "@DocID", SqlDbType.NVarChar, docID);
                db.AddOutParameter(cmd, "@Blocked", SqlDbType.Bit, 1);
                db.AddOutParameter(cmd, "@Message", SqlDbType.NVarChar, 300);
                db.ExecuteNonQuery(cmd);
                message = Convert.ToString(db.GetParameterValue(cmd, "@Message"));
                return Convert.ToBoolean(db.GetParameterValue(cmd, "@Blocked"));
            }
        }

        public void LogForward(string stage, string docID, string action, string awardMode, string note, string userID)
        {
            var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand("OBS_CSWorkflow_LogForward"))
            {
                db.AddInParameter(cmd, "@Stage", SqlDbType.NVarChar, stage);
                db.AddInParameter(cmd, "@DocID", SqlDbType.NVarChar, docID);
                db.AddInParameter(cmd, "@Action", SqlDbType.NVarChar, action);
                db.AddInParameter(cmd, "@AwardMode", SqlDbType.NVarChar, awardMode);
                db.AddInParameter(cmd, "@Note", SqlDbType.NVarChar, note);
                db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, userID);
                db.ExecuteNonQuery(cmd);
            }
        }

        private List<Dictionary<string, object>> Read(string proc, string stage, string docID)
        {
            var list = new List<Dictionary<string, object>>();
            var db = new DatabaseProviderFactory().CreateDefault() as SqlDatabase;
            using (DbCommand cmd = db.GetStoredProcCommand(proc))
            {
                db.AddInParameter(cmd, "@Stage", SqlDbType.NVarChar, stage);
                db.AddInParameter(cmd, "@DocID", SqlDbType.NVarChar, docID);
                DataSet ds = db.ExecuteDataSet(cmd);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    var d = new Dictionary<string, object>();
                    foreach (DataColumn c in ds.Tables[0].Columns) d[c.ColumnName] = r[c] == DBNull.Value ? null : r[c];
                    list.Add(d);
                }
            }
            return list;
        }
    }
}

