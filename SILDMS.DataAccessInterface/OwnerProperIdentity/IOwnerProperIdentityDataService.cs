﻿using System.Collections.Generic;
using SILDMS.Model.DocScanningModule;

namespace SILDMS.DataAccessInterface.OwnerProperIdentity
{
    public interface IOwnerProperIdentityDataService
    {
        List<DSM_DocPropIdentify> GetDocPropIdentify(string _UserID, string docPropIdentifyID, out string errorNumber);
        List<DSM_DocPropIdentify> GetIdentificationAttributesForDocProperties(
            string _UserID, string _SelectedPropID, out string errorNumber);
        string AddOwnerPropIdentity(DSM_DocPropIdentify modelDsmDocPropIdentify, string action, out string errorNumber);
    }
}
