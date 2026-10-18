using SILDMS.DataAccess.VendorFinalBill;
using SILDMS.Model;
using System.Collections.Generic;

namespace SILDMS.Service.VendorFinalBill
{
    // Same out-List shape as IVendorDemandService — matches how the
    // controllers in this project call their service layer.
   
    public class VendorFinalBillService : IVendorFinalBillService
    {
        private readonly IVendorFinalBillData vendorFinalBillData;

        public VendorFinalBillService(IVendorFinalBillData _vendorFinalBillData)
        {
            vendorFinalBillData = _vendorFinalBillData;
        }

        public void GetVendorFinalBillClientInfo(out List<OBS_VendorFinalBill> vendorFinalBillList)
        {
            vendorFinalBillList = vendorFinalBillData.GetVendorFinalBillClientInfo(out string errorNumber);
        }

        public void GetVendorFinalBillItem(string pOAprvID, out List<OBS_VendorFinalBillItem> vendorFinalBillItemList)
        {
            vendorFinalBillItemList = vendorFinalBillData.GetVendorFinalBillItem(pOAprvID, out string errorNumber);
        }

        public void GetVendorAdvanceDemandHistory(string pOAprvID, out List<OBS_VendorDemandHistoryRow> demandHistoryList)
        {
            demandHistoryList = vendorFinalBillData.GetVendorAdvanceDemandHistory(pOAprvID, out string errorNumber);
        }

        public void GetVendorFinalBillHistory(string pOAprvID, out List<OBS_VendorFinalBillHistoryRow> billHistoryList)
        {
            billHistoryList = vendorFinalBillData.GetVendorFinalBillHistory(pOAprvID, out string errorNumber);
        }

        public void GetVendorFinalBillTermList(string pOAprvID, out List<OBS_VendorCSRecmTerms> termList)
        {
            termList = vendorFinalBillData.GetVendorFinalBillTermList(pOAprvID, out string errorNumber);
        }

        public string SaveVendorFinalBillInfo(OBS_VendorFinalBill vendorFinalBill, List<OBS_VendorFinalBillItem> vendorFinalBillItem)
            => vendorFinalBillData.SaveVendorFinalBillInfo(vendorFinalBill, vendorFinalBillItem);
    }
}