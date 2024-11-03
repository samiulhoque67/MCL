using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface IQuotationToClientRevisedDataService
    {
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_QuotationToClientRevised> GetClientInfoList();
        string SaveQuotationToClientRevised(OBS_QuotationToClientRevised woInfo, List<OBS_QuotationToClientRevisedItem> woInfoItem, List<OBS_QuotationToClientRevisedTerms> woInfoTerm);
        List<OBS_QuotationToClientRevised> GetQuotationToClientRevisedSearchList();
        List<OBS_QuotationToClientRevisedItem> GetQuotationToClientRevisedItemList(string QuotationToClientRevisedID);
        List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedTermList(string QuotationToClientRevisedID);
        List<OBS_QuotationToClientRevisedItem> GetQuotationToClientRevisedSearchItemList(string QuotationToClientRevisedID);
        List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedSearchTermsList(string QuotationToClientRevisedID);
        List<OBS_QuotationToClientRevisedTerms> GetQuotationToClientRevisedTermAgainstFormList(string TermsID);
        string DeleteQuotationToClientRevisedItemAndTerm(string QuotationToClientRevisedItemID, string QuotationToClientRevisedTermID);
        List<OBS_Terms> GetTermsConditionsList();
    }
}
