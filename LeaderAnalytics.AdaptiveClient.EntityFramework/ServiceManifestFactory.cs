using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using LeaderAnalytics.AdaptiveClient;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public class ServiceManifestFactory
    {
        public Func<IEndPointConfiguration> EndPointFactory { get; set; }
        public ResolutionHelper ResolutionHelper { get; set; }

        public TService Create<TService>()
        {
            IEndPointConfiguration ep = EndPointFactory();
            return ResolutionHelper.ResolveClient<TService>(ep.EndPointType, ep.ProviderName);
        }
    }
}
