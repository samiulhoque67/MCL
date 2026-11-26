using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.RequisitionDelete
{
    public class RequisitionDeleteData : IRequisitionDeleteData
    {
        private readonly string _spStatusParam = "@p_Status";
        private readonly string _spErrorMessageParam = "@ErrorMessage";

        public void GetRequisitionForDelete(string clientReqID,
            out OBS_ClientReqHeader header, out string errorNumber)
        {
            errorNumber = string.Empty;
            header = null;

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;

            using (var cmd = db.GetStoredProcCommand("OBS_GetRequisitionForDelete"))
            {
                db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientReqID);
                db.AddOutParameter(cmd, _spStatusParam, DbType.String, 10);

                var ds = db.ExecuteDataSet(cmd);

                if (!db.GetParameterValue(cmd, _spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(cmd, _spStatusParam).PrefixErrorCode();
                    return;
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var r = ds.Tables[0].Rows[0];
                    header = new OBS_ClientReqHeader
                    {
                        ClientReqID = r.GetString("ClientReqID"),
                        ClientReqNo = r.GetString("ClientReqNo"),
                        Remarks = r.GetString("Remarks"),
                        ClientName = r.GetString("ClientName"),
                        SetBy = r.GetString("SetBy"),
                        SetByName = r.GetString("SetByName"),
                        SetOn = r.GetString("SetOn")
                    };
                }
            }
        }
        public void DeleteEntireRequisition(
            string clientReqID,
            string deletedBy,
            string deleteReason,
            out string errorNumber,
            out string errorMessage)
        {
            errorNumber = string.Empty;
            errorMessage = string.Empty;

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;

            using (var cmd = db.GetStoredProcCommand("OBS_DeleteEntireRequisition"))
            {
                db.AddInParameter(cmd, "@ClientReqID", SqlDbType.NVarChar, clientReqID);
                db.AddInParameter(cmd, "@DeletedBy", SqlDbType.NVarChar, deletedBy);
                db.AddInParameter(cmd, "@DeleteReason", SqlDbType.NVarChar,
                    string.IsNullOrEmpty(deleteReason) ? (object)DBNull.Value : deleteReason);

                db.AddOutParameter(cmd, _spStatusParam, DbType.String, 10);
                db.AddOutParameter(cmd, _spErrorMessageParam, DbType.String, 4000);

                cmd.CommandTimeout = 120;

                db.ExecuteNonQuery(cmd);

                var status = db.GetParameterValue(cmd, _spStatusParam);
                var message = db.GetParameterValue(cmd, _spErrorMessageParam);

                if (!status.IsNullOrZero())
                {
                    errorNumber = status.PrefixErrorCode();
                    errorMessage = message == null || message == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(message);
                }
            }
        }
    }
}
