using Ninject;
using SILDMS.DataAccess.Departments;
using SILDMS.DataAccess.Menu;
using SILDMS.DataAccess.Users;
using SILDMS.DataAccessInterface.Departments;
using SILDMS.DataAccessInterface.Menu;
using SILDMS.DataAccessInterface.Users;
using SILDMS.Service;
using SILDMS.Service.Departments;
using SILDMS.Service.Menu;
using SILDMS.Service.Users;
using SILDMS.Utillity.Localization;
using SILDMS.DataAccess;
using SILDMS.DataAccess.DashboardDataService;
using SILDMS.DataAccess.DataLevelPermission;
using SILDMS.DataAccess.DefaultValueSetup;
using SILDMS.DataAccess.DocDestroy;
using SILDMS.DataAccess.DocumentCategory;
using SILDMS.DataAccess.DocumentType;
using SILDMS.DataAccess.MultiDocScan;
using SILDMS.DataAccess.Owner;
using SILDMS.DataAccess.OwnerProperIdentity;
using SILDMS.DataAccess.Roles;
using SILDMS.DataAccessInterface.OwnerProperIdentity;
using SILDMS.DataAccessInterface.Roles;
using SILDMS.Service.Roles;
using SILDMS.Service.OwnerProperIdentity;
using SILDMS.Service.OwnerLevel;
using SILDMS.DataAccessInterface.OwnerLevel;
using SILDMS.DataAccess.OwnerLevel;
using SILDMS.DataAccessInterface.DocumentCategory;
using SILDMS.DataAccessInterface.DocumentType;
using SILDMS.DataAccessInterface.Owner;
using SILDMS.Service.DocumentCategory;
using SILDMS.Service.DocumentType;
using SILDMS.Service.Owner;
using SILDMS.Service.DocProperty;
using SILDMS.DataAccessInterface.OwnerProperty;
using SILDMS.DataAccess.OwnerProperty;
using SILDMS.DataAccess.RoleSetup;
using SILDMS.DataAccessInterface.MultiDocScan;
using SILDMS.DataAccessInterface.RoleSetup;
using SILDMS.Service.MultiDocScan;
using SILDMS.Service.RoleSetup;
using SILDMS.Service.NavMenuOptSetup;
using SILDMS.DataAccessInterface.NavMenuOptSetup;
using SILDMS.DataAccess.NavMenuOptSetup;
using SILDMS.DataAccess.OriginalDocSearching;
using SILDMS.DataAccess.OwnerLevelPermission;
using SILDMS.DataAccess.RoleMenuPermission;
using SILDMS.DataAccess.Server;
using SILDMS.DataAccess.UserAccessLog;
using SILDMS.DataAccess.UserLevel;
using SILDMS.DataAccessInterface.DataLevelPermission;
using SILDMS.DataAccessInterface.OriginalDocSearching;
using SILDMS.DataAccessInterface.OwnerLevelPermission;
using SILDMS.DataAccessInterface.RoleMenuPermission;
using SILDMS.DataAccessInterface.Server;
using SILDMS.DataAccessInterface.UserAccessLog;
using SILDMS.DataAccessInterface.UserLevel;
using SILDMS.Service.DataLevelPermission;
using SILDMS.Service.OriginalDocSearching;
using SILDMS.Service.OwnerLevelPermission;
using SILDMS.Service.RoleMenuPermission;
using SILDMS.Service.Server;
using SILDMS.Service.UserAccessLog;
using SILDMS.Service.UserLevel;
using SILDMS.Service.VersionDocSearching;
using SILDMS.DataAccess.VersionDocSearching;
using SILDMS.DataAccess.VersioningOfOriginalDoc;
using SILDMS.DataAccess.VersioningVersionedDoc;
using SILDMS.DataAccessInterface;
using SILDMS.DataAccessInterface.DashboardDataService;
using SILDMS.DataAccessInterface.VersionDocSearching;
using SILDMS.DataAccessInterface.VersioningOfOriginalDoc;
using SILDMS.DataAccessInterface.VersioningVersionedDoc;
using SILDMS.Service.Dashboard;
using SILDMS.Service.DocDestroyPolicy;
using SILDMS.Service.VersioningOfOriginalDoc;
using SILDMS.Service.VersioningVersionedDoc;
using SILDMS.DataAccessInterface.DefaultValueSetup;
using SILDMS.DataAccessInterface.DocDestroy;
using SILDMS.Service.DefaultValueSetup;
using SILDMS.Service.DocDestroy;
using SILDMS.Service.AutoValueConf;
using SILDMS.DataAccessInterface.AutoValueConf;
using SILDMS.DataAccess.AutoValueConf;
using SILDMS.DataAccess.AutoValueSetup;
using SILDMS.DataAccessInterface.AutoValueSetup;
using SILDMS.Service.AutoValueSetup;
using SILDMS.DataAccessInterface.Reports;
using SILDMS.DataAccess.Reports;
using SILDMS.Service.Reports;
using SILDMS.Service.MassDocPrint;
using SILDMS.Service.DashboardV2;
using SILDMS.DataAccessInterface.DashboardV2;
using SILDMS.DataAccess.DashboardV2;
using SILDMS.Service.UserDoctypeMap;
using SILDMS.DataAccessInterface.UserDoctypeMap;
using SILDMS.DataAccess.UserDoctypeMap;
using SILDMS.Service.DataMigration;
using SILDMS.DataAccess.DataMigration;
using SILDMS.DataAccessInterface.DataMigration;
using SILDMS.Service.MasterData;
using SILDMS.DataAccessInterface.MasterDataSetup;
using SILDMS.Service.TaxCodeEntry;
using SILDMS.Service.ResendFailedSmsEmail;
using SILDMS.DataAccessInterface.ResendFailedSmsEmail;
using SILDMS.DataAccess.ResendFailedSmsEmail;
using SILDMS.Service.EmailMessage;
using SILDMS.Service.MessageFormatSetup;
using SILDMS.DataAccessInterface.MessageFormatDataSetup;
using SILDMS.DataAccess.MessageFormatDataSetup;
using SILDMS.Service.EmailNotification;
using SILDMS.Service.TestData;
using SILDMS.DataAccessInterface.TestData;
using SILDMS.DataAccess.TestData;
using SILDMS.Service.PoCreation;
using SILDMS.DataAccess.POCreation;
using SILDMS.Service.PoRecom;
using SILDMS.DataAccess.PoRecom;
using SILDMS.Service.PoAprv;
using SILDMS.DataAccess.PoAprv;
using SILDMS.DataAccess.QuotationToClientService;
using SILDMS.Service.QuotationToClient;
using SILDMS.Service.QuotationRecommendation;
using SILDMS.DataAccess.QuotationRecommendation;
using SILDMS.Service.QuotationApproval;
using SILDMS.DataAccess.QuotationApproval;
using SILDMS.Service.AdvDemandVendor;
using SILDMS.DataAccess.AdvDemandVendor;
using SILDMS.Service.AdvanceRecommendation;
using SILDMS.Service.AdvanceApproval;
using SILDMS.DataAccess.AdvanceApproval;
using SILDMS.DataAccess.AdvanceRecommendation;
using SILDMS.Service.VendorFinalBillReceived;
using SILDMS.DataAccess.VendorFinalBillReceived;
using SILDMS.Service.RcmdFinalBillRcvd;
using SILDMS.DataAccess.RcmdFinalBillRcvd;
using SILDMS.Service.AprvFinalBillRcvd;
using SILDMS.DataAccess.AprvFinalBillRcvd;
using SILDMS.Service.AdvanceClaim;
using SILDMS.DataAccess.AdvanceClaim;
using SILDMS.Service.ClientFinalBillPrepare;
using SILDMS.DataAccess.ClientFinalBillPrepare;
using SILDMS.Service.ClientBillRcmd;
using SILDMS.DataAccess.ClientBillRcmd;
using SILDMS.Service.ClientAprvBill;
using SILDMS.DataAccess.ClientAprvBill;
using SILDMS.Service.AdvanceClaimRecomClient;
using SILDMS.DataAccess.AdvanceClaimRecomClient;
using SILDMS.Service.AdvanceClaimAprvClient;
using SILDMS.DataAccess.AdvanceClaimAprvClient;
using SILDMS.Service.ClaimReceived;
using SILDMS.DataAccess.Claim_Received;

