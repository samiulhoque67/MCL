using SILDMS.DataAccess.ClientBillCollection;
using SILDMS.Model;
using System.Collections.Generic;

namespace SILDMS.Service.ClientBillCollection
{

    public class ClientBillCollectionService : IClientBillCollectionService
    {
        private readonly IClientBillCollectionData clientBillCollectionData;

        public ClientBillCollectionService(IClientBillCollectionData _clientBillCollectionData)
        {
            clientBillCollectionData = _clientBillCollectionData;
        }

        public void GetClientBillCollectionList(out List<OBS_ClientBillCollection> collectionList)
        {
            collectionList = clientBillCollectionData.GetClientBillCollectionList(out string errorNumber);
        }

        public void GetClientBillCollectionItem(string clientFinalBillID, out List<OBS_ClientBillCollectionItem> itemList)
        {
            itemList = clientBillCollectionData.GetClientBillCollectionItem(clientFinalBillID, out string errorNumber);
        }

        public string SaveClientBillCollectionInfo(OBS_ClientBillCollection collection)
            => clientBillCollectionData.SaveClientBillCollectionInfo(collection);
    }
}