using SILDMS.DataAccess.ClientClaim;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientClaim
{
    public class ClientClaimService : IClientClaimService
    {

        private readonly IClientClaimData clientClaimData;

        public ClientClaimService(IClientClaimData _clientClaimData)
        {
            clientClaimData = _clientClaimData;
        }

        public void GetClientClaimClientInfo(out List<OBS_ClientClaim> clientClaimList)
        {
            clientClaimList = clientClaimData.GetClientClaimClientInfo(out string errorNumber);
        }

        public void GetClientClaimItem(string wOInfoID, out List<OBS_ClientClaimItem> clientClaimItemList)
        {
            clientClaimItemList = clientClaimData.GetClientClaimItem(wOInfoID, out string errorNumber);
        }

        public void GetClientClaimTermList(string wOInfoID, out List<OBS_VendorCSRecmTerms> termList)
        {
            termList = clientClaimData.GetClientClaimTermList(wOInfoID, out string errorNumber);
        }

        public string SaveClientClaimInfo(OBS_ClientClaim clientClaim)
            => clientClaimData.SaveClientClaimInfo(clientClaim);
    }
}
