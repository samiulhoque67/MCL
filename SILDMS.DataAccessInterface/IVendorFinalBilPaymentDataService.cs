using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorFinalBilPaymentDataService
    {
        List<OBS_VendorFinalBilPayment> GetPOSearchList();
        List<OBS_VendorFinalBilPayment> GetShowVendorReqList();
        string SaveVendorFinalBill(OBS_VendorFinalBilPayment billRecv);
    }
}
