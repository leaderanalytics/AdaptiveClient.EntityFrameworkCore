using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MySQL
{
    class ProductsService: LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MSSQL.ProductsService
    {
        public ProductsService(Db db, IStoreFrontServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }
    }
}
