using SILDMS.DataAccess.AdvanceApproval;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.AdvanceApproval
{
    public class AdvanceApprovalService : IAdvanceApprovalService
    {
        private readonly IAdvanceApprovalData _advanceApprovalData;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;

        public AdvanceApprovalService(IAdvanceApprovalData advanceApprovalData, ILocalizationService localizationService)
        {
            _advanceApprovalData = advanceApprovalData;
            _localizationService = localizationService;
            _errorNumber = string.Empty;
        }


    }
}
