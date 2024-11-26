using SILDMS.DataAccess.ClientBillRcmd;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.ClientAprvBill;

namespace SILDMS.Service.ClientAprvBill
{
    public class ClientAprvBillService : IClientAprvBillService
    {
        private readonly IClientAprvBillData _clientAprvBillData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public ClientAprvBillService(IClientAprvBillData clientAprvBillData, ILocalizationService localizationService)
        {
            _clientAprvBillData = clientAprvBillData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _clientAprvBillData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<VendorBillRecvd> vendorReqList)
        {
            vendorReqList = _clientAprvBillData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveClientFinalBill(VendorBillRecvd billRecv)
        {
            return _clientAprvBillData.SaveClientFinalBill(billRecv);
        }
        public string SaveVendorFinalBillRcvd(VendorBillRecvd billRecv)
        {
            return _clientAprvBillData.SaveVendorFinalBillRcvd(billRecv);
        }
    }
}
