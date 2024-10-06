﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using SILDMS.Model.DocScanningModule;
using SILDMS.Model.SecurityModule;
using SILDMS.Service.DocProperty;
using SILDMS.Service.MultiDocScan;
using SILDMS.Service.OriginalDocSearching;
using SILDMS.Service.OwnerProperIdentity;
using SILDMS.Service.Server;
using SILDMS.Utillity;
using SILDMS.Web.UI.Areas.SecurityModule.Models;
using WebGrease.Css.Extensions;
using SILDMS.Web.UI.Areas.SecurityModule;

namespace SILDMS.Web.UI.Areas.DocScanningModule.Controllers
{
    public class OriginalDocSearchingController : Controller
    {
        private readonly IMultiDocScanService _multiDocScanService;
        private readonly IOwnerProperIdentityService _ownerProperIdentityService;
        private readonly IOriginalDocSearchingService _originalDocSearchingService;
        private readonly IServerService _serverService;
        private readonly IDocPropertyService _docPropertyService;
        private ValidationResult respStatus = new ValidationResult();
        private string outStatus = string.Empty;
        private readonly string UserID = string.Empty;
        public OriginalDocSearchingController(IOriginalDocSearchingService originalDocSearchingService,
            IMultiDocScanService multiDocScanService,
            IOwnerProperIdentityService ownerProperIdentityRepository,
            IDocPropertyService docPropertyService, IServerService serverService)
        {
            _originalDocSearchingService = originalDocSearchingService;
            _multiDocScanService = multiDocScanService;
            _ownerProperIdentityService = ownerProperIdentityRepository;
            _docPropertyService = docPropertyService;
            _serverService = serverService;
            UserID = SILAuthorization.GetUserID();
        }

        
        [Authorize]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public async Task<dynamic> GetDocPropIdentityForSelectedDocTypes(string _OwnerID, string _DocCategoryID,
             string _DocTypeID, string _DocPropertyID, string _SearchBy)
        {

            List<DocSearch> lstDocSearch = null;
            if (!string.IsNullOrEmpty(_OwnerID) && !string.IsNullOrEmpty(_DocCategoryID) && !string.IsNullOrEmpty(_DocTypeID) && !string.IsNullOrEmpty(_DocPropertyID) && !string.IsNullOrEmpty(_SearchBy))
            {
                await Task.Run(() => _originalDocSearchingService.GetOriginalDocBySearchParam(_OwnerID, _DocCategoryID, _DocTypeID, _DocPropertyID, _SearchBy, UserID, out lstDocSearch));
            }

            var result = (from r in lstDocSearch
                group r by new
                {
                    r.DocumentID,
                    r.DocDistributionID
                }
                into g

                select new
                {
                    DocumentID = g.Key.DocumentID,
                    FileCodeName = g.Select(o => o.FileCodeName).FirstOrDefault(),
                    MetaValue = String.Join(", ", g.Select(o => o.MetaValue)),
                    DocPropIdentifyID = String.Join(",", g.Select(o => o.DocPropIdentifyID).Distinct()),
                    DocPropIdentifyName = String.Join(",", g.Select(o => o.DocPropIdentifyName).Distinct()),
                    FileServerURL = g.Select(o => o.FileServerURL).FirstOrDefault(),
                    ServerIP = g.Select(o => o.ServerIP).FirstOrDefault(),
                    ServerPort = g.Select(o => o.ServerPort).FirstOrDefault(),
                    FtpUserName = g.Select(o => o.FtpUserName).FirstOrDefault(),
                    FtpPassword = g.Select(o => o.FtpPassword).FirstOrDefault(),
                    DocDistributionID = g.Select(o => o.DocDistributionID).FirstOrDefault(),

                    OwnerID = g.Select(o => o.OwnerID).FirstOrDefault(),
                    DocCategoryID = g.Select(o => o.DocCategoryID).FirstOrDefault(),
                    DocTypeID = g.Select(o => o.DocTypeID).FirstOrDefault(),
                    DocPropertyID = g.Select(o => o.DocPropertyID).FirstOrDefault(),
                    DocPropertyName = g.Select(o => o.DocPropertyName).FirstOrDefault()
                }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [SILLogAttribute]
        public async Task<dynamic> UpdateDocMetaInfo(DocumentsInfo _modelDocumentsInfo)
        {
            Session["UserID"].ToString();
            respStatus = await Task.Run(() => _originalDocSearchingService.UpdateDocMetaInfo(_modelDocumentsInfo, UserID, out outStatus));
            return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
        }

        [SILLogAttribute]
        public async Task<dynamic> DeleteDocument(string _DocumentID, string _DocumentType)
        {
            Session["UserID"].ToString();
            respStatus = await Task.Run(() => _originalDocSearchingService.DeleteDocument(_DocumentID, _DocumentType, UserID, out outStatus));
            return Json(new { Message = respStatus.Message, respStatus }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public async Task<dynamic> GetDocPropIdentityForSpecificDocType(string _OwnerID,
            string _DocCategoryID, string _DocTypeID, string _DocPropertyID, string _DocumentID,
            string _SearchBy, string _DocDistributionID)
        {
            List<DocSearch> lstDocSearch = null;
            if (!string.IsNullOrEmpty(_OwnerID) && !string.IsNullOrEmpty(_DocCategoryID)
                && !string.IsNullOrEmpty(_DocTypeID) && !string.IsNullOrEmpty(_DocPropertyID)
                && !string.IsNullOrEmpty(_SearchBy))
            {
                await Task.Run(() => _originalDocSearchingService.GetOriginalDocBySearchParam
                    (_OwnerID, _DocCategoryID, _DocTypeID, _DocPropertyID, _SearchBy, UserID,
                    out lstDocSearch));
            }
            var result = (from r in lstDocSearch
                where r.DocumentID == _DocumentID & r.DocDistributionID == _DocDistributionID
                select new
                {
                    DocMetaID = r.DocMetaID,
                    DocPropIdentifyName = r.DocPropIdentifyName,
                    MetaValue = r.MetaValue,
                    Status = r.Status
                }).ToList();
            

            return Json( result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        //[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public async Task<dynamic> GetDocumentsByWildSearch(string _SearchFor, int _Skip, int _Take)
        {

            List<DocSearch> lstDocSearch = null;
            if (!string.IsNullOrEmpty(_SearchFor))
            {
                await Task.Run(() => _originalDocSearchingService.GetDocumentsByWildSearch(_SearchFor, UserID, out lstDocSearch));

                var Totalresult = (from r in lstDocSearch
                              group r by new
                              {
                                  r.DocumentID,
                                  r.DocDistributionID
                              }
                                  into g
                                       orderby g.Select(o => o.DocPropertyID).FirstOrDefault()
                                  select new
                                  {
                                      DocumentID = g.Key.DocumentID,
                                      FileCodeName = g.Select(o => o.FileCodeName).FirstOrDefault(),
                                      MetaValue = String.Join(", ", g.Select(o => o.MetaValue)),
                                      DocPropIdentifyID = String.Join(",", g.Select(o => o.DocPropIdentifyID).Distinct()),
                                      DocPropIdentifyName = String.Join(",", g.Select(o => o.DocPropIdentifyName).Distinct()),
                                      FileServerURL = g.Select(o => o.FileServerURL).FirstOrDefault(),
                                      ServerIP = g.Select(o => o.ServerIP).FirstOrDefault(),
                                      ServerPort = g.Select(o => o.ServerPort).FirstOrDefault(),
                                      FtpUserName = g.Select(o => o.FtpUserName).FirstOrDefault(),
                                      FtpPassword = g.Select(o => o.FtpPassword).FirstOrDefault(),
                                      DocDistributionID = g.Select(o => o.DocDistributionID).FirstOrDefault(),

                                      OwnerLevelID = g.Select(o => o.OwnerLevelID).FirstOrDefault(),
                                      OwnerID = g.Select(o => o.OwnerID).FirstOrDefault(),
                                      DocCategoryID = g.Select(o => o.DocCategoryID).FirstOrDefault(),
                                      DocTypeID = g.Select(o => o.DocTypeID).FirstOrDefault(),
                                      DocPropertyID = g.Select(o => o.DocPropertyID).FirstOrDefault(),
                                      DocPropertyName = g.Select(o => o.DocPropertyName).FirstOrDefault()
                                  }).ToList();

                var count = ((Totalresult.Count())/_Take) + 1;

                var result = Totalresult.Skip(_Skip).Take(_Take).ToList();


                return Json(new {result, numberOfPages = count},
                    JsonRequestBehavior.AllowGet);
            }



            return Json(null, JsonRequestBehavior.AllowGet);
        }
	}

    
}