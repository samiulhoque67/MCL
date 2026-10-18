using SILDMS.DataAccess.ClientFinalBill;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientFinalBill
{
    public class ClientFinalBillService : IClientFinalBillService
    {
        private readonly IClientFinalBillData clientFinalBillData;

        public ClientFinalBillService(IClientFinalBillData _clientFinalBillData)
        {
            clientFinalBillData = _clientFinalBillData;
        }

        public void GetClientFinalBillClientInfo(out List<OBS_ClientFinalBill> clientFinalBillList)
        {
            clientFinalBillList = clientFinalBillData.GetClientFinalBillClientInfo(out string errorNumber);
        }

        public void GetClientFinalBillItem(string wOInfoID, out List<OBS_ClientFinalBillItem> clientFinalBillItemList)
        {
            clientFinalBillItemList = clientFinalBillData.GetClientFinalBillItem(wOInfoID, out string errorNumber);
        }

        public void GetClientAdvanceClaimHistory(string wOInfoID, out List<OBS_ClientAdvanceClaimHistoryRow> claimHistoryList)
        {
            claimHistoryList = clientFinalBillData.GetClientAdvanceClaimHistory(wOInfoID, out string errorNumber);
        }

        public void GetClientFinalBillHistory(string wOInfoID, out List<OBS_ClientFinalBillHistoryRow> billHistoryList)
        {
            billHistoryList = clientFinalBillData.GetClientFinalBillHistory(wOInfoID, out string errorNumber);
        }

        public void GetClientFinalBillTermList(string wOInfoID, out List<OBS_VendorCSRecmTerms> termList)
        {
            termList = clientFinalBillData.GetClientFinalBillTermList(wOInfoID, out string errorNumber);
        }

        public string SaveClientFinalBillInfo(OBS_ClientFinalBill clientFinalBill, List<OBS_ClientFinalBillItem> clientFinalBillItem)
            => clientFinalBillData.SaveClientFinalBillInfo(clientFinalBill, clientFinalBillItem);
    }
}

