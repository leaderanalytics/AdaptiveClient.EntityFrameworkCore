using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL
{
    public class AccountsService : BaseService, IAccountssService
    {
        public AccountsService(Db db, IBackOficeServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        public virtual async Task<Account> GetClientByID(int id)
        {
            return await db.Accounts.FirstOrDefaultAsync(x => x.ID == id);
        }

        public virtual async Task SaveClient(Account client)
        {
            db.Entry(client).State = client.ID == 0 ? EntityState.Added : EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
