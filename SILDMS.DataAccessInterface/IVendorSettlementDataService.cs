using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IVendorSettlementDataService
    {
        List<VendorBillRecvd> GetQutnSearchList();
        List<OBS_VendorSettlement> GetShowClientReqList();
        string SaveVendorSettlement(OBS_VendorSettlement billRecv);
    }
}
