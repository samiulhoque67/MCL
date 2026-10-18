using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientBillCollection
{
    public interface IClientBillCollectionData
    {
        List<OBS_ClientBillCollection> GetClientBillCollectionList(out string errorNumber);
        List<OBS_ClientBillCollectionItem> GetClientBillCollectionItem(string clientFinalBillID, out string errorNumber);
        string SaveClientBillCollectionInfo(OBS_ClientBillCollection collection);
    }
}
