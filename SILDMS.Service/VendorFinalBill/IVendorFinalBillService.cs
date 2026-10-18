using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.VendorFinalBill
{
    public interface IVendorFinalBillService
    {
        void GetVendorFinalBillClientInfo(out List<OBS_VendorFinalBill> vendorFinalBillList);
        void GetVendorFinalBillItem(string pOAprvID, out List<OBS_VendorFinalBillItem> vendorFinalBillItemList);
        void GetVendorAdvanceDemandHistory(string pOAprvID, out List<OBS_VendorDemandHistoryRow> demandHistoryList);
        void GetVendorFinalBillHistory(string pOAprvID, out List<OBS_VendorFinalBillHistoryRow> billHistoryList);
        void GetVendorFinalBillTermList(string pOAprvID, out List<OBS_VendorCSRecmTerms> termList);
        string SaveVendorFinalBillInfo(OBS_VendorFinalBill vendorFinalBill, List<OBS_VendorFinalBillItem> vendorFinalBillItem);
    }
}
