namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MySQL;

public class OrdersService : LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL.OrdersService
{
    public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {
    }

    
}
