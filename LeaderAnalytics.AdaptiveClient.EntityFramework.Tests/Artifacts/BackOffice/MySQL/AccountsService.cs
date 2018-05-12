using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MySQL
{
    public class AccountsService : LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL.AccountsService
    {
        public AccountsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        
    }
}
