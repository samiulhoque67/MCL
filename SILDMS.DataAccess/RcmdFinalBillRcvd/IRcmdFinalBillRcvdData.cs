using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.RcmdFinalBillRcvd
{
    public interface IRcmdFinalBillRcvdData
    {
        List<VendorBillRecvd> GetPOSearchList();
        List<VendorBillRecvd> GetShowVendorReqList();
        string SaveVendorFinalBill(VendorBillRecvd billRecv);
    }
}
