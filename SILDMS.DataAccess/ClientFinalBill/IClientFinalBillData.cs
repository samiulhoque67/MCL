using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.ClientFinalBill
{
    public interface IClientFinalBillData
    {
        List<OBS_ClientFinalBill> GetClientFinalBillClientInfo(out string errorNumber);
        List<OBS_ClientFinalBillItem> GetClientFinalBillItem(string wOInfoID, out string errorNumber);
        List<OBS_ClientAdvanceClaimHistoryRow> GetClientAdvanceClaimHistory(string wOInfoID, out string errorNumber);
        List<OBS_ClientFinalBillHistoryRow> GetClientFinalBillHistory(string wOInfoID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetClientFinalBillTermList(string wOInfoID, out string errorNumber);
        string SaveClientFinalBillInfo(OBS_ClientFinalBill clientFinalBill, List<OBS_ClientFinalBillItem> clientFinalBillItem);
    }
}
