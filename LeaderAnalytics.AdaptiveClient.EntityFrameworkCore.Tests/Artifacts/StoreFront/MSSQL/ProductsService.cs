using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL
{
    class ProductsService: BaseService, IProductsService
    {
        public ProductsService(Db db, IStoreFrontServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        public Task<Product> GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Db> GetDbContext()
        {
            return this.db;
        }
    }
}
