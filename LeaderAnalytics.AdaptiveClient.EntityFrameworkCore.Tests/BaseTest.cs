namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests;

public abstract class BaseTest
{
    protected ContainerBuilder Builder { get; set; }
    protected IContainer Container { get; set; }
    protected IEnumerable<IEndPointConfiguration> EndPoints { get; set; }
    protected IAdaptiveClient<IBOServiceManifest> BOServiceClient;
    protected readonly string CurrentDatabaseProviderName;
    private const string secretsFile = "c:\\users\\sam\\onedrive\\LeaderAnalytics\\secrets.json";

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
        EndPoints = EndPointUtilities.LoadEndPoints("appsettings.json");
        SecretsManager secretsManager = new SecretsManager(secretsFile);

        if (EndPoints.Any(x => x.ProviderName == DataBaseProviderName.MySQL))
        {
            EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = secretsManager.PopulateConnectionStrings(EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = secretsManager.PopulateConnectionStrings(EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
        }
        Builder = new ContainerBuilder();
        Builder.RegisterModule(new AutofacModule());
        Builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
        RegistrationHelper registrationHelper = new RegistrationHelper(Builder);
        registrationHelper.RegisterModule(new AdaptiveClientModule());
        Container = Builder.Build();
        BOServiceClient = Container.Resolve<IAdaptiveClient<IBOServiceManifest>>();
    }
}
