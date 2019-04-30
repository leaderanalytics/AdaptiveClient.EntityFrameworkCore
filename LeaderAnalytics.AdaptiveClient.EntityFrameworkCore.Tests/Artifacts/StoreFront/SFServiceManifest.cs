using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront
{
    public class SFServiceManifest : ServiceManifestFactory, ISFServiceManifest
    {
        public IOrdersService OrdersService { get => Create<IOrdersService>(); }
        public IProductsService ProductsService { get => Create<IProductsService>(); }
    }
}
