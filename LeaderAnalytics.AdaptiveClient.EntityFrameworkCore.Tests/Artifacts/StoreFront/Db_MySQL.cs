using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront
{
    public class Db_MySQL : Db, IMigrationContext
    {
        public Db_MySQL(DbContextOptions options) : base(options)
        {
        }
    }
}
