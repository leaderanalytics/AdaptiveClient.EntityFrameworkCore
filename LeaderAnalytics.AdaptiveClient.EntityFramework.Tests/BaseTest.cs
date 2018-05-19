using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using Autofac;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{

    public abstract class BaseTest
    {
        protected ContainerBuilder Builder { get; set; }
        protected IContainer Container { get; set; }
        protected IEnumerable<IEndPointConfiguration> EndPoints { get; set; }
        protected IAdaptiveClient<IBOServiceManifest> BOServiceClient;
        
        protected readonly string CurrentDatabaseProviderName;

        public BaseTest(string databaseProviderName)
        {
            CurrentDatabaseProviderName = databaseProviderName;  // passed in by NUnit based on TestFixture attribute
        }

        protected async Task DropAndRecreate(IEndPointConfiguration ep)
        {
            using (ILifetimeScope scope = Container.BeginLifetimeScope())
            {
                IDatabaseUtilities databaseUtilities = Container.Resolve<IDatabaseUtilities>();
                await databaseUtilities.DropDatabase(ep);
                await databaseUtilities.ApplyMigrations(ep);
            }
        }

        protected async Task CreateTestArtifacts()
        {
            EndPoints = EndPointUtilities.LoadEndPoints("EndPoints.json");
            EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            Builder = new ContainerBuilder();
            Builder.RegisterModule(new AutofacModule());
            Builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFramework.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(Builder);
            registrationHelper.RegisterModule(new AdaptiveClientModule());
            Container = Builder.Build();
            BOServiceClient = Container.Resolve<IAdaptiveClient<IBOServiceManifest>>();
        }
    }
}
