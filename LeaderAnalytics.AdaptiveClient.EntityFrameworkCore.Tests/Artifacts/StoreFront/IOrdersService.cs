using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront
{
    public interface IOrdersService
    {
        Task<Order> GetOrderByID(int id);
        Task SaveOrder(Order order);
        Task<Db> GetDbContext();
    }
}
