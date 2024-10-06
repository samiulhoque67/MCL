using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IClientRequisitionDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_ClientAndAddressInfo> GetClientInfoList();
        string SaveClientRequisition(OBS_ClientReq clientReq, List<OBS_ClientReqItem> clientReqItem, List<OBS_ClientReqTerms> clientReqTerm);
        List<OBS_ClientReq> GetClientReqSearchList();
        List<OBS_ClientReqItem> GetClientReqItemList(string ClientReqID);
        List<OBS_ClientReqTerms> GetClientReqTermList(string ClientReqID);
        List<OBS_ClientReqTerms> GetClientReqTermAgainstFormList(string TermsID);
        string DeleteClientReqItemAndTerm(string ClientReqItemID, string ClientReqTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}
