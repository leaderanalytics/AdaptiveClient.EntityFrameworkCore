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
        }
    }
}
