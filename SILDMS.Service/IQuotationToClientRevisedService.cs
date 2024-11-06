using SILDMS.Model;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public interface IQuotationToClientRevisedService
    {
        ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> ownerLevelList);
        ValidationResult GetClientInfoList(out List<OBS_QuotationToClientRevised> ClientInfoSearchList);
        string SaveQuotationToClientRevised(OBS_QuotationToClientRevised woInfo, List<OBS_QuotationToClientRevisedItem> woInfoItem, List<OBS_QuotationToClientRevisedTerms> woInfoTerm);
        ValidationResult GetQuotationToClientRevisedSearchList(out List<OBS_QuotationToClientRevised> QuotationToClientRevisedSearchList);
        ValidationResult GetQuotationToClientRevisedItemList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList);
        ValidationResult GetQuotationToClientRevisedTermList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList);
        ValidationResult GetQuotationToClientRevisedSearchItemList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList);
        ValidationResult GetQuotationToClientRevisedSearchTermsList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList);
        ValidationResult GetQuotationToClientRevisedTermAgainstFormList(string TermsID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList);
        string DeleteQuotationToClientRevisedItemAndTerm(string QuotationToClientRevisedItemID, string QuotationToClientRevisedTermID);
        ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList);
    }
}
