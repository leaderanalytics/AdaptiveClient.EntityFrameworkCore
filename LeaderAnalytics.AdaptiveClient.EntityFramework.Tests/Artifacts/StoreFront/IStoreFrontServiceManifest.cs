using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront
{
    public interface IStoreFrontServiceManifest
    {
        IOrdersService OrdersService { get; }
        IProductsService ProductsService { get; }
    }
}