namespace SILDMS.InfraStructure
{
    public class ServiceRegistration
    {
        internal void Load(IKernel kernel)
        {
            kernel.Bind<ILocalizationService>().To<LocalizationService>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserDataService>().To<UserDataService>();

            kernel.Bind<IMenuService>().To<MenuService>();
            kernel.Bind<IMenuDataService>().To<MenuDataService>();

            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IDepartmentDataService>().To<DepartmentDataService>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleDataService>().To<RoleDataService>();

            kernel.Bind<IOwnerProperIdentityService>().To<OwnerProperIdentityService>();
            kernel.Bind<IOwnerProperIdentityDataService>().To<OwnerProperIdentityDataService>();

            kernel.Bind<IOwnerLevelService>().To<OwnerLevelService>();
            kernel.Bind<IOwnerLevelDataService>().To<OwnerLevelDataService>();

            kernel.Bind<IOwnerService>().To<OwnerService>();
            kernel.Bind<IOwnerDataService>().To<OwnerDataService>();

            kernel.Bind<IDocCategoryService>().To<DocCategoryService>();
            kernel.Bind<IDocCategoryDataService>().To<DocCategoryDataService>();

            kernel.Bind<IDocTypeService>().To<DocTypeService>();
            kernel.Bind<IDocTypeDataService>().To<DocTypeDataService>();

            kernel.Bind<IDocPropertyService>().To<DocPropertyService>();
            kernel.Bind<IDocPropertyDataService>().To<DocPropertyDataService>();

            kernel.Bind<IMultiDocScanService>().To<MultiDocScanService>();
            kernel.Bind<IMultiDocScanDataService>().To<MultiDocScanDataService>();

            kernel.Bind<IRoleSetupService>().To<RoleSetupService>();
            kernel.Bind<IRoleSetupDataService>().To<RoleSetupDataService>();

            kernel.Bind<INavMenuOptSetupService>().To<NavMenuOptSetupService>();
            kernel.Bind<INavMenuOptSetupDataService>().To<NavMenuOptSetupDataService>();

            kernel.Bind<IRoleMenuPermissionService>().To<RoleMenuPermissionService>();
            kernel.Bind<IRoleMenuPermissionDataService>().To<RoleMenuPermissionDataService>();

            kernel.Bind<IOwnerLevelPermissionService>().To<OwnerLevelPermissionService>();
            kernel.Bind<IOwnerLevelPermissionDataService>().To<OwnerLevelPermissionDataService>();

            kernel.Bind<IDataLevelPermissionService>().To<DataLevelPermissionService>();
            kernel.Bind<IDataLevelPermissionDataService>().To<DataLevelPermissionDataService>();

            kernel.Bind<IUserLevelService>().To<UserLevelService>();
            kernel.Bind<IUserLevelDataService>().To<UserLevelDataService>();

            kernel.Bind<IUserAccessLogService>().To<UserAccessLogService>();
            kernel.Bind<IUserAccessLogDataService>().To<UserAccessLogDataService>();

            kernel.Bind<IServerService>().To<ServerService>();
            kernel.Bind<IServerDataService>().To<ServerDataService>();

            kernel.Bind<IOriginalDocSearchingService>().To<OriginalDocSearchingService>();
            kernel.Bind<IOriginalDocSearchingDataService>().To<OriginalDocSearchingDataService>();

            kernel.Bind<IVersionDocSearchingDataService>().To<VersionDocSearchingDataService>();
            kernel.Bind<IVersionDocSearchingService>().To<VersionDocSearchingService>();

            kernel.Bind<IVersioningOfOriginalDocService>().To<VersioningOfOriginalDocService>();
            kernel.Bind<IVersioningOfOriginalDocDataService>().To<VersioningOfOriginalDocDataService>();

            kernel.Bind<IVersioningVersionedDocDataService>().To<VersioningVersionedDocDataService>();
            kernel.Bind<IVersioningVersionedDocService>().To<VersioningVersionedDocService>();

            kernel.Bind<IDashboardDataService>().To<DashboardDataService>();
            kernel.Bind<IDashboardService>().To<DashboardService>();

            kernel.Bind<IDocDestroyPolicyService>().To<DocDestroyPolicyService>();
            kernel.Bind<IDocDestroyPolicyDataService>().To<DocDestroyPolicyDataService>();

            kernel.Bind<IDocDestroyService>().To<DocDestroyService>();
            kernel.Bind<IDocDestroyDataService>().To<DocDestroyDataService>();

            kernel.Bind<IDefaultValueSetupDataService>().To<DefaultValueSetupDataService>();
            kernel.Bind<IDefalutValueSetupService>().To<DefaultValueSetupService>();

            kernel.Bind<IAutoValueConfService>().To<AutoValueConfService>();
            kernel.Bind<IAutoValueConfDataService>().To<AutoValueConfDataService>();

            kernel.Bind<IAutoValueSetupService>().To<AutoValueSetupService>();
            kernel.Bind<IAutoValueSetupDataService>().To<AutoValueSetupDataService>();
            kernel.Bind<IReportsDataService>().To<ReportsDataService>();
            kernel.Bind<IReportsService>().To<ReportsService>();

            kernel.Bind<IDashboardV2Service>().To<DashboardV2Service>();
            kernel.Bind<IDashboardV2DataService>().To<DashboardV2DataService>();

            kernel.Bind<IUserDoctypeMapService>().To<UserDoctypeMapService>();
            kernel.Bind<IUserDoctypeMapDataService>().To<UserDoctypeMapDataService>();

            kernel.Bind<IDataMigrationService>().To<DataMigrationService>();
            kernel.Bind<IDataMigrationDataService>().To<DataMigrationDataService>();

            kernel.Bind<IResendFailedSmsEmailService>().To<ResendFailedSmsEmailService>();
            kernel.Bind<IResendFailedSmsEmailDataService>().To<ResendFailedSmsEmailDataService>();

            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<ITestData>().To<TestData>();

            kernel.Bind<IServicesCategoryService>().To<ServicesCategoryService>();
            kernel.Bind<IServicesCategoryDataService>().To<ServicesCategoryDataService>();

            kernel.Bind<IServiceItemInfoService>().To<ServiceItemInfoService>();
            kernel.Bind<IServiceItemInfoDataService>().To<ServiceItemInfoDataService>();

            kernel.Bind<IClientInfoService>().To<ClientInfoService>();
            kernel.Bind<IClientInfoDataService>().To<ClientInfoDataService>();

            kernel.Bind<IVendorInfoService>().To<VendorInfoService>();
            kernel.Bind<IVendorInfoDataService>().To<VendorInfoDataService>();

            kernel.Bind<ITermsService>().To<TermsService>();
            kernel.Bind<ITermsDataService>().To<TermsDataService>();

            kernel.Bind<IClientRequisitionService>().To<ClientRequisitionService>();
            kernel.Bind<IClientRequisitionDataService>().To<ClientRequisitionDataService>();

            kernel.Bind<IVendorRequisitionService>().To<VendorRequisitionService>();
            kernel.Bind<IVendorRequisitionDataService>().To<VendorRequisitionDataService>();

            kernel.Bind<IVendorQuotationService>().To<VendorQuotationService>();
            kernel.Bind<IVendorQuotationDataService>().To<VendorQuotationDataService>();

            kernel.Bind<IVendorCSInfoService>().To<VendorCSInfoService>();
            kernel.Bind<IVendorCSInfoDataService>().To<VendorCSInfoDataService>();

            kernel.Bind<IVendorCSAprvService>().To<VendorCSAprvService>();
            kernel.Bind<IVendorCSAprvDataService>().To<VendorCSAprvDataService>();


            kernel.Bind<IPOCreationService>().To<POCreationService>();
            kernel.Bind<IPOCreationData>().To<POCreationData>();

            kernel.Bind<IPoRecomService>().To<PoRecomService>();

            kernel.Bind<IPoRecomData>().To<PoRecomData>();

            kernel.Bind<IPoAprvService>().To<PoAprvService>();
            kernel.Bind<IPoAprvData>().To<PoAprvData>();



            kernel.Bind<IQuotationToClientService>().To<QuotationToClientService>();
            kernel.Bind<IQuotationToClientDataService>().To<QuotationToClientDataService>();

            kernel.Bind<IQuotationRecommendationService>().To<QuotationRecommendationService>();
            kernel.Bind<IQuotationRecommendationData>().To<QuotationRecommendationData>();

            kernel.Bind<IQuotationApprovalService>().To<QuotationApprovalService>();
            kernel.Bind<IQuotationApprovalDataService>().To<QuotationApprovalDataService>();

            kernel.Bind<IAdvDemandVendorService>().To<AdvDemandVendorService>();
            kernel.Bind<IAdvDemandVendorData>().To<AdvDemandVendorData>();

            kernel.Bind<IAdvanceRecommendationService>().To<AdvanceRecommendationService>();
            kernel.Bind<IAdvanceRecommendationData>().To<AdvanceRecommendationData>();

            kernel.Bind<IAdvanceApprovalService>().To<AdvanceApprovalService>();
            kernel.Bind<IAdvanceApprovalData>().To<AdvanceApprovalData>();
            kernel.Bind<IVendorFinalBillReceivedService>().To<VendorFinalBillReceivedService>();
            kernel.Bind<IVendorFinalBillReceivedData>().To<VendorFinalBillReceivedData>();

            kernel.Bind<IRcmdFinalBillRcvdService>().To<RcmdFinalBillRcvdService>();
            kernel.Bind<IRcmdFinalBillRcvdData>().To<RcmdFinalBillRcvdData>();

            kernel.Bind<IAprvFinalBillRcvdService>().To<AprvFinalBillRcvdService>();
            kernel.Bind<IAprvFinalBillRcvdData>().To<AprvFinalBillRcvdData>();

            kernel.Bind<IAdvanceClaimService>().To<AdvanceClaimService>();
            kernel.Bind<IAdvanceClaimData>().To<AdvanceClaimData>();

            kernel.Bind<IAdvanceClaimRecomClientService>().To<AdvanceClaimRecomClientService>();
            kernel.Bind<IAdvanceClaimRecomClientData>().To<AdvanceClaimRecomClientData>();

            kernel.Bind<IAdvanceClaimAprvClientService>().To<AdvanceClaimAprvClientService>();
            kernel.Bind<IAdvanceClaimAprvClientData>().To<AdvanceClaimAprvClientData>();


            kernel.Bind<IClaimReceivedService>().To<ClaimReceivedService>();
            kernel.Bind<IClaimReceivedDataService>().To<ClaimReceivedDataService>();


            kernel.Bind<IQuotationToClientRevisedService>().To<QuotationToClientRevisedService>();
            kernel.Bind<IQuotationToClientRevisedDataService>().To<QuotationToClientRevisedDataService>();

            kernel.Bind<IWorkOrderInfoService>().To<WorkOrderInfoService>();
            kernel.Bind<IWorkOrderInfoDataService>().To<WorkOrderInfoDataService>();
            
            kernel.Bind<IClientFinalBillPrepareService>().To<ClientFinalBillPrepareService>();
            kernel.Bind<IClientFinalBillPrepareData>().To<ClientFinalBillPrepareData>();

            kernel.Bind<IClientBillRcmdService>().To<ClientBillRcmdService>();
            kernel.Bind<IClientBillRcmdData>().To<ClientBillRcmdData>();

            kernel.Bind<IClientAprvBillService>().To<ClientAprvBillService>();
            kernel.Bind<IClientAprvBillData>().To<ClientAprvBillData>();

      

        }
    }
}