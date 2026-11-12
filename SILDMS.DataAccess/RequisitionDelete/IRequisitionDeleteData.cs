using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.RequisitionDelete
{
    public interface IRequisitionDeleteData
    {

        void GetRequisitionForDelete(string clientReqID,
        out OBS_ClientReqHeader header, out string errorNumber);

        // Performs the actual delete
        void DeleteEntireRequisition(string clientReqID, string deletedBy, string deleteReason,
            out string errorNumber);
    }
}
