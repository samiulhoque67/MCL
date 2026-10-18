using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorBillPayment
{
    public interface IVendorBillPaymentService
    {
        void GetVendorBillPaymentList(out List<OBS_VendorBillPayment> paymentList);
        void GetVendorBillPaymentItem(string vendorFinalBillID, out List<OBS_VendorBillPaymentItem> itemList);
        string SaveVendorBillPaymentInfo(OBS_VendorBillPayment payment);
    }
}
