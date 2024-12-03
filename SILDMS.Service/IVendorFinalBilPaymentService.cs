using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorFinalBilPaymentService
    {
        ValidationResult GetPOSearchList(out List<OBS_VendorFinalBilPayment> poSearchList);
        ValidationResult GetShowVendorReqList(out List<OBS_VendorFinalBilPayment> vendorReqList);
        string SaveVendorFinalBill(OBS_VendorFinalBilPayment billRecv);
    }
}
