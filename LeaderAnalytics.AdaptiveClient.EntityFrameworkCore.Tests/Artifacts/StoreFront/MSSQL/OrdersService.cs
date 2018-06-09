using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL
{
    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(Db db, IStoreFrontServiceManifest serviceManifest) : base(db, serviceManifest)
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

        public async Task<Db> GetDbContext()
        {
            ServiceManifest.ProductsService.GetDbContext();
            return this.db; 
        }
    }
}
