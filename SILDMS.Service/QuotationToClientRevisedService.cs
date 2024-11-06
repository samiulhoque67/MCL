using SILDMS.DataAccessInterface;
using SILDMS.Model;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service
{
    public class QuotationToClientRevisedService: IQuotationToClientRevisedService 
    {
        #region Fields

        private readonly IQuotationToClientRevisedDataService quotationToClientRevisedService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";

        #endregion

        #region Constractor

        public QuotationToClientRevisedService(IQuotationToClientRevisedDataService repository, ILocalizationService localizationService)
        {
            this.quotationToClientRevisedService = repository;
            this.localizationService = localizationService;
        }

        #endregion
        public ValidationResult GetServicesCategory(string action, out List<OBS_ServicesCategory> servicesCategoryList)
        {
            servicesCategoryList = quotationToClientRevisedService.GetServicesCategory(action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }
        public ValidationResult GetClientInfoList(out List<OBS_QuotationToClientRevised> ClientInfoList)
        {
            ClientInfoList = quotationToClientRevisedService.GetClientInfoList();
            return ValidationResult.Success;
        }
        public string SaveQuotationToClientRevised(OBS_QuotationToClientRevised woInfo, List<OBS_QuotationToClientRevisedItem> woInfoItem, List<OBS_QuotationToClientRevisedTerms> woInfoTerm)
        {
            return quotationToClientRevisedService.SaveQuotationToClientRevised(woInfo, woInfoItem, woInfoTerm);
        }
        public ValidationResult GetQuotationToClientRevisedSearchList(out List<OBS_QuotationToClientRevised> QuotationToClientRevisedSearchList)
        {
            QuotationToClientRevisedSearchList = quotationToClientRevisedService.GetQuotationToClientRevisedSearchList();
            return ValidationResult.Success;
        }
        public ValidationResult GetQuotationToClientRevisedItemList(string ClientID, out List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList)
        {
            QuotationToClientRevisedItemList = quotationToClientRevisedService.GetQuotationToClientRevisedItemList(ClientID);
            return ValidationResult.Success;
        }
        public ValidationResult GetQuotationToClientRevisedTermList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList)
        {
            QuotationToClientRevisedTermList = quotationToClientRevisedService.GetQuotationToClientRevisedTermList(QuotationToClientRevisedID);
            return ValidationResult.Success;
        }
        public ValidationResult GetQuotationToClientRevisedSearchItemList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedItem> QuotationToClientRevisedItemList)
        {
            QuotationToClientRevisedItemList = quotationToClientRevisedService.GetQuotationToClientRevisedSearchItemList(QuotationToClientRevisedID);
            return ValidationResult.Success;
        }
        public ValidationResult GetQuotationToClientRevisedSearchTermsList(string QuotationToClientRevisedID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList)
        {
            QuotationToClientRevisedTermList = quotationToClientRevisedService.GetQuotationToClientRevisedSearchTermsList(QuotationToClientRevisedID);
            return ValidationResult.Success;
        }
        public ValidationResult GetQuotationToClientRevisedTermAgainstFormList(string TermsID, out List<OBS_QuotationToClientRevisedTerms> QuotationToClientRevisedTermList)
        {
            QuotationToClientRevisedTermList = quotationToClientRevisedService.GetQuotationToClientRevisedTermAgainstFormList(TermsID);
            return ValidationResult.Success;
        }
        public string DeleteQuotationToClientRevisedItemAndTerm(string QuotationToClientRevisedItemID, string QuotationToClientRevisedTermID)
        {
            return quotationToClientRevisedService.DeleteQuotationToClientRevisedItemAndTerm(QuotationToClientRevisedItemID, QuotationToClientRevisedTermID);
        }
        public ValidationResult GetTermsConditionsList(out List<OBS_Terms> TermsConditionsList)
        {
            TermsConditionsList = quotationToClientRevisedService.GetTermsConditionsList();
            return ValidationResult.Success;
        }
    }
}
