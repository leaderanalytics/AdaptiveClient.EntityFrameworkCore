using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DatabaseUtilities>().As<IDatabaseUtilities>();
            builder.RegisterType<ServiceManifestFactory>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.Register<Func<IDbContextOptions>>(c => { ILifetimeScope cxt = c.Resolve<ILifetimeScope>(); return () => new ResolutionHelper(cxt).ResolveDbContextOptions(); });
        }
    }
}
