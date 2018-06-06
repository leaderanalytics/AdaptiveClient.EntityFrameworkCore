using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public interface IBOServiceManifest : IDisposable
    {
        IPaymentsService PaymentsService { get; }
        IAccountsService AccountsService { get; }
    }
}
