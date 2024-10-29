using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientFinalBillPrepare
{
    public interface IClientFinalBillPrepareData
    {
        List<VendorBillRecvd> GetQutnSearchList();
        List<OBS_VendorQutn> GetShowClientReqList();
        string SaveClientFinalBill(VendorBillRecvd billRecv);
    }
}
