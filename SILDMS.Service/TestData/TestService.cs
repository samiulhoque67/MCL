
using SILDMS.DataAccessInterface.TestData;
using SILDMS.Model.SecurityModule;
using SILDMS.Service;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.TestData
{
    public class TestService : ITestService
    {
        private readonly ITestData _service;
        private readonly ILocalizationService _localizationService;
        private string _errorNumber = string.Empty;
        public TestService(ITestData service, ILocalizationService localizationService)
        {
            _service = service;
            _localizationService = localizationService;
        }

        public ValidationResult TestloadService(string _userId, out List<TestModel> test)
        {
            test = _service.LoadTestData(_userId, out _errorNumber);

            if (_errorNumber.Length > 0)
            {
                return new ValidationResult(_errorNumber, _localizationService.GetResource(_errorNumber));
            }
            return ValidationResult.Success;
        }
    }
}
