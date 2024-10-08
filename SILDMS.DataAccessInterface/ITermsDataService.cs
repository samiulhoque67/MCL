﻿using SILDMS.Model.CBPSModule;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccessInterface
{
    public interface ITermsDataService
    {
        string SaveTermsMst(OBS_Terms _modelTermsMst);
        string SaveTermsItem(OBS_TermsItem _modelTermsMst);
        List<OBS_ServicesCategory> GetServicesCategory(string action, out string errorNumber);
        List<OBS_Terms> GetTermsSearchList();
        List<OBS_TermsItem> GetTermsItemList(string ClientID);
        List<Sys_MasterData> GetJobLocation(string UserID, out string errorNumber);
    }
}
