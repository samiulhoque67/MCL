using SILDMS.Model;
using SILDMS.Service;
using SILDMS.Service.VendorFinalBillReceived;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Controllers
{
    public class VendorFinalBillReceivedController : Controller
    {
        // GET: VendorFinalBillReceived
        readonly IVendorFinalBillReceivedService _vendorFinalBillReceivedService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;

        public VendorFinalBillReceivedController(IVendorFinalBillReceivedService vendorFinalBillReceivedService, ILocalizationService localizationService)
        {
            _vendorFinalBillReceivedService = vendorFinalBillReceivedService;
            _localizationService = localizationService;
            UserID = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> GetShowVendorReqList()
        {
            var VendorReqList = new List<OBS_VendorQutn>();
            await Task.Run(() => _vendorFinalBillReceivedService.GetShowVendorReqList(out VendorReqList));
            var result = Json(new { VendorReqList, msg = "Client Info List are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        public async Task<dynamic> SaveVendorFinalBill(VendorBillRecvd BillRecv)
        { string VendrFinalBilRcvdID = string.Empty; ;
            BillRecv.SetBy = UserID;
            string status = string.Empty;//, message = string.Empty;
            status = _vendorFinalBillReceivedService.SaveVendorFinalBill(BillRecv);

            if (status != string.Empty)
            {
                string[] statusarr = status.Split(',');
                VendrFinalBilRcvdID = statusarr[1];
                /*clientReq.ClientReqID = statusarr[1];*/
                status = statusarr[0];
            }
            return Json(new { status, VendrFinalBilRcvdID }, JsonRequestBehavior.AllowGet);
        }

        public async Task<dynamic> GetPOSearchList()
        {
            var PoSearchList = new List<VendorBillRecvd>();
            await Task.Run(() => _vendorFinalBillReceivedService.GetPOSearchList(out PoSearchList));
            var result = Json(new { PoSearchList, msg = "clientReqSearchList are loaded in the table." }, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }


    }
}