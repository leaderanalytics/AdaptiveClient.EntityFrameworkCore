using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront
{
    public interface IProductsService
    {
        Task<Product> GetProductByID(int id);
        Task SaveProduct(Product product);
        Db GetDbContext();
    }
}
