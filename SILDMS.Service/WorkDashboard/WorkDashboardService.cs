using SILDMS.DataAccess.WorkDashboard;
using SILDMS.Model;
using SILDMS.Model.SILDMS.Model;
using System.Collections.Generic;

namespace SILDMS.Service.WorkDashboard
{
   

    public class WorkDashboardService : IWorkDashboardService
    {
        private readonly IWorkDashboardData workDashboardData;

        public WorkDashboardService(IWorkDashboardData _workDashboardData)
        {
            workDashboardData = _workDashboardData;
        }

        public void GetWorkDashboard(string userId, bool onlyMine,
            out List<OBS_DashboardStage> stages, out List<OBS_DashboardItem> items)
        {
            workDashboardData.GetWorkDashboard(userId, onlyMine, out stages, out items, out string errorNumber);
        }
    }
}