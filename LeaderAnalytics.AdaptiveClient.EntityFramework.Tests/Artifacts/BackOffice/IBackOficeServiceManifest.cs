using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public interface IBackOficeServiceManifest
    {
        IPaymentsService PaymentsService { get; set; }
        IAccountssService AccountsService { get; set; }
    }
}
