using SILDMS.Model;
using SILDMS.Model.SILDMS.Model;
using SILDMS.Service.WorkDashboard;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class DashboardMCLController : Controller
    {
        private readonly IWorkDashboardService workDashboardService;
        private readonly string UserID;

        public DashboardMCLController(IWorkDashboardService _workDashboardService)
        {
            workDashboardService = _workDashboardService;
            UserID = SILAuthorization.GetUserID();
        }

        public ActionResult Index()
        {
            return View();
        }

        // The user id always comes from the session — never from the request —
        // so one user can't ask for another user's dashboard.
        public async Task<dynamic> GetWorkDashboard(bool OnlyMine = false)
        {
            if (string.IsNullOrEmpty(UserID))
                return Json(new { Stages = new List<OBS_DashboardStage>(), Items = new List<OBS_DashboardItem>(), msg = "Session expired." }, JsonRequestBehavior.AllowGet);

            var stages = new List<OBS_DashboardStage>();
            var items = new List<OBS_DashboardItem>();
            await Task.Run(() => workDashboardService.GetWorkDashboard(UserID, OnlyMine, out stages, out items));

            var result = Json(new
            {
                Stages = stages,
                Items = items,
                LoadedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                msg = "Dashboard loaded."
            }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }
    }
}