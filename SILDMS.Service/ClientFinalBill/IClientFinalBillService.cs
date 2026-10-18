using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientFinalBill
{
    public interface IClientFinalBillService
    {

        void GetClientFinalBillClientInfo(out List<OBS_ClientFinalBill> clientFinalBillList);
        void GetClientFinalBillItem(string wOInfoID, out List<OBS_ClientFinalBillItem> clientFinalBillItemList);
        void GetClientAdvanceClaimHistory(string wOInfoID, out List<OBS_ClientAdvanceClaimHistoryRow> claimHistoryList);
        void GetClientFinalBillHistory(string wOInfoID, out List<OBS_ClientFinalBillHistoryRow> billHistoryList);
        void GetClientFinalBillTermList(string wOInfoID, out List<OBS_VendorCSRecmTerms> termList);
        string SaveClientFinalBillInfo(OBS_ClientFinalBill clientFinalBill, List<OBS_ClientFinalBillItem> clientFinalBillItem);
    }
}
