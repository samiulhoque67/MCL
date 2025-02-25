﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SILDMS.Model.CBPSModule;
//using SILDMS.Model.CheckPrintModule;
using SILDMS.Model.DocScanningModule;
//using SILDMS.Model.LCModule;
using SILDMS.Model.SecurityModule;
//using SILDMS.Service.ChecquePrint;
using SILDMS.Service.DocumentCategory;
using SILDMS.Service.EmailMessage;
using SILDMS.Service.Owner;
using SILDMS.Service.Reports;
//using SILDMS.Service.TDSVDS;
using SILDMS.Service.Users;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using SILDMS.Web.UI.Areas.SecurityModule;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using SILDMS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PdfSharp.Pdf.IO;
//using SILDMS.Service.BackgroundTaskService;
using SILDMS.Model.BackgroundTaskModule;
using System.Web.Http.Results;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILDMS.DataAccess;
using System.Data.Common;
using SILDMS.Model;
using System.Web.Services.Description;
/////////////////////////////////////////Test///////////////////////////
namespace SILDMS.Web.UI.Controllers
{
    public class ReportsController : Controller
    {
        readonly IUserService _userService;
        readonly IOwnerService _ownerService;
        readonly IReportsService _reportService;
        private readonly ILocalizationService _localizationService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        private string action = string.Empty;
        readonly IDocCategoryService _docCategoryService;
        //private readonly IEmailMessageService _emailMessageService;
        private string res_code = string.Empty;
        private string res_message = string.Empty;

        public ReportsController(IOwnerService ownerService, IUserService usrRepository, IReportsService reportService, ILocalizationService localizationService, IDocCategoryService docCategoryService)//, IEmailMessageService emailMessageService)//, IChequePrintService repository, ITdsVdsService tdsVdsService, IBackgroundTaskService backgroundTaskService)
        {
            this._ownerService = ownerService;
            this._userService = usrRepository;
            //this._chequePrintService = repository;
            this._localizationService = localizationService;
            this._reportService = reportService;
            UserID = SILAuthorization.GetUserID();
            _docCategoryService = docCategoryService;
            //_tdsVdsService = tdsVdsService;
            //this._emailMessageService = emailMessageService;
            //this._backgroundTaskService = backgroundTaskService;
        }

        public ActionResult ExportChecqueInfoInXls()
        {
            return View();
        }

        public ActionResult viewpdf()
        {
            return View();
        }
        [HttpPost]
        public ActionResult viewpdf(string a)
        {
            //string path = @"D:\inputPdf\doc.pdf";
            string path = Server.MapPath("~/Buffer/inputPdf/doc.pdf");

            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }

            Array.ForEach(Directory.GetFiles(Server.MapPath("~/Buffer/inputPdf/")), System.IO.File.Delete);
            return View("viewpdf");
        }

