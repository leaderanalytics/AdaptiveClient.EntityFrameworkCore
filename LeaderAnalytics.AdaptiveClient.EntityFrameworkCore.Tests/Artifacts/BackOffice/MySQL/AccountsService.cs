using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MySQL;

public class AccountsService : LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MSSQL.AccountsService
{
    public AccountsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {
    }
}
