using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SILDMS.Model.SecurityModule;
using SILDMS.Service;
using SILDMS.Service.TestData;
using SILDMS.Utillity.Localization;
using SILDMS.Utillity;
using SILDMS.Web.UI.Areas.SecurityModule.Models;

namespace SILDMS.Web.UI.Areas.SecurityModule.Controllers
{
    public class TestController : Controller
    {
        // GET: SecurityModule/Test
        private readonly ITestService _test;
        private readonly ILocalizationService _localizationService;
        private string action = string.Empty;
        private string outStatus = string.Empty;
        private ValidationResult result;
        private readonly string _userId;
        public TestController(ITestService test, ILocalizationService localizationService)
        {
            _test = test;
            _localizationService = localizationService;
            result = new ValidationResult();
            _userId = SILAuthorization.GetUserID();
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task <dynamic> Testload()
            {


            
            
                List<TestModel> tests = null;

                await Task.Run(() => _test.TestloadService(_userId, out tests));

                var result = (from b in tests

                              select new TestModel
                              {
                                  Id = b.Id,
                                  Name = b.Name,
                                  Address = b.Address,
                                  Status = b.Status,
                                 

                              }).ToList();

                return Json(new { Msg = "", result }, JsonRequestBehavior.AllowGet);
            
        }














            //List<SILDMS.Model.CBPSModule.Sys_MasterData> masterDataList = null;

            //await Task.Run(() => _imasterSetupService.LoadMasterDataList(_userId, MasterDataTypeID, out masterDataList));

            //var result = (from b in masterDataList

            //              select new SILDMS.Model.CBPSModule.Sys_MasterData
            //              {
            //                  MasterDataID = b.MasterDataID,
            //                  MasterDataValue = b.MasterDataValue,
            //                  MasterDataTypeID = b.MasterDataTypeID,
            //                  OwnerID = b.OwnerID,
            //                  UserLevel = b.UserLevel,
            //                  Status = b.Status,
            //                  IsDefault = b.IsDefault,
            //                  SerialNo = b.SerialNo,
            //                  UDCode = b.UDCode,
            //                  ParrentID = b.ParrentID

    }
}