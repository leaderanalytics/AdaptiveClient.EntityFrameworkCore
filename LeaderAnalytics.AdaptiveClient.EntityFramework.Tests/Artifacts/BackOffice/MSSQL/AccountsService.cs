using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL
{
    public class AccountsService : BaseService, IAccountsService
    {
        public AccountsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        public virtual async Task<Account> GetAccountByID(int id)
        {
            return await db.Accounts.FirstOrDefaultAsync(x => x.ID == id);
        }

        public virtual async Task<List<Account>> GetAccounts()
        {
            return await db.Accounts.ToListAsync();
        }

        public virtual async Task<int> SaveAccount(Account account)
        {
            db.Entry(account).State = account.ID == 0 ? EntityState.Added : EntityState.Modified;
            await db.SaveChangesAsync();
            return account.ID;
        }
    }
}
