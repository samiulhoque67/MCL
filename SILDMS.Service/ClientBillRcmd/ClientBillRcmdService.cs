using SILDMS.DataAccess.ClientFinalBillPrepare;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SILDMS.DataAccess.ClientBillRcmd;

namespace SILDMS.Service.ClientBillRcmd
{
    public class ClientBillRcmdService : IClientBillRcmdService
    {
        private readonly IClientBillRcmdData _clientBillRcmdData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public ClientBillRcmdService(IClientBillRcmdData clientBillRcmdData, ILocalizationService localizationService)
        {
            _clientBillRcmdData = clientBillRcmdData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _clientBillRcmdData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<VendorBillRecvd> vendorReqList)
        {
            vendorReqList = _clientBillRcmdData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveClientFinalBill(VendorBillRecvd billRecv)
        {
            return _clientBillRcmdData.SaveClientFinalBill(billRecv);
        }
    }
}
