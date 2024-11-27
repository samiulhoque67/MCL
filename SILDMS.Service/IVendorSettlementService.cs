using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IVendorSettlementService
    {
        ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList);
        ValidationResult GetShowClientReqList(out List<OBS_VendorSettlement> vendorReqList);
        string SaveVendorSettlement(OBS_VendorSettlement billRecv);
    }
}
