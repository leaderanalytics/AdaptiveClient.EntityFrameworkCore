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
        private bool disposed;

        protected TService Create<TService>()
        {
            IEndPointConfiguration ep = EndPointFactory();
            return ResolutionHelper.ResolveClient<TService>(ep.EndPointType, ep.ProviderName);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                }
            }
        }
    }
}
