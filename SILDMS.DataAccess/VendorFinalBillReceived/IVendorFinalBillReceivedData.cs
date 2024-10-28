using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorFinalBillReceived
{
    public interface IVendorFinalBillReceivedData
    {
        List<VendorBillRecvd> GetPOSearchList();
        List<OBS_VendorQutn> GetShowVendorReqList();
        string SaveVendorFinalBill(VendorBillRecvd billRecv);
    }
}
