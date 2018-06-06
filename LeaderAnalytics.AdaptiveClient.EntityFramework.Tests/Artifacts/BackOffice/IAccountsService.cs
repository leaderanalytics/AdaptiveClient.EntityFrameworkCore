using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public interface IAccountsService
    {
        Task<Account> GetAccountByID(int id);
        Task<List<Account>> GetAccounts();
        Task<int> SaveAccount(Account account);
        Task<DateTime?> GetLastPaymentDate(int accountID);
    }
}
