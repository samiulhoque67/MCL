using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AprvFinalBillRcvd
{
    public interface IAprvFinalBillRcvdData
    {
        List<VendorBillRecvd> GetPOSearchList();
        List<VendorBillRecvd> GetShowVendorReqList();
        string SaveVendorFinalBill(VendorBillRecvd billRecv);
    }
}
