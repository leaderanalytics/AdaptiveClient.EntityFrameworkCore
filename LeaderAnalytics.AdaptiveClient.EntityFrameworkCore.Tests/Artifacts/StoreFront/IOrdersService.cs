namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public interface IOrdersService
{
    Task<Order> GetOrderByID(int id);
    Task SaveOrder(Order order);
    Db GetDbContext();
    bool AreDbContextsEqual();
}
