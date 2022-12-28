namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public interface IBOServiceManifest : IDisposable
{
    IPaymentsService PaymentsService { get; }
    IAccountsService AccountsService { get; }
}
