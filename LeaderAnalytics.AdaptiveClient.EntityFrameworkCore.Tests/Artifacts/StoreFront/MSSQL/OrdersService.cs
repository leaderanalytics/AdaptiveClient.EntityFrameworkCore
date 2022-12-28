namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL;

public class OrdersService : BaseService, IOrdersService
{
    public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {
    }

    public Task<Order> GetOrderByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Db GetDbContext()
    { 
        return this.db; 
    }

    public bool AreDbContextsEqual()
    {
        return Object.ReferenceEquals(this.db, ServiceManifest.ProductsService.GetDbContext());
    }
}
