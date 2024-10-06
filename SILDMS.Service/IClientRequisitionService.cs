using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IClientRequisitionService
    {
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetClientInfoList(out List<OBS_ClientAndAddressInfo> ClientInfoSearchList);
        string SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm);
        ValidationResult GetClientReqSearchList(out List<OBS_ClientReq> ClientReqSearchList);
        ValidationResult GetClientReqItemList(string ClientReqID,out List<OBS_ClientReqItem> ClientReqItemList);
        ValidationResult GetClientReqTermList(string ClientReqID, out List<OBS_ClientReqTerms> ClientReqTermList);
        ValidationResult GetClientReqTermAgainstFormList(string TermsID, out List<OBS_ClientReqTerms> ClientReqTermList);
        string DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
