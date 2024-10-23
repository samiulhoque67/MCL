using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AprvFinalBillRcvd
{
    public interface IAprvFinalBillRcvdService
    {
        ValidationResult GetPOSearchList(out List<VendorBillRecvd> poSearchList);
        ValidationResult GetShowVendorReqList(out List<VendorBillRecvd> vendorReqList);
        string SaveVendorFinalBill(VendorBillRecvd billRecv);
    }
}
