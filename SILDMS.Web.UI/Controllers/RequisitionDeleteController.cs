using SILDMS.Model;
using SILDMS.Service.RequisitionDelete;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

public class RequisitionDeleteController : Controller
{
    private readonly IRequisitionDeleteService _service;
    private readonly string UserID;

    public RequisitionDeleteController(IRequisitionDeleteService service)
    {
        _service = service;
        UserID = SILAuthorization.GetUserID();
    }

    // Only this user is allowed
    private bool IsAuthorized()
    {
        return UserID == "16102400032";
    }

    public ActionResult Index()
    {
        if (!IsAuthorized())
            return RedirectToAction("Index", "Home");   // or return HttpNotFound() / AccessDenied

        return View();
    }

    [HttpPost]
    public async Task<JsonResult> GetRequisition(string ClientReqID)
    {
        if (!IsAuthorized())
            return Json(new { success = false, msg = "Access denied." });

        if (string.IsNullOrEmpty(UserID))
            return Json(new { success = false, msg = "Session expired." });

        if (string.IsNullOrWhiteSpace(ClientReqID))
            return Json(new { success = false, msg = "ClientReqID is required." });

        OBS_ClientReqHeader header = null;
        string errorNumber = string.Empty;

        await Task.Run(() => _service.GetRequisitionForDelete(ClientReqID.Trim(), out header, out errorNumber));

        if (!string.IsNullOrEmpty(errorNumber) || header == null)
            return Json(new { success = false, msg = "Requisition not found or error: " + errorNumber });

        return Json(new
        {
            success = true,
            data = header,
            msg = "Requisition loaded. Please confirm delete."
        });
    }

    [HttpPost]
    public async Task<JsonResult> DeleteRequisition(string ClientReqID, string DeleteReason)
    {
        if (!IsAuthorized())
            return Json(new { success = false, msg = "Access denied." });

        if (string.IsNullOrEmpty(UserID))
            return Json(new { success = false, msg = "Session expired." });

        if (string.IsNullOrWhiteSpace(ClientReqID))
            return Json(new { success = false, msg = "ClientReqID is required." });

        string errorNumber = string.Empty;

        await Task.Run(() => _service.DeleteEntireRequisition(
            ClientReqID.Trim(),
            UserID,
            DeleteReason,
            out errorNumber));

        if (!string.IsNullOrEmpty(errorNumber))
            return Json(new { success = false, msg = "Delete failed. Error: " + errorNumber });

        return Json(new
        {
            success = true,
            msg = "Requisition " + ClientReqID + " has been permanently deleted and logged."
        });
    }
}