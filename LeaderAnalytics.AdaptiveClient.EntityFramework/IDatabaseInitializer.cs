using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public interface IDatabaseInitializer
    {
        Task Seed(string migrationName);
    }
}
