using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.RequisitionDelete
{
    // IRequisitionDeleteService.cs
    public interface IRequisitionDeleteService
    {
        void GetRequisitionForDelete(string clientReqID,
            out OBS_ClientReqHeader header, out string errorNumber);

        void DeleteEntireRequisition(string clientReqID, string deletedBy, string deleteReason,
            out string errorNumber);
    }
}
