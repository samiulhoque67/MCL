using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientFinalBillPrepare
{
    public interface IClientFinalBillPrepareService
    {
        ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList);
        ValidationResult GetShowClientReqList(out List<OBS_VendorQutn> vendorReqList);
        string SaveClientFinalBill(VendorBillRecvd billRecv);
    }
}
