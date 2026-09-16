using System.Web.Mvc;
using SILDMS.Service.RequisitionMovement;

namespace SILDMS.Areas.SecurityModule.Controllers
{
    public class RequisitionMovementController : Controller
    {
        private readonly IRequisitionMovementService _requisitionMovementService;

        public RequisitionMovementController()
        {
            _requisitionMovementService = new RequisitionMovementService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMovement(string clientReqId)
        {
            if (string.IsNullOrWhiteSpace(clientReqId))
            {
                return Json(new
                {
                    status = "E",
                    message = "Client Requisition ID is required."
                });
            }

            string errorNumber;

            var data = _requisitionMovementService
                .GetRequisitionMovementInfo(clientReqId, out errorNumber);

            if (!string.IsNullOrEmpty(errorNumber))
            {
                return Json(new
                {
                    status = "E",
                    message = errorNumber
                });
            }

            if (data == null || data.Count == 0)
            {
                return Json(new
                {
                    status = "E",
                    message = "No requisition movement information found."
                });
            }

            return Json(new
            {
                status = "S",
                data = data
            });
        }
    }
}