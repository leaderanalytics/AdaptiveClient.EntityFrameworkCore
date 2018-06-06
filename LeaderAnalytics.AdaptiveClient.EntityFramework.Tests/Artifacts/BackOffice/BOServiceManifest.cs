using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient.EntityFramework;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public class BOServiceManifest : ServiceManifestFactory, IBOServiceManifest
    {
        public IAccountsService AccountsService { get => Create<IAccountsService>(); }
        public IPaymentsService PaymentsService { get => Create<IPaymentsService>(); }
    }
}
