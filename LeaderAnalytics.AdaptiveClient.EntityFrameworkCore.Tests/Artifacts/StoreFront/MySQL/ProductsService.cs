using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MySQL
{
    class ProductsService: LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront.MSSQL.ProductsService
    {
        public ProductsService(Db db, IStoreFrontServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }
    }
}
