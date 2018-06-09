using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice
{
    public class Db_MSSQL : Db, IMigrationContext
    {
        public Db_MSSQL(DbContextOptions options) : base(options)
        {
            // Nothing to see here.  This is just a stub so EntityFramework reflection finds the right DbContext to use to create a migration.
        }
    }
}
