using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientBillCollection
{
    public interface IClientBillCollectionService
    {
        void GetClientBillCollectionList(out List<OBS_ClientBillCollection> collectionList);
        void GetClientBillCollectionItem(string clientFinalBillID, out List<OBS_ClientBillCollectionItem> itemList);
        string SaveClientBillCollectionInfo(OBS_ClientBillCollection collection);
    }
}
