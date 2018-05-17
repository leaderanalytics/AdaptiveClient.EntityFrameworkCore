using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DatabaseUtilities>().As<IDatabaseUtilities>();
            builder.RegisterType<ServiceManifestFactory>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.Register<Func<IDbContextOptions>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return () => new ResolutionHelper(cxt).ResolveDbContextOptions(); });
        }
    }
}
