using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MySQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MSSQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MySQL;


namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            IEnumerable<IEndPointConfiguration> endpoints = EndPointUtilities.LoadEndPoints("EndPoints.json");

            registrationHelper

            // EndPoints   (Endpoints must be registered FIRST) 
            .RegisterEndPoints(endpoints)

            // -- EndPoint Validator
            .RegisterEndPointValidator<AdaptiveClient.InProcessEndPointValidator>(EndPointType.InProcess, DataBaseProviderName.MSSQL)
            .RegisterEndPointValidator<AdaptiveClient.InProcessEndPointValidator>(EndPointType.InProcess, DataBaseProviderName.MySQL)

            // --- BackOffice Services ---
            // MSSQL
            .RegisterService<Artifacts.BackOffice.MSSQL.AccountsService, IAccountsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterService<Artifacts.BackOffice.MSSQL.PaymentsService, IPaymentsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            // MySQL
            .RegisterService<Artifacts.BackOffice.MySQL.AccountsService, IAccountsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MySQL)
            .RegisterService<Artifacts.BackOffice.MySQL.PaymentsService, IPaymentsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MySQL)

            // --- StoreFront Services ---
            // MSSQL
            .RegisterService<Artifacts.StoreFront.MSSQL.OrdersService, IOrdersService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterService<Artifacts.StoreFront.MSSQL.ProductsService, IProductsService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            // MySQL
            .RegisterService<Artifacts.StoreFront.MySQL.OrdersService, IOrdersService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MySQL)
            .RegisterService<Artifacts.StoreFront.MySQL.ProductsService, IProductsService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MySQL)

            // DbContexts
            .RegisterDbContext<Artifacts.BackOffice.Db>(API_Name.BackOffice)
            .RegisterDbContext<Artifacts.StoreFront.Db>(API_Name.StoreFront)

            // DbContextOptions
            .RegisterDbContextOptions<DbContextOptions_MSSQL>(DataBaseProviderName.MSSQL)
            .RegisterDbContextOptions<DbContextOptions_MySQL>(DataBaseProviderName.MySQL)

            // Migration Contexts
            .RegisterMigrationContext<Artifacts.BackOffice.Db_MSSQL>(API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterMigrationContext<Artifacts.BackOffice.Db_MySQL>(API_Name.BackOffice, DataBaseProviderName.MySQL)
            .RegisterMigrationContext<Artifacts.StoreFront.Db_MSSQL>(API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterMigrationContext<Artifacts.StoreFront.Db_MySQL>(API_Name.StoreFront, DataBaseProviderName.MySQL)


            // Database Initalizers
            .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MySQL) // register same class for both providers.  If we had stored procs, etc. that were different we could just create a new class.
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MySQL) 


            // Service Manifests
            .RegisterServiceManifest<Artifacts.BackOffice.BOServiceManifest, IBOServiceManifest>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterServiceManifest<Artifacts.BackOffice.BOServiceManifest, IBOServiceManifest>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MySQL);
        }
    }
}
