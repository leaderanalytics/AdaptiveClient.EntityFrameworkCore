using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.Utilities;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            IEnumerable<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints("EndPoints.json");

            if (endPoints.Any(x => x.ProviderName == DataBaseProviderName.MySQL))
            {
                endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
                endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            }


            registrationHelper

            // EndPoints   (Endpoints must be registered FIRST) 
            .RegisterEndPoints(endPoints)

            // -- EndPoint Validator
            .RegisterEndPointValidator<MSSQL_EndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MSSQL)
            .RegisterEndPointValidator<MySQL_EndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MySQL)

            // --- BackOffice Services ---
            // MSSQL
            .RegisterService<Artifacts.BackOffice.MSSQL.AccountsService, IAccountsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterService<Artifacts.BackOffice.MSSQL.PaymentsService, IPaymentsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            // MySQL
            .RegisterService<Artifacts.BackOffice.MySQL.AccountsService, IAccountsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)
            .RegisterService<Artifacts.BackOffice.MySQL.PaymentsService, IPaymentsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)

            // --- StoreFront Services ---
            // MSSQL
            .RegisterService<Artifacts.StoreFront.MSSQL.OrdersService, IOrdersService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterService<Artifacts.StoreFront.MSSQL.ProductsService, IProductsService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            // MySQL
            .RegisterService<Artifacts.StoreFront.MySQL.OrdersService, IOrdersService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MySQL)
            .RegisterService<Artifacts.StoreFront.MySQL.ProductsService, IProductsService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MySQL)

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


            // Database Initializers
            .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MySQL) // register same class for both providers.  If we had stored procs, etc. that were different we could just create a new class.
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MySQL) 


            // Service Manifests
            .RegisterServiceManifest<Artifacts.BackOffice.BOServiceManifest, IBOServiceManifest>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)
            .RegisterServiceManifest<Artifacts.BackOffice.BOServiceManifest, IBOServiceManifest>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)
            .RegisterServiceManifest<Artifacts.StoreFront.SFServiceManifest, ISFServiceManifest>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL);
        }
    }
}
