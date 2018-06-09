using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice
{
    public interface IBOServiceManifest : IDisposable
    {
        IPaymentsService PaymentsService { get; }
        IAccountsService AccountsService { get; }
    }
}
