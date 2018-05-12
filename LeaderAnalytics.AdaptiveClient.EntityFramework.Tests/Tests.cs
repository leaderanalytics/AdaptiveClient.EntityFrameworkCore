using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using NUnit;
using NUnit.Framework;
using Autofac;
using Moq;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;


namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
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
                builder.RegisterInstance(fakeMSSQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.InProcess + DataBaseProviderName.MSSQL);
                builder.RegisterInstance(fakeMySQLAccountsService.Object).Keyed<IAccountsService>(EndPointType.InProcess + DataBaseProviderName.MySQL);
            }))
            {
                IAdaptiveClient<IBOServiceManifest> client = scope.Resolve<IAdaptiveClient<IBOServiceManifest>>();
                Account account = await client.TryAsync(async x => await x.AccountsService.GetAccountByID(1));
            }
        }
    }
}
