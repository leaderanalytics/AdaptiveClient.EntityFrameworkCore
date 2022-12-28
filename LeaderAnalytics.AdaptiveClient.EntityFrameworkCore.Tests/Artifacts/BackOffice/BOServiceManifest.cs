namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public class BOServiceManifest : ServiceManifestFactory, IBOServiceManifest
{
    public IAccountsService AccountsService { get => Create<IAccountsService>(); }
    public IPaymentsService PaymentsService { get => Create<IPaymentsService>(); }
}
