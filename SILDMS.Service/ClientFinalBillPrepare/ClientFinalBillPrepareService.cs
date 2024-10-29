using SILDMS.DataAccess.ClientFinalBillPrepare;
using SILDMS.Model;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.ClientFinalBillPrepare
{
    public class ClientFinalBillPrepareService : IClientFinalBillPrepareService
    { private readonly IClientFinalBillPrepareData _clientFinalBillPrepareData;
        private readonly ILocalizationService _localizationService;
        private string errorNumber = "";

        public ClientFinalBillPrepareService(IClientFinalBillPrepareData clientFinalBillPrepareData,ILocalizationService localizationService)
        {
            _clientFinalBillPrepareData = clientFinalBillPrepareData;
            _localizationService = localizationService;
        }

        public ValidationResult GetQutnSearchList(out List<VendorBillRecvd> poSearchList)
        {
            poSearchList = _clientFinalBillPrepareData.GetQutnSearchList();
            return ValidationResult.Success;
        }

        public ValidationResult GetShowClientReqList(out List<OBS_VendorQutn> vendorReqList)
        {
            vendorReqList = _clientFinalBillPrepareData.GetShowClientReqList();
            return ValidationResult.Success;
        }

        public string SaveClientFinalBill(VendorBillRecvd billRecv)
        {
            return _clientFinalBillPrepareData.SaveClientFinalBill(billRecv);
        }
    }
}
