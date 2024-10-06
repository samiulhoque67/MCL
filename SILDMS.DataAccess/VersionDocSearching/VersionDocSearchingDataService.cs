﻿using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILDMS.DataAccessInterface.VersionDocSearching;
using SILDMS.Model.DocScanningModule;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;


namespace SILDMS.DataAccess.VersionDocSearching
{
    public class VersionDocSearchingDataService : IVersionDocSearchingDataService
    {
        private readonly string spStatusParam = "@p_Status";

        public List<DocSearch> GetVersionDocBySearchParam(string _OwnerID, string _DocCategoryID, 
            string _DocTypeID, string _DocPropertyID, string _SearchBy, string _UserID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<DocSearch> userList = new List<DocSearch>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetVersionDocBySearchParam"))
            {
                db.AddInParameter(dbCommandWrapper, "@OwnerID", SqlDbType.NVarChar, _OwnerID);
                db.AddInParameter(dbCommandWrapper, "@DocCategoryID", SqlDbType.NVarChar, _DocCategoryID);
                db.AddInParameter(dbCommandWrapper, "@DocTypeID", SqlDbType.NVarChar, _DocTypeID);
                db.AddInParameter(dbCommandWrapper, "@DocPropertyID", SqlDbType.NVarChar, _DocPropertyID);
                db.AddInParameter(dbCommandWrapper, "@SearchBy", SqlDbType.NVarChar, _SearchBy);
                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.NVarChar, _UserID);
                     db.AddOutParameter(dbCommandWrapper, "@p_Status", DbType.Int32, 10);
                
                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        userList = dt1.AsEnumerable().Select(reader => new DocSearch
                        {
                            DocMetaIDVersion = reader.GetString("DocMetaIDVersion"),
                            DocVersionID = reader.GetString("DocVersionID"),
                            DocMetaID = reader.GetString("DocMetaID"),
                            DocumentID = reader.GetString("DocumentID"),
                            ReferenceVersionID = reader.GetString("ReferenceVersionID"),
                            DocPropIdentifyID = reader.GetString("DocPropIdentifyID"),
                            DocPropIdentifyName = reader.GetString("DocPropIdentifyName"),
                            MetaValue = reader.GetString("MetaValue"),
                            OriginalReference = reader.GetString("OriginalReference"),
                            FileServerURL = reader.GetString("FileServerURL"),
                            ServerIP = reader.GetString("ServerIP"),
                            ServerPort = reader.GetString("ServerPort"),
                            FtpUserName = reader.GetString("FtpUserName"),
                            FtpPassword = reader.GetString("FtpPassword"),
                            VersionNo = reader.GetString("VersionNo"),
                            DocDistributionID = reader.GetString("DocDistributionID"),
                            OwnerID = reader.GetString("OwnerID"),
                            DocCategoryID = reader.GetString("DocCategoryID"),
                            DocTypeID = reader.GetString("DocTypeID"),
                            DocPropertyID = reader.GetString("DocPropertyID"),
                            DocPropertyName = reader.GetString("DocPropertyName")
                        }).ToList();
                    }
                }
            }
            return userList;
        }

        public List<DocSearch> GetDocumentsByWildSearch(string _SearchFor, string _UserID, out string errorNumber)
        {
            errorNumber = string.Empty;
            List<DocSearch> userList = new List<DocSearch>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase db = factory.CreateDefault() as SqlDatabase;
            using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("GetVersionDocByWildSearch"))
            {
                db.AddInParameter(dbCommandWrapper, "@SearchFor", SqlDbType.NVarChar, _SearchFor);
                db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.NVarChar, _UserID);
                db.AddOutParameter(dbCommandWrapper,"@p_Status", DbType.Int32, 10);

                DataSet ds = db.ExecuteDataSet(dbCommandWrapper);

                if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                {
                    errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        userList = dt1.AsEnumerable().Select(reader => new DocSearch
                        {
                            DocMetaIDVersion = reader.GetString("DocMetaIDVersion"),
                            DocVersionID = reader.GetString("DocVersionID"),
                            DocMetaID = reader.GetString("DocMetaID"),
                            DocumentID = reader.GetString("DocumentID"),
                            ReferenceVersionID = reader.GetString("ReferenceVersionID"),
                            DocPropIdentifyID = reader.GetString("DocPropIdentifyID"),
                            DocPropIdentifyName = reader.GetString("DocPropIdentifyName"),
                            MetaValue = reader.GetString("MetaValue"),
                            OriginalReference = reader.GetString("OriginalReference"),
                            //DocPropertyID = reader.GetString("DocPropertyID"),
                            //DocPropertyName = reader.GetString("DocPropertyName"),
                            FileServerURL = reader.GetString("FileServerURL"),
                            ServerIP = reader.GetString("ServerIP"),
                            ServerPort = reader.GetString("ServerPort"),
                            FtpUserName = reader.GetString("FtpUserName"),
                            FtpPassword = reader.GetString("FtpPassword"),
                            VersionNo = reader.GetString("VersionNo"),
                            DocDistributionID = reader.GetString("DocDistributionID"),
                            OwnerID = reader.GetString("OwnerID"),
                            DocCategoryID = reader.GetString("DocCategoryID"),
                            DocTypeID = reader.GetString("DocTypeID"),
                            DocPropertyID = reader.GetString("DocPropertyID"),
                            DocPropertyName = reader.GetString("DocPropertyName")
                        }).ToList();
                    }
                }
            }
            return userList;
        }

        public string UpdateDocMetaInfo(DocumentsInfo _modelDocumentsInfo, string userId, out string errorNumber)
        {
            errorNumber = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                SqlDatabase db = factory.CreateDefault() as SqlDatabase;

                DataTable docMetaDataTable = new DataTable();
                docMetaDataTable.Columns.Add("DocMetaID");
                docMetaDataTable.Columns.Add("MetaValue");


                foreach (var item in _modelDocumentsInfo.DocMetaValues)
                {
                    DataRow objDataRow = docMetaDataTable.NewRow();
                    objDataRow[0] = item.DocMetaID;
                    objDataRow[1] = item.MetaValue;

                    docMetaDataTable.Rows.Add(objDataRow);
                }



                using (DbCommand dbCommandWrapper = db.GetStoredProcCommand("UpdateDocMetaVersionInfo"))
                {
                    db.AddInParameter(dbCommandWrapper, "@Doc_Meta", SqlDbType.Structured, docMetaDataTable);
                    db.AddInParameter(dbCommandWrapper, "@UserID", SqlDbType.NVarChar, userId);
                    db.AddOutParameter(dbCommandWrapper, spStatusParam, SqlDbType.VarChar, 10);
                    db.ExecuteNonQuery(dbCommandWrapper);

                    if (!db.GetParameterValue(dbCommandWrapper, spStatusParam).IsNullOrZero())
                    {
                        errorNumber = db.GetParameterValue(dbCommandWrapper, spStatusParam).PrefixErrorCode();
                    }
                }
            }
            catch (Exception ex)
            {
                errorNumber = "E404";
            }
            return errorNumber;
        }
    }
}
