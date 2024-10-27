using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorFinalBillReceived
{
    public interface IVendorFinalBillReceivedService
    {
        ValidationResult GetPOSearchList(out List<VendorBillRecvd> poSearchList);
        ValidationResult GetShowVendorReqList(out List<OBS_VendorQutn> vendorReqList);
        string SaveVendorFinalBill(VendorBillRecvd billRecv);
    }
}
