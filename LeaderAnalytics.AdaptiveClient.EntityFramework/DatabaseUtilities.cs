using System;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Migrations;

using LeaderAnalytics.AdaptiveClient;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public class DatabaseUtilities : IDatabaseUtilities
    {
        private ResolutionHelper resolver;

        public DatabaseUtilities(ResolutionHelper resolver)
        {
            this.resolver = resolver;
        }

        public virtual async Task<DatabaseValidationResult> VerifyDatabase(IEndPointConfiguration endPoint) 
        {
            DatabaseValidationResult result = new DatabaseValidationResult();
            DatabaseStatus status = await GetDatabaseStatus(endPoint);
            result.DatabaseWasCreated = status == DatabaseStatus.DoesNotExist;

            if (status != DatabaseStatus.ConsistentWithModel)
                result.AppliedMigrations = await ApplyMigrations(endPoint);

            return result;
        }

        public virtual async Task<DatabaseStatus> GetDatabaseStatus(IEndPointConfiguration endPoint)
        {
            DbContext context = resolver.ResolveMigrationContext(endPoint);

            if (!await ((context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).ExistsAsync()))
                return DatabaseStatus.DoesNotExist;

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                return DatabaseStatus.NotConsistentWithModel;

            return DatabaseStatus.ConsistentWithModel;
        }

        public virtual async Task<List<string>> ApplyMigrations(IEndPointConfiguration endPoint) 
        {
            DbContext context = resolver.ResolveMigrationContext(endPoint);
            IDataInitializer dataInitializer = resolver.ResolveDataInitializer(endPoint);
            
            List<string> migrations = (await context.Database.GetPendingMigrationsAsync()).ToList();
            IMigrator migrator = context.Database.GetService<IMigrator>();

            foreach (string migrationName in migrations)
            {
                await migrator.MigrateAsync(migrationName);

                if (dataInitializer != null)
                    await dataInitializer.Seed(migrationName);
            }
            return migrations;
        }

        public virtual async Task DropDatabase(IEndPointConfiguration endPoint) 
        {
            DbContext context = resolver.ResolveMigrationContext(endPoint);
            await context.Database.EnsureDeletedAsync();
        }
    }
}
