using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ReceivedFinalPayment
{
    public interface IReceivedFinalPaymentService
    {
        ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList);
        ValidationResult GetShowClientReqList(out List<VendorBillRecvd> vendorReqList);
        string SaveClientFinalBill(VendorBillRecvd billRecv);
        string SaveVendorFinalBillRcvd(VendorBillRecvd billRecv);
    }
}