        [HttpPost]
        public JsonResult UploadExcelFile()
        {
            //TempData.Remove("ExcelData");
            HttpPostedFile httpPostedFileBase = System.Web.HttpContext.Current.Request.Files[0];
            if (httpPostedFileBase != null)
            {
                string[] file = httpPostedFileBase.FileName.Split('.');
                if (file.Length > 0)
                {
                    if ((file[file.Length - 1].ToString()) == "xlsx" || (file[file.Length - 1].ToString() == "xls"))
                    {
                        DataTable dt;
                        ExcelFileReader xlReader = new ExcelFileReader();
                        dt = xlReader.GetExcelDataTable(HttpContext.Request.Files[0]);
                        TempData["ExcelData"] = dt;
                    }
                }
            }
            else
            {
                return Json(new { Code = "0" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Code = "1" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        private void CopyPages(PdfSharp.Pdf.PdfDocument from, PdfSharp.Pdf.PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        [SILAuthorize]
        public ActionResult UserActivityStatusReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> UserActivityStatusReport(ReportModel model)
        {
            DataTable dt = new DataTable();

            //if (string.IsNullOrEmpty(model.BillReceiveFromDate))
            //{
            //    if (string.IsNullOrEmpty(model.BillReceiveToDate))
            //    {
            //        model.BillReceiveFromDate = model.BillReceiveFromDate;
            //        model.BillReceiveToDate = model.BillReceiveToDate;
            //    }
            //    else
            //        model.BillReceiveFromDate = model.BillReceiveToDate;
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(model.BillReceiveToDate))
            //        model.BillReceiveToDate = model.BillReceiveFromDate;
            //}

            //await Task.Run(() => _reportService.GetRptUserActivityStatus(model.BillReceiveFromDate, model.BillReceiveToDate, model.UserRptID, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptUserActivityStatus.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(model.UserRptID));
            reportDocument.SetParameterValue("rptName", "User Activity Status");
            reportDocument.SetParameterValue("ProcessBy", GetUserName(model.UserRptID));
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));
            //reportDocument.SetParameterValue("fromDate", model.BillReceiveFromDate);
            //reportDocument.SetParameterValue("toDate", model.BillReceiveToDate);

            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "UserActivityStatus");
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "UserActivityStatus");
                else if (model.ReportType == "EXCEL")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                        "attachment;filename=DataTable.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";


                    StringBuilder sb = new StringBuilder();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Columns[k].ColumnName + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            //add separator
                            sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                        }
                        //append new line
                        sb.Append("\r\n");
                    }
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();

                }
                //reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, "UserActivityStatus");
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, "UserActivityStatus");
            }
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        public class SMSProprties
        {
            public string MobileNo { get; set; }
            public string Text { get; set; }
            public string TokenNo { get; set; }
        }

        [SILAuthorize]
        public ActionResult QuotationToClientReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> QuotationToClientReport(string ReportType)
        {
            var tempdata = TempData["QuotationToClientReport"];

            ReportType = "PDF";
            if (TempData["VendorRequisition"] == null)
            {
                ViewBag.Title = "No valid data.";
                return View();
            }

            OBS_QutntoClientMaster objVendorReq = new OBS_QutntoClientMaster();
            objVendorReq = (OBS_QutntoClientMaster)TempData["VendorRequisition"];
            string ClientQuotationID = objVendorReq.ClientQuotationID;

            DataTable dt = new DataTable();

            //await Task.Run(() => _reportService.VendorCSApprevedReport(model.UserRptID, model.BillReceiveFromDate, model.Status, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptQuotationtoClient.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            //reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            //reportDocument.SetParameterValue("rptName", "User Details");
            //reportDocument.SetParameterValue("rptUser", GetUserName(UserID));


            string reportName = "rptQuotationtoClient";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);

            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }


        [SILAuthorize]
        public ActionResult VendorCSApprevedReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> VendorCSApprevedReport(string ReportType)
        {
            var tempdata = TempData["VendorCSRecmInfo"];
            string VendorReqID = string.Empty, ServiceItemID = string.Empty;
            ReportType = "PDF";
            OBS_VendorCSAprv objVendorReq = new OBS_VendorCSAprv();

            if (TempData["VendorCSRecmInfo"] == null)
            {
                ViewBag.Title = "No valid data.";
                //return View();
            }
            else
            {
                objVendorReq = (OBS_VendorCSAprv)TempData["VendorCSRecmInfo"];
                VendorReqID = objVendorReq.VendorReqID;
                ServiceItemID = objVendorReq.ServiceItemID;
            }

            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.VendorCSApprevedReport(VendorReqID, ServiceItemID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptVendorCSRecmInfo.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            reportDocument.SetParameterValue("RecmVendor", objVendorReq.CSRecmVendorName);
            reportDocument.SetParameterValue("RecmBy", objVendorReq.RecommendedByName);
            reportDocument.SetParameterValue("RecmDesig", objVendorReq.RecommendedByDesignation);

            //reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            //reportDocument.SetParameterValue("RecmVendor", "Square Informatix Ltd.");
            //reportDocument.SetParameterValue("rptUser", GetUserName(UserID));


            string reportName = "VendorCSApprevedReport";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        //[SILAuthorize]
        public ActionResult RequisitionToVendorReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> RequisitionToVendorReport(string ReportType)
        {
            var tempdata = TempData["VendorRequisition"];
            string VendorReqID = string.Empty, VendorID = string.Empty;
            ReportType = "PDF";

            OBS_VendorReq objVendorReq = new OBS_VendorReq();

            if (TempData["VendorRequisition"] == null)
            {
                ViewBag.Title = "No valid data.";
                //return View();
            }
            else
            {
                objVendorReq = (OBS_VendorReq)TempData["VendorRequisition"];
                VendorReqID = objVendorReq.VendorReqID;
                VendorID = objVendorReq.VendorID;
            }

            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.VendorRequisitionReport(VendorReqID, VendorID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptVendorRequisition.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            //reportDocument.SetParameterValue("RequisitionNo", objVendorReq.RequisitionNo);
            //reportDocument.SetParameterValue("RequisitionDate", objVendorReq.RequisitionDate);

            //reportDocument.SetParameterValue("submittedby", objVendorReq.Submittedby);
            //reportDocument.SetParameterValue("submittedbyDesig", objVendorReq.SubmittedbyDesig);

            string reportName = "rptVendorRequisition";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }


        //[SILAuthorize]
        public ActionResult AgeingReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> AgeingReport(string ReportType)
        {
            var tempdata = TempData["VendorRequisition"];
            string VendorReqID = string.Empty, VendorID = string.Empty;
            ReportType = "PDF";

            OBS_VendorReq objVendorReq = new OBS_VendorReq();

            if (TempData["VendorRequisition"] == null)
            {
                ViewBag.Title = "No valid data.";
                //return View();
            }
            else
            {
                objVendorReq = (OBS_VendorReq)TempData["VendorRequisition"];
                VendorReqID = objVendorReq.VendorReqID;
                VendorID = objVendorReq.VendorID;
            }

            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.VendorRequisitionReport(VendorReqID, VendorID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptVendorRequisition.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            string reportName = "rptVendorRequisition";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        [SILAuthorize]
        public ActionResult ClientAprvBillReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> ClientAprvBillReport(string ReportType)
        {
            var tempdata = TempData["ClientAprvBill"];
            string WoinfoID = string.Empty;
            string BillCategory = string.Empty;
            int InstallmentNo=0;
            ReportType = "PDF";
            VendorBillRecvd BillRecv = new VendorBillRecvd();
            int ClientBillAprvID = 0;

            if (TempData["ClientAprvBill"] == null)
            {
                ViewBag.Title = "No valid data.";
                //return View();
            }
            else
            {
                BillRecv = (VendorBillRecvd)TempData["ClientAprvBill"];
                //BillRecv = (VendorBillRecvd)TempData["ClientAprvBill"];
                ClientBillAprvID = Convert.ToInt32(TempData["ClientBillAprvID"]);

                WoinfoID = BillRecv.WOInfoID;
                InstallmentNo = BillRecv.WOInstallmentNo;
                BillCategory = BillRecv.BillCategory;
            }

            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.ClientAprvBillReport(WoinfoID, InstallmentNo, ClientBillAprvID, BillCategory, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptClientAprvBill.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

        
            //reportDocument.SetParameterValue("RecmBy", BillRecv.RecommendedByName);
            //reportDocument.SetParameterValue("RecmDesig", BillRecv.RecommendedByDesignation);

            //reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            //reportDocument.SetParameterValue("RecmVendor", "Square Informatix Ltd.");
            //reportDocument.SetParameterValue("rptUser", GetUserName(UserID));


            string reportName = "ClientAprvBill";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }


        [SILAuthorize]
        public ActionResult ClientQuotationApproveReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> ClientQuotationApproveReport(string ReportType)
        {
           /* TempData["ClientQutnAprv"] = MasterData;
            TempData["ClientQutnAprvID"] = ClientQutnAprvID;*/

            var tempdata = TempData["ClientQutnAprv"];
            string WoinfoID = string.Empty;
            int InstallmentNo = 0;
            ReportType = "PDF";
            OBS_QutntoClientMaster ClientQutnAprv = new OBS_QutntoClientMaster();
            string ClientQutnAprvID = string.Empty;

            if (TempData["ClientQutnAprv"] == null)
            {
                ViewBag.Title = "No valid data.";
                //return View();
            }
            else
            {
                ClientQutnAprv = (OBS_QutntoClientMaster)TempData["ClientQutnAprv"];
                ClientQutnAprvID = Convert.ToString(TempData["ClientQutnAprvID"]);
            }

            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.ClientQuotationApproveReport(ClientQutnAprvID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptClientQuotationApprove.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();


            string reportName = "ClientQuotationApproveReport";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }


        [SILAuthorize]
        public ActionResult UserDetails()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> UserDetails(ReportModel model)
        {
            DataTable dt = new DataTable();

            //await Task.Run(() => _reportService.GetRptUserDetails(model.UserRptID, model.BillReceiveFromDate, model.Status, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptUserDetails.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            reportDocument.SetParameterValue("rptName", "User Details");
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));


            string reportName = "UserDetails";
            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        //[SILAuthorize]
        public ActionResult RequisitionMovementInfo()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> RequisitionMovementInfo(ReportModel model)
        {
            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.RequisitionMovementInfo(model.RequisitionNo, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptRequisitionMovementInfo.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            //reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            //reportDocument.SetParameterValue("rptName", "User Details");
            //reportDocument.SetParameterValue("rptUser", GetUserName(UserID));

            string reportName = "RequisitionMovementInfo";
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            //if (model.ButtonType == "Preview")
            //    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            //else
            //{
            //    if (model.ReportType == "PDF")
            //        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
            //    else if (model.ReportType == "EXCEL")
            //        reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
            //    else
            //        reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            //}
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        //[SILLogAttribute]
        public async Task<dynamic> SaveMasterDetailData(string Dept, List<SILDMS.Model.CBPSModule.Sys_MasterData> addList, List<SILDMS.Model.CBPSModule.Sys_MasterData> editList)
        //public async Task<dynamic> SaveMasterDetailData(string _dept, List<SILDMS.Model.CBPSModule.Sys_MasterData> _masterData)
        {
            DataTable addData = new DataTable();
            addData.Columns.Add("StuID");
            addData.Columns.Add("Name");
            addData.Columns.Add("Address");
            if (addList != null)
            {
                foreach (var item in addList)
                {
                    DataRow objDataRow = addData.NewRow();

                    objDataRow[0] = null;
                    objDataRow[1] = item.Name;
                    objDataRow[2] = item.Address;
                    addData.Rows.Add(objDataRow);
                }
            }

            DataTable editData = new DataTable();
            editData.Columns.Add("StuID");
            editData.Columns.Add("Name");
            editData.Columns.Add("Address");
            if (editList != null)
            {
                foreach (var item in editList)
                {
                    DataRow objDataRow = editData.NewRow();

                    objDataRow[0] = item.Id;
                    objDataRow[1] = item.Name;
                    objDataRow[2] = item.Address;
                    editData.Rows.Add(objDataRow);
                }
            }

            //string spStatusParam = "@p_Status";
            string _errorNumber = String.Empty;


            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;

            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("CBPS_SetInsertUpdate"))
            {
                try
                {
                    db.AddInParameter(dbCommandWrapper, "@addList", SqlDbType.Structured, addData);
                    db.AddInParameter(dbCommandWrapper, "@editList", SqlDbType.Structured, editData);
                    DataSet ds = db.ExecuteDataSet(dbCommandWrapper);
                    _errorNumber = "1";
                }
                catch (Exception ex)
                {
                    _errorNumber = ex.Message;
                }
            }
            return Json(new { _errorNumber }, JsonRequestBehavior.AllowGet);
        }

        [SILAuthorize]
        public ActionResult OwnerList()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> OwnerList(ReportModel model)
        {
            DataTable dt = new DataTable();
            //await Task.Run(() => _reportService.GetRptOwnerList(model.OwnerLevelID, model.Company, model.ParentOwnerID, model.Status, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptOwnerList.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            if (string.IsNullOrEmpty(model.Company))
                reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            else
                reportDocument.SetParameterValue("ComDiv", GetCompanyName(model.Company));
            reportDocument.SetParameterValue("rptName", "Owner List");
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));

            string reportName = GetCompanyShortName(model.Company) + "-" + "OwnerList";
            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }
        [SILAuthorize]
        public ActionResult DocumentsList()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> DocumentsList(ReportModel model)
        {
            DataTable dt = new DataTable();
            //await Task.Run(() => _reportService.GetRptDocumentsList(model.OwnerLevelID, model.OwnerID, model.DocCategoryID, model.DocTypeID, model.DocPropertyID, model.Status, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptDocumentsList.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            if (string.IsNullOrEmpty(model.Company))
                reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            else
                reportDocument.SetParameterValue("ComDiv", GetCompanyName(model.Company));
            reportDocument.SetParameterValue("rptName", "Documents List");
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));

            string reportName = GetCompanyShortName(model.Company) + "-" + "DocumentsList";
            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }
        [SILAuthorize]
        public ActionResult RoleList()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> RoleList(ReportModel model)
        {
            DataTable dt = new DataTable();
            //await Task.Run(() => _reportService.GetRptRoleList(model.OwnerLevelID, model.OwnerID, model.RoleID, model.Status, "", UserID, out dt));

            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptRoleList.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();
            if (string.IsNullOrEmpty(model.Company))
                reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            //else
            //reportDocument.SetParameterValue("ComDiv", GetCompanyName(model.OwnerID));
            reportDocument.SetParameterValue("rptName", "Role List");
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));

            string reportName = string.Empty;//GetCompanyShortName(model.OwnerID) + "-" + "RoleList";
            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }
            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }

        [HttpPost]
        public JsonResult UploadUpdatedChequeNoExcelFile()
        {
            TempData.Remove("ExcelUpdatedChequeNoData");
            HttpPostedFile httpPostedFileBase = System.Web.HttpContext.Current.Request.Files[0];
            if (httpPostedFileBase != null)
            {
                string[] file = httpPostedFileBase.FileName.Split('.');
                if (file.Length > 0)
                {
                    if ((file[file.Length - 1].ToString()) == "xlsx" || (file[file.Length - 1].ToString() == "xls"))
                    {
                        DataTable dt;
                        ExcelFileReader xlReader = new ExcelFileReader();
                        dt = xlReader.GetExcelDataTable(HttpContext.Request.Files[0]);
                        TempData["ExcelUpdatedChequeNoData"] = dt;
                    }
                }
            }
            else
            {
                return Json(new { Code = "0" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Code = "1" }, JsonRequestBehavior.AllowGet);
        }

        public string GetUserName(string UserID)
        {
            List<SEC_User> obUser = null;
            _userService.GetAllUser(UserID, "", out obUser);
            return obUser[0].UserName;
        }

        public string GetUserFullName(string UserID)
        {
            List<SEC_User> obUser = null;
            _userService.GetAllUser(UserID, "", out obUser);
            return obUser[0].UserFullName;
        }

        public string GetUserDesignation(string UserID)
        {
            List<SEC_User> obUser = null;
            _userService.GetAllUser(UserID, "", out obUser);
            return obUser[0].UserDesignation;
        }

        public string GetUserPhoneNo(string UserID)
        {
            List<SEC_User> obUser = null;
            _userService.GetAllUser(UserID, "", out obUser);
            return obUser[0].ContactNo;
        }

        public string GetUserEmail(string UserID)
        {
            List<SEC_User> obUser = null;
            _userService.GetAllUser(UserID, "", out obUser);
            return obUser[0].IntMailAddress;
        }

        public string GetCompanyName(string ownerId)
        {
            var owner = new List<DSM_Owner>();
            _ownerService.GetAllOwners(ownerId, UserID, out owner);
            return owner[0].OwnerName;
        }

        public string GetCompanyOrOwnerNameByUserID(string UserID)
        {
            //_reportService.GetCompanyOrOwnerNameByUserID(UserID);
            return _reportService.GetCompanyOrOwnerNameByUserID(UserID);
        }

        public string GetOwnerAddress(string ownerId)
        {
            var ownerAddress = new List<DSM_OwnerAddress>();
            _ownerService.GetOwnerAddress(ownerId, UserID, out ownerAddress);
            if (ownerAddress.Count > 0)
                return (ownerAddress[0].OwnerAddress1 + ownerAddress[0].OwnerAddress2);
            else
                return "";
        }

        public string GetBINNo(string ownerId)
        {
            var owner = new List<DSM_OwnerAddress>();
            _ownerService.GetOwnerAddress(ownerId, UserID, out owner);
            if (owner.Count > 0)
                return owner[0].OwnerBINNo;
            else
                return "";
        }

        public string GetTINNo(string ownerId)
        {
            var owner = new List<DSM_OwnerAddress>();
            _ownerService.GetOwnerAddress(ownerId, UserID, out owner);
            if (owner.Count > 0)
                return owner[0].OwnerTINNo;
            else
                return "";
        }

        public string GetVendorNameAndAddress(string vendorID)
        {
            var RptVendorWithAddressList = new List<RptVendorWithAddress>();
            _reportService.GetVendorNameAndAddress(vendorID, out RptVendorWithAddressList);
            if (RptVendorWithAddressList.Count > 0)
                return RptVendorWithAddressList[0].VendorName + "\n" + RptVendorWithAddressList[0].VendorAddress;
            //return RptVendorWithAddressList[0].VendorName.Substring(10, RptVendorWithAddressList[0].VendorName.Length - 10) + RptVendorWithAddressList[0].VendorAddress;
            else
                return "";
        }

        public string GetVendorName(string vendorID)
        {
            var RptVendorWithAddressList = new List<RptVendorWithAddress>();
            _reportService.GetVendorNameAndAddress(vendorID, out RptVendorWithAddressList);
            if (RptVendorWithAddressList.Count > 0)
                return RptVendorWithAddressList[0].VendorName;
            else
                return "";
        }

        public string GetVendorAddress(string vendorID)
        {
            var RptVendorWithAddressList = new List<RptVendorWithAddress>();
            _reportService.GetVendorNameAndAddress(vendorID, out RptVendorWithAddressList);
            if (RptVendorWithAddressList.Count > 0)
                return RptVendorWithAddressList[0].VendorAddress;
            else
                return "";
        }

        public string GetVendorEmailAddress(string vendorID)
        {
            var RptVendorWithAddressList = new List<RptVendorWithAddress>();
            _reportService.GetVendorNameAndAddress(vendorID, out RptVendorWithAddressList);
            if (RptVendorWithAddressList.Count > 0)
                return RptVendorWithAddressList[0].VendorEmail;
            else
                return "";
        }

        public string GetVenTINNo(string vendorID)
        {
            var RptVendorWithAddressList = new List<RptVendorWithAddress>();
            _reportService.GetVendorNameAndAddress(vendorID, out RptVendorWithAddressList);
            if (RptVendorWithAddressList.Count > 0)
                return RptVendorWithAddressList[0].TIN;
            else
                return "";
        }

        public string GetCompanyShortName(string ownerId)
        {
            string OwnerName = "";
            if (!string.IsNullOrEmpty(ownerId))
            {
                var owner = new List<DSM_Owner>();
                _ownerService.GetAllOwners(ownerId, UserID, out owner);
                if (owner.Count > 0)
                    OwnerName = owner[0].OwnerShortName;
                else
                    return "";
            }
            return OwnerName;
        }

        public string GetCompanyCode(string ownerId)
        {
            string companyCode = "";
            if (!string.IsNullOrEmpty(ownerId))
            {
                var owner = new List<DSM_Owner>();
                _ownerService.GetAllOwners(ownerId, UserID, out owner);
                if (owner.Count > 0)
                    companyCode = owner[0].UDOwnerCode;
                else
                    return "";
            }
            return companyCode;
        }

        [Authorize]
        [HttpGet]
        public async Task<dynamic> GetDocCategoryByOwner(string id)
        {
            var docCategories = new List<DSM_DocCategory>();
            await Task.Run(() => _docCategoryService.GetDocCategoriesByOwner(id, out docCategories));
            var result = docCategories.Where(ob => ob.OwnerID == id).Select(x => new
            {
                x.DocCategoryID,
                x.OwnerID,
                x.DocCategorySL,
                x.UDDocCategoryCode,
                x.DocCategoryName,
                x.Status
            }).OrderBy(ob => ob.DocCategoryID);
            return Json(new { result, Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        private string getHtml(DataTable dt1)
        {
            try
            {
                string messageBody = "<font>Reference No : " + dt1.Rows[0].ItemArray[16].ToString() + " </font><br>" +
                    "<font> Date: " + (DateTime.Now).ToString("dd/MM/yyyy") + "</font><br><br>" +
                    "<font>" + dt1.Rows[0].ItemArray[11].ToString() + "</font><br>" +
                    "<font>" + dt1.Rows[0].ItemArray[12].ToString() + "</font><br><br><br>";

                messageBody += "<font>Sub: Request for Insurance Cover Note Issue</font><br>" +
               "<font>Ref : Enclosed please find herewith the purchase order and proforma invoice relating to below data kindly acknowledge and favor us with an early insurance cover note issue.</font><br><br>";
                if (dt1.Rows.Count == 0)
                {
                    ViewBag.Title = "No data Found.";
                    return messageBody;
                }
                string htmlTableStart = "<table style=\"border-collapse:collapse; border: 1px solid black; text-align:center;\" >";
                string htmlTableEnd = "</table>";

                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2;border: 1px solid black; color:#ffffff;\" >";
                string htmlHeaderRowEnd = "</tr>";

                string htmlTrStart = "<tr style=\"border: 1px solid #ddd;padding: 8px;\" >";
                string htmlTrEnd = "</tr>";

                string htmlTdStart = "<td style=\"border: 1px solid #ddd;padding: 8px;\" >";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                //messageBody += htmlTrStart;
                //messageBody += htmlTdStart + "Request No" + htmlTdEnd;
                messageBody += htmlTdStart + "PO No" + htmlTdEnd;
                messageBody += htmlTdStart + "Benificiary" + htmlTdEnd;
                messageBody += htmlTdStart + "Manufacturer" + htmlTdEnd;
                messageBody += htmlTdStart + "LC Amt." + htmlTdEnd;
                messageBody += htmlTdStart + "LC Currency" + htmlTdEnd;
                messageBody += htmlTdStart + "PI No." + htmlTdEnd;
                messageBody += htmlTdStart + "PI Date" + htmlTdEnd;
                messageBody += htmlTdStart + "LC Opening Bank" + htmlTdEnd;
                //messageBody += htmlTdStart + "10" + htmlTdEnd;
                //messageBody += htmlTdStart + "11" + htmlTdEnd;
                //messageBody += htmlTdStart + "12" + htmlTdEnd;
                //messageBody += htmlTdStart + "13" + htmlTdEnd;
                //messageBody += htmlTdStart + "14" + htmlTdEnd;
                //messageBody += htmlTdStart + "15" + htmlTdEnd;
                //messageBody += htmlTdStart + "16" + htmlTdEnd;
                //messageBody += htmlTdStart + "17" + htmlTdEnd;
                //messageBody += htmlTrEnd;
                messageBody += htmlHeaderRowEnd;
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    for (int j = 0; j <= dt1.Columns.Count - 10; j++)
                    {
                        string a = dt1.Rows[i].ItemArray[j].ToString();

                        messageBody = messageBody + htmlTdStart + dt1.Rows[i].ItemArray[j].ToString() + htmlTdEnd;

                    }
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;

                messageBody += "<font>Thanking You</font><br><br>";
                messageBody += "<font>For:" + dt1.Rows[0].ItemArray[14].ToString() + "</font><br>";
                return messageBody;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
        public ActionResult FinalClientBillReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> FinalClientBillReport(ReportModel model)
        {
            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.FinalClientBillReport(model.ClientID,model.BillReceiveFromDate, model.BillReceiveToDate, out dt));

           
            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptClientFinalBillPayment.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            if (string.IsNullOrEmpty(model.ClientName))
                reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            else
                reportDocument.SetParameterValue("ComDiv", model.ClientName);
            string rptHeaderName = "Client Wise Bill Receive.";
            reportDocument.SetParameterValue("rptName", rptHeaderName);
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));
            //string reportName = GetCompanyShortName(model.ClientName) + "-" + "ChequeOrEFTInfoVendorWise";
            string reportName = "Bill Receive Report"+' '+ model.ClientName;

            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }

            //reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);

            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }
        
        public ActionResult FinalClientDueBillReport()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [SILLogAttribute]
        public async Task<dynamic> FinalClientDueBillReport(ReportModel model)
        {
            DataTable dt = new DataTable();

            await Task.Run(() => _reportService.FinalClientDueBillReport(model.ClientID, model.BillReceiveFromDate, model.BillReceiveToDate, out dt));


            ReportDocument reportDocument = new ReportDocument();
            string ReportPath = Server.MapPath("~/Reports");
            ReportPath = ReportPath + "/rptClientFinalDueBillPayment.rpt";
            reportDocument.Load(ReportPath);
            reportDocument.SetDataSource(dt);
            reportDocument.Refresh();

            if (string.IsNullOrEmpty(model.ClientName))
                reportDocument.SetParameterValue("ComDiv", GetCompanyOrOwnerNameByUserID(UserID));
            else
                reportDocument.SetParameterValue("ComDiv", model.ClientName);
            string rptHeaderName = "Client Wise Bill Receive.";
            reportDocument.SetParameterValue("rptName", rptHeaderName);
            reportDocument.SetParameterValue("rptUser", GetUserName(UserID));
            //string reportName = GetCompanyShortName(model.ClientName) + "-" + "ChequeOrEFTInfoVendorWise";
            string reportName = "Bill Receive Report" + ' ' + model.ClientName;

            if (model.ButtonType == "Preview")
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);
            else
            {
                if (model.ReportType == "PDF")
                    reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportName);
                else if (model.ReportType == "EXCEL")
                    reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, System.Web.HttpContext.Current.Response, true, reportName);
                else
                    reportDocument.ExportToHttpResponse(ExportFormatType.EditableRTF, System.Web.HttpContext.Current.Response, true, reportName);
            }

            //reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, reportName);

            reportDocument.Close();
            reportDocument.Dispose();
            return View();
        }
    }
}