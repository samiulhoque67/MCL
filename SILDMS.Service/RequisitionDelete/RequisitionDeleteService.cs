using SILDMS.DataAccess.RequisitionDelete;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.RequisitionDelete
{
    // RequisitionDeleteService.cs
    public class RequisitionDeleteService : IRequisitionDeleteService
    {
        private readonly IRequisitionDeleteData _data;

        public RequisitionDeleteService(IRequisitionDeleteData data)
        {
            _data = data;
        }

        public void GetRequisitionForDelete(string clientReqID,
            out OBS_ClientReqHeader header, out string errorNumber)
        {
            _data.GetRequisitionForDelete(clientReqID, out header, out errorNumber);
        }

        public void DeleteEntireRequisition(string clientReqID, string deletedBy, string deleteReason,
            out string errorNumber)
        {
            _data.DeleteEntireRequisition(clientReqID, deletedBy, deleteReason, out errorNumber);
        }
    }
}
