namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public class SFServiceManifest : ServiceManifestFactory, ISFServiceManifest
{
    public IProductsService ProductsService { get => Create<IProductsService>(); }
    public IOrdersService OrdersService { get => Create<IOrdersService>(); }
}
