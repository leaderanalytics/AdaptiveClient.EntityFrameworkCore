namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MySQL;

class ProductsService: LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL.ProductsService
{
    public ProductsService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {
    }
}
