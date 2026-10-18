using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientClaim
{
    public interface IClientClaimData
    {

        List<OBS_ClientClaim> GetClientClaimClientInfo(out string errorNumber);
        List<OBS_ClientClaimItem> GetClientClaimItem(string wOInfoID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetClientClaimTermList(string wOInfoID, out string errorNumber);
        string SaveClientClaimInfo(OBS_ClientClaim clientClaim);
    }
}
