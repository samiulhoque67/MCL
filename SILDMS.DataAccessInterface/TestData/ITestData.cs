﻿using SILDMS.Model.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface.TestData
{
    public interface ITestData
    {
        List<TestModel> LoadTestData(string _userId, out string _errorNumber);
    }
}
