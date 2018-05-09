using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using Autofac;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{

    public abstract class BaseTest
    {
        private IContainer Container { get; set; }
        

        public BaseTest()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);
            registrationHelper.RegisterModule(new AdaptiveClientModule());
            Container = builder.Build();
        }

        public async Task DropAndRecreate(IEndPointConfiguration ep)
        {

        }

        public async Task CreateTestArtifacts()
        {

        }
    }
}
