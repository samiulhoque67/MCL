using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.VendorFinalBill
{
    public interface IVendorFinalBillData
    {
        List<OBS_VendorFinalBill> GetVendorFinalBillClientInfo(out string errorNumber);
        List<OBS_VendorFinalBillItem> GetVendorFinalBillItem(string pOAprvID, out string errorNumber);
        List<OBS_VendorDemandHistoryRow> GetVendorAdvanceDemandHistory(string pOAprvID, out string errorNumber);
        List<OBS_VendorFinalBillHistoryRow> GetVendorFinalBillHistory(string pOAprvID, out string errorNumber);
        List<OBS_VendorCSRecmTerms> GetVendorFinalBillTermList(string pOAprvID, out string errorNumber);
        string SaveVendorFinalBillInfo(OBS_VendorFinalBill vendorFinalBill, List<OBS_VendorFinalBillItem> vendorFinalBillItem);
    }

}
