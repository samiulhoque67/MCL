using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using SILDMS.Model.SILDMS.Model;

namespace SILDMS.DataAccess.WorkDashboard
{
  

    public class WorkDashboardData : IWorkDashboardData
    {
        private readonly string _spStatusParam = "@p_Status";

        public void GetWorkDashboard(string userId, bool onlyMine,
            out List<OBS_DashboardStage> stages, out List<OBS_DashboardItem> items, out string errorNumber)
        {
            errorNumber = string.Empty;
            stages = new List<OBS_DashboardStage>();
            items = new List<OBS_DashboardItem>();

            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;

            using (var cmd = db.GetStoredProcCommand("OBS_GetWorkDashboard"))
            {
                db.AddInParameter(cmd, "@UserID", SqlDbType.NVarChar, userId);
                db.AddInParameter(cmd, "@OnlyMine", SqlDbType.Bit, onlyMine);
                db.AddOutParameter(cmd, _spStatusParam, DbType.String, 10);
                cmd.CommandTimeout = 120;

                var ds = db.ExecuteDataSet(cmd);

                if (!db.GetParameterValue(cmd, _spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(cmd, _spStatusParam).PrefixErrorCode();
                    return;
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    stages = ds.Tables[0].AsEnumerable().Select(reader => new OBS_DashboardStage
                    {
                        StageCode = reader.GetString("StageCode"),
                        StageTitle = reader.GetString("StageTitle"),
                        GroupName = reader.GetString("GroupName"),
                        MenuID = reader.GetString("MenuID"),
                        Url = reader.GetString("Url"),
                        SortOrder = reader.GetString("SortOrder"),
                        IsOptional = reader.GetString("IsOptional"),
                        IsQueue = reader.GetString("IsQueue"),
                        PendingMeans = reader.GetString("PendingMeans"),
                    }).ToList();
                }

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    items = ds.Tables[1].AsEnumerable().Select(reader => new OBS_DashboardItem
                    {
                        StageCode = reader.GetString("StageCode"),
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        ProjectName = reader.GetString("ProjectName"),
                        ClientName = reader.GetString("ClientName"),
                        RefNo = reader.GetString("RefNo"),
                        Detail = reader.GetString("Detail"),
                        WaitingSince = reader.GetString("WaitingSince"),
                        DaysWaiting = reader.GetString("DaysWaiting"),
                        RaisedBy = reader.GetString("RaisedBy"),
                        IsMine = reader.GetString("IsMine"),
                    }).ToList();
                }
            }
        }
    }
}