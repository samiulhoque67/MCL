using SILDMS.DataAccess.VendorBillPayment;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorBillPayment
{
    public class VendorBillPaymentService : IVendorBillPaymentService
    {
        private readonly IVendorBillPaymentData vendorBillPaymentData;

        public VendorBillPaymentService(IVendorBillPaymentData _vendorBillPaymentData)
        {
            vendorBillPaymentData = _vendorBillPaymentData;
        }

        public void GetVendorBillPaymentList(out List<OBS_VendorBillPayment> paymentList)
        {
            paymentList = vendorBillPaymentData.GetVendorBillPaymentList(out string errorNumber);
        }

        public void GetVendorBillPaymentItem(string vendorFinalBillID, out List<OBS_VendorBillPaymentItem> itemList)
        {
            itemList = vendorBillPaymentData.GetVendorBillPaymentItem(vendorFinalBillID, out string errorNumber);
        }

        public string SaveVendorBillPaymentInfo(OBS_VendorBillPayment payment)
            => vendorBillPaymentData.SaveVendorBillPaymentInfo(payment);
    
}
}
