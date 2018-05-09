using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFramework;


namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public class Db : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public Db(ContextResolver resolver) : base(resolver.ResolveDbContextOptions().Options)
        {

        }

        public Db(DbContextOptions options) : base(options)
        {
        }

        private void Initialize()
        {

            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Database.SetCommandTimeout(360);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
