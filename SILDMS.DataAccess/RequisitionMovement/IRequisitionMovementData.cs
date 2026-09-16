using System.Collections.Generic;
using SILDMS.Model;

namespace SILDMS.DataAccess.RequisitionMovement
{
    public interface IRequisitionMovementData
    {
        List<OBS_RequisitionMovementInfo> GetRequisitionMovementInfo(string clientReqId, out string _errorNumber);
    }
}