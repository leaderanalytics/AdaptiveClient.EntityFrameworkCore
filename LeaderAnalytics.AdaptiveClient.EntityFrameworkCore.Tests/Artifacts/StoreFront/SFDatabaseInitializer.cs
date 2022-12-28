namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public class SFDatabaseInitializer : IDatabaseInitializer
{
    private Db db;


    public SFDatabaseInitializer(Db db)
    {
        this.db = db;
    }

    public async Task Seed(string migrationName)
    {
        Product product = new Product { Name = "Some Product" };
        Order order = new Order { AccountID = 1, Amount = 100, Product = product };

        db.Products.Add(product);
        db.Orders.Add(order);

        await db.SaveChangesAsync();
    }
}
