using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorBillPayment
{
    public interface IVendorBillPaymentData
    {
        List<OBS_VendorBillPayment> GetVendorBillPaymentList(out string errorNumber);
        List<OBS_VendorBillPaymentItem> GetVendorBillPaymentItem(string vendorFinalBillID, out string errorNumber);
        string SaveVendorBillPaymentInfo(OBS_VendorBillPayment payment);
    }
}
