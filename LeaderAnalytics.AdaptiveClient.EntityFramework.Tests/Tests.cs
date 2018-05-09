using System;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{
    [TestFixture("MSSQL")]
    [TestFixture("MySQL")]
    public class Tests
    {
        [Test]
        public async Task Test1()
        {
            await Task.Delay(1);
        }
    }
}
