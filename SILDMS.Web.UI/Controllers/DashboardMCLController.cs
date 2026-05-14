using SILDMS.Web.UI.Areas.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class DashboardMCLController : Controller
    {
        // GET: DashboardMCL

        [SILAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}