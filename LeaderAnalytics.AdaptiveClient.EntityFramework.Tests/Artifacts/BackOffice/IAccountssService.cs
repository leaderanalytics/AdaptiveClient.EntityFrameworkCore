using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public interface IAccountssService
    {
        Task<Account> GetClientByID(int id);
        Task SaveClient(Account client);
    }
}
