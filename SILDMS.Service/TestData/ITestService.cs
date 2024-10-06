using SILDMS.Model.SecurityModule;
using SILDMS.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.TestData
{
    public interface ITestService
    {
        ValidationResult TestloadService(string _userId, out List<TestModel> test);

    }
}
