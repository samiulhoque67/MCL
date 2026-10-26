using SILDMS.Model.SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.WorkDashboard
{
    public interface IWorkDashboardService
    {
        void GetWorkDashboard(string userId, bool onlyMine,
            out List<OBS_DashboardStage> stages, out List<OBS_DashboardItem> items);
    }
}
