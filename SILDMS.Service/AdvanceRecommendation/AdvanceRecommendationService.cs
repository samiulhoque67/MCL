using SILDMS.DataAccess.AdvanceApproval;
using SILDMS.DataAccess.AdvanceRecommendation;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceRecommendation
{
    public class AdvanceRecommendationService : IAdvanceRecommendationService
    {
        private readonly IAdvanceRecommendationData _advanceRecommendationData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceRecommendationService(IAdvanceRecommendationData advanceRecommendationData, ILocalizationService localizationService)
        {
            _advanceRecommendationData = advanceRecommendationData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }


    }
}
