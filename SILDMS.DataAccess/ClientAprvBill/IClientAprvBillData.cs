using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientAprvBill
{
    public interface IClientAprvBillData
    {
        List<VendorBillRecvd> GetQutnSearchList();
        List<VendorBillRecvd> GetShowClientReqList();
        string SaveClientFinalBill(VendorBillRecvd billRecv);
        string SaveVendorFinalBillRcvd(VendorBillRecvd billRecv);
    }
}
