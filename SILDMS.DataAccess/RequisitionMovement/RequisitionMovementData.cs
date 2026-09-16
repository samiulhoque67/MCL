using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace SILDMS.DataAccess.RequisitionMovement
{
    public class RequisitionMovementData : IRequisitionMovementData
    {
        private readonly string _spStatusParam;

        public RequisitionMovementData()
        {
            _spStatusParam = "@p_Status";
        }

        public List<OBS_RequisitionMovementInfo> GetRequisitionMovementInfo(string clientReqId, out string _errorNumber)
        {
            _errorNumber = string.Empty;
            var movementList = new List<OBS_RequisitionMovementInfo>();
            var factory = new DatabaseProviderFactory();
            var db = factory.CreateDefault() as SqlDatabase;

            using (var dbCommandWrapper = db.GetStoredProcCommand("OBS_GetRequisitionMovementInfo"))
            {
                db.AddInParameter(dbCommandWrapper, "@ClientReqID", SqlDbType.NVarChar, clientReqId);
                db.AddOutParameter(dbCommandWrapper, _spStatusParam, DbType.String, 10);
                dbCommandWrapper.CommandTimeout = 300;

                var ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, _spStatusParam).IsNullOrZero())
                {
                    _errorNumber = db.GetParameterValue(dbCommandWrapper, _spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count <= 0) return movementList;

                    var dt1 = ds.Tables[0];
                    movementList = dt1.AsEnumerable().Select(reader => new OBS_RequisitionMovementInfo
                    {
                        ClientReqID = reader.GetString("ClientReqID"),
                        ClientReqNo = reader.GetString("ClientReqNo"),
                        RequisitionDate = reader.GetString("RequisitionDate"),
                        ClientReqStatus = reader.GetString("ClientReqStatus"),
                        ClientName = reader.GetString("ClientName"),

                        ClientReqItemID = reader.GetString("ClientReqItemID"),
                        ServiceItemID = reader.GetString("ServiceItemID"),
                        ServiceItemName = reader.GetString("ServiceItemName"),
                        ReqType = reader.GetString("ReqType"),
                        DeliveryLocation = reader.GetString("DeliveryLocation"),
                        DeliveryDate = reader.GetString("DeliveryDate"),

                        CsStatus = reader.GetString("CsStatus"),

                        VendorReqID = reader.GetString("VendorReqID"),
                        VendorReqDate = reader.GetString("VendorReqDate"),
                        VendorReqRaised = reader.GetString("VendorReqRaised") == "1",

                        FirstVendorQutnDate = reader.GetString("FirstVendorQutnDate"),
                        VendorQutnCount = string.IsNullOrEmpty(reader.GetString("VendorQutnCount")) ? 0 : Convert.ToInt32(reader.GetString("VendorQutnCount")),
                        VendorQutnReceived = reader.GetString("VendorQutnReceived") == "1",

                        CSRecommended = reader.GetString("CSRecommended") == "1",
                        CSRecmDate = reader.GetString("CSRecmDate"),

                        CSApproved = reader.GetString("CSApproved") == "1",
                        CSAprvDate = reader.GetString("CSAprvDate"),

                        CSActualApproved = reader.GetString("CSActualApproved") == "1",
                        CSActualAprvDate = reader.GetString("CSActualAprvDate"),

                        WorkOrderRaised = reader.GetString("WorkOrderRaised") == "1",
                        WODate = reader.GetString("WODate"),

                        PurchaseOrderRaised = reader.GetString("PurchaseOrderRaised") == "1",
                        PODate = reader.GetString("PODate")
                    }).ToList();
                }
            }

            return movementList;
        }
    }
}