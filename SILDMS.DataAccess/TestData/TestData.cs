using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.DataAccessInterface.TestData;
using SILDMS.Model.SecurityModule;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.TestData
{
    public class TestData : ITestData
    {
        private readonly string spStatusParam = "@p_Status";
        public List<TestModel> LoadTestData(string _userId, out string _errorNumber)
        {

            _errorNumber = string.Empty;
            List<TestModel> test = new List<TestModel>();

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetAll"))
            {



                db.AddOutParameter(dbCommandWrapper, spStatusParam, DbType.String, 10);

                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        test = dt1.AsEnumerable().Select(reader => new TestModel
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Address = reader.GetString("Address")
                            //,
                            
                            //Status = reader.GetInt32("Status")



                        }).ToList();
                    }
                }
            }
            return test;
        }
    }
}
