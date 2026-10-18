using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientClaim
{
    public interface IClientClaimService
    {
        void GetClientClaimClientInfo(out List<OBS_ClientClaim> clientClaimList);
        void GetClientClaimItem(string wOInfoID, out List<OBS_ClientClaimItem> clientClaimItemList);
        void GetClientClaimTermList(string wOInfoID, out List<OBS_VendorCSRecmTerms> termList);
        string SaveClientClaimInfo(OBS_ClientClaim clientClaim);
    }
}
