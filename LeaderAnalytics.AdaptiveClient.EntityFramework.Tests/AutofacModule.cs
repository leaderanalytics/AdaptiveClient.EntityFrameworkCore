using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Artifacts.BackOffice.Db>();
            builder.RegisterType<Artifacts.StoreFront.Db>();
        }
    }
}
