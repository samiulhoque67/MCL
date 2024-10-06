﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model.DocScanningModule
{
    public class DocSearch
    {
        public string DocMetaID { get; set; }
        public string DocDistributionID { get; set; }
        public string DocumentID { get; set; }
        public string DocPropIdentifyID { get; set; }
        public string DocPropIdentifyName { get; set; }
        public string MetaValue { get; set; }
        public string DocPropertyID { get; set; }
        public string FileServerURL { get; set; }
        public string VersionMetaValue { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string OriginalReference { get; set; }
        public string ReferenceVersionID { get; set; }
        public string DocMetaIDVersion { get; set; }
        public string DocVersionID { get; set; }
        public string VersionNo { get; set; }
        public string FileCodeName { get; set; }

        public string OwnerLevelID { get; set; }
        public string OwnerID { get; set; }
        public string DocCategoryID { get; set; }
        public string DocTypeID { get; set; }
        public string LevelName{ get; set; }
        public string OwnerName{ get; set; }
        public string DocCategoryName{ get; set; }
        public string DocTypeName{ get; set; }
        public string DocPropertyName{ get; set; }
        public string IdentificationAttribute{ get; set; }
        public string IsRequired{ get; set; }
        public int SerialNo { get; set; }

        public string Status { get; set; }
    }
}
