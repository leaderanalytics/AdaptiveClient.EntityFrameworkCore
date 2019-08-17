using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;
using NUnit;
using NUnit.Framework;
using Autofac;
using Moq;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;


namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests
{
    [TestFixture("MSSQL")]
    [TestFixture("MySQL")]
    public class Tests : BaseTest
    {

        public Tests(string databaseProviderName) : base(databaseProviderName)
        {

        }

        [SetUp]
        public async Task Setup()
        {
            await CreateTestArtifacts();
        }

        [Test]
        public async Task DropAndRecreateAllDatabases()
        {
            IDatabaseUtilities databaseUtilities = Container.Resolve<IDatabaseUtilities>();
            foreach (IEndPointConfiguration ep in EndPoints)
            {
                await databaseUtilities.DropDatabase(ep);
                await databaseUtilities.ApplyMigrations(ep);
            }
        }

        [Test]
        public void Resolve_DbContextOptions_for_provider()
        {
            IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName);
            ResolutionHelper resolutionHelper = Container.Resolve<ResolutionHelper>();
            IDbContextOptions options = resolutionHelper.ResolveDbContextOptions(ep);
            Assert.IsNotNull(options);

            if(CurrentDatabaseProviderName == DataBaseProviderName.MSSQL)
                Assert.IsTrue(options is Artifacts.DbContextOptions_MSSQL);
            else if(CurrentDatabaseProviderName == DataBaseProviderName.MySQL)
                Assert.IsTrue(options is Artifacts.DbContextOptions_MySQL);
        }

        [Test]
        public void Resolve_DbContext_for_API()
        {
            IEndPointConfiguration storeFrontep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
            IEndPointConfiguration backOfficeep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
            ResolutionHelper resolutionHelper = Container.Resolve<ResolutionHelper>();
            DbContext storeFrontContext = resolutionHelper.ResolveDbContext(storeFrontep);
            DbContext backOfficeContext = resolutionHelper.ResolveDbContext(backOfficeep);
            Assert.IsNotNull(storeFrontContext);
            Assert.IsNotNull(backOfficeContext);
            Assert.IsTrue(storeFrontContext is Artifacts.StoreFront.Db);
            Assert.IsTrue(backOfficeContext is Artifacts.BackOffice.Db);

            if (CurrentDatabaseProviderName == DataBaseProviderName.MSSQL)
            {
                Assert.IsTrue(storeFrontContext.Database.GetDbConnection().ConnectionString == storeFrontep.ConnectionString);
                Assert.IsTrue(backOfficeContext.Database.GetDbConnection().ConnectionString == backOfficeep.ConnectionString);
            }
            else if (CurrentDatabaseProviderName == DataBaseProviderName.MySQL)
            {
                // MySql database connector changes the connection string.
                Assert.IsTrue(storeFrontContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("database=adaptiveclientef_storefront"));
                Assert.IsTrue(backOfficeContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("database=adaptiveclientef_backoffice"));
            }
        }

        [Test]
        public void Resolve_MigrationHelper_for_API()
        {
            IEndPointConfiguration storeFrontep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
            IEndPointConfiguration backOfficeep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
            ResolutionHelper resolutionHelper = Container.Resolve<ResolutionHelper>();
            DbContext storeFrontContext = resolutionHelper.ResolveMigrationContext(storeFrontep);
            DbContext backOfficeContext = resolutionHelper.ResolveMigrationContext(backOfficeep);
            Assert.IsNotNull(storeFrontContext);
            Assert.IsNotNull(backOfficeContext);

            if (CurrentDatabaseProviderName == DataBaseProviderName.MSSQL)
            {
                Assert.IsTrue(storeFrontContext is Artifacts.StoreFront.Db_MSSQL);
                Assert.IsTrue(backOfficeContext is Artifacts.BackOffice.Db_MSSQL);
            }
            else if (CurrentDatabaseProviderName == DataBaseProviderName.MySQL)
            {
                Assert.IsTrue(storeFrontContext is Artifacts.StoreFront.Db_MySQL);
                Assert.IsTrue(backOfficeContext is Artifacts.BackOffice.Db_MySQL);
            }
        }


        [Test]
        public void Resolve_DatabaseInitalizer_for_API()
        {
            IEndPointConfiguration storeFrontep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
            IEndPointConfiguration backOfficeep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
            ResolutionHelper resolutionHelper = Container.Resolve<ResolutionHelper>();

            IDatabaseInitializer storeFrontInitalizer = resolutionHelper.ResolveDatabaseInitializer(storeFrontep);
            IDatabaseInitializer backOfficeInitalizer = resolutionHelper.ResolveDatabaseInitializer(backOfficeep);
            Assert.IsTrue(storeFrontInitalizer is Artifacts.StoreFront.SFDatabaseInitializer);
            Assert.IsTrue(backOfficeInitalizer is Artifacts.BackOffice.BODatabaseInitializer);
        }


        [Test]
        public async Task AdaptiveClient_Resolves_to_MSSQL_on_Try()
        {
            Mock<IAccountsService> fakeMSSQLAccountsService = new Mock<IAccountsService>();
            fakeMSSQLAccountsService.Setup(x => x.GetAccountByID(It.IsAny<int>())).ReturnsAsync(new Account {Name = DataBaseProviderName.MSSQL });
            Mock<IAccountsService> fakeMySQLAccountsService = new Mock<IAccountsService>();
            fakeMySQLAccountsService.Setup(x => x.GetAccountByID(It.IsAny<int>())).ReturnsAsync(new Account { Name = DataBaseProviderName.MySQL });

            using (var scope = Container.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(fakeMSSQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.DBMS + DataBaseProviderName.MSSQL);
                builder.RegisterInstance(fakeMySQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.DBMS + DataBaseProviderName.MySQL);
            }))
            {
                IAdaptiveClient<IBOServiceManifest> client = scope.Resolve<IAdaptiveClient<IBOServiceManifest>>();
                Account account = await client.TryAsync(async x => await x.AccountsService.GetAccountByID(1));
                Assert.AreEqual(DataBaseProviderName.MSSQL, account.Name);
            }
        }


        [Test]
        public async Task Services_are_resolved_on_service_manifest()
        {
            // PaymentsService calls AccountsService internally.  
            // In this test we mock AccountService so we know it is resolved and accessible from within PaymentsService.
            
            IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
            await DropAndRecreate(ep);
            Mock<IAccountsService> fakeMSSQLAccountsService = new Mock<IAccountsService>();
            fakeMSSQLAccountsService.Setup(x => x.GetAccountByID(It.IsAny<int>())).ReturnsAsync(new Account { Name = "TEST" });

            using (var scope = Container.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(fakeMSSQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.DBMS + CurrentDatabaseProviderName);

            }))
            {
                IAdaptiveClient<IBOServiceManifest> client = scope.Resolve<IAdaptiveClient<IBOServiceManifest>>();
                Account account = await client.CallAsync(x => x.PaymentsService.GetAccountForPaymentID(1), ep.Name);
                Assert.AreEqual("TEST", account.Name);
            }
        }

        [Test]
        public async Task AdaptiveClient_falls_back_to_MySQL_on_Try()
        {
            Mock<IAccountsService> fakeMSSQLAccountsService = new Mock<IAccountsService>();
            fakeMSSQLAccountsService.Setup(x => x.GetAccountByID(It.Is<int>(i => i == 1))).ReturnsAsync(new Account { Name = DataBaseProviderName.MSSQL });
            fakeMSSQLAccountsService.Setup(x => x.GetAccountByID(It.Is<int>(i => i == 2))).Throws(new Exception("boo"));

            Mock<IAccountsService> fakeMySQLAccountsService = new Mock<IAccountsService>();
            fakeMySQLAccountsService.Setup(x => x.GetAccountByID(It.IsAny<int>())).ReturnsAsync(new Account { Name = DataBaseProviderName.MySQL });

            using (var scope = Container.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(fakeMSSQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.DBMS + DataBaseProviderName.MSSQL);
                builder.RegisterInstance(fakeMySQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.DBMS + DataBaseProviderName.MySQL);
            }))
            {
                IAdaptiveClient<IBOServiceManifest> client = scope.Resolve<IAdaptiveClient<IBOServiceManifest>>();
                Account account = await client.TryAsync(async x => await x.AccountsService.GetAccountByID(1));
                Assert.AreEqual(DataBaseProviderName.MSSQL, account.Name);

                // We are passing 2 as an ID so the call to the MSSQL instance will throw.  
                // We fall back to the MySQL instance which returns "MySQL"
                account = await client.TryAsync(async x => await x.AccountsService.GetAccountByID(2));
                Assert.AreEqual(DataBaseProviderName.MySQL, account.Name);
            }
        }

        [Test]
        public void Single_DbContext_instance_is_injected_into_all_services()
        {
            IAdaptiveClient<ISFServiceManifest> client = Container.Resolve<IAdaptiveClient<ISFServiceManifest>>();
            Assert.IsTrue(client.Call(x => x.OrdersService.AreDbContextsEqual()));
        }
    }
}
