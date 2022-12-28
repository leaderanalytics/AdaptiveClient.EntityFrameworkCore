namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MySQL;

public class PaymentsService : LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MSSQL.PaymentsService
{
    public PaymentsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {
    }
    
}
