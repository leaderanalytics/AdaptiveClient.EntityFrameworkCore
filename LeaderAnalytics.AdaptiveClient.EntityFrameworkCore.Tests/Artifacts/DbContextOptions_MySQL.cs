using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts
{
    public class DbContextOptions_MySQL : IDbContextOptions
    {
        public DbContextOptions Options { get; set; }

        public DbContextOptions_MySQL(string connectionString)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            //https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/1246
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); 
            Options = builder.Options;
        }
    }
}
