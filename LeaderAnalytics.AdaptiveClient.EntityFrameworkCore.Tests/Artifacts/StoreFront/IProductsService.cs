namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public interface IProductsService
{
    Task<Product> GetProductByID(int id);
    Task SaveProduct(Product product);
    Db GetDbContext();
}
