using System.Collections.Generic;
using SILDMS.DataAccess.RequisitionMovement;
using SILDMS.Model;

namespace SILDMS.Service.RequisitionMovement
{
    public interface IRequisitionMovementService
    {
        List<OBS_RequisitionMovementInfo> GetRequisitionMovementInfo(string clientReqId, out string errorNumber);
    }

    public class RequisitionMovementService : IRequisitionMovementService
    {
        private readonly IRequisitionMovementData _requisitionMovementData;

        public RequisitionMovementService()
        {
            _requisitionMovementData = new RequisitionMovementData();
        }

        public RequisitionMovementService(IRequisitionMovementData requisitionMovementData)
        {
            _requisitionMovementData = requisitionMovementData;
        }

        public List<OBS_RequisitionMovementInfo> GetRequisitionMovementInfo(string clientReqId, out string errorNumber)
        {
            return _requisitionMovementData.GetRequisitionMovementInfo(clientReqId, out errorNumber);
        }
    }
}