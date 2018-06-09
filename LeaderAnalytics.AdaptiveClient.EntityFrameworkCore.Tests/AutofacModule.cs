using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
