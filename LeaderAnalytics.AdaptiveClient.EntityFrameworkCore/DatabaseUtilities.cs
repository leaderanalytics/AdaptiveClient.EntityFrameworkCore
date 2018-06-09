using System;
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

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore
{
    public class DatabaseUtilities : IDatabaseUtilities
    {
        private ResolutionHelper resolver;

        public DatabaseUtilities(ResolutionHelper resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        /// Creates a database if it does not exist and/or applies pending migrations, if any.
        /// </summary>
        /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
        /// <returns>DatabaseValidationResult</returns>
        public virtual async Task<DatabaseValidationResult> CreateOrUpdateDatabase(IEndPointConfiguration endPoint) 
        {
            DatabaseValidationResult result = new DatabaseValidationResult();
            DatabaseStatus status = await GetDatabaseStatus(endPoint);
            result.DatabaseWasCreated = status == DatabaseStatus.DoesNotExist;

            if (status != DatabaseStatus.ConsistentWithModel)
                result.AppliedMigrations = await ApplyMigrations(endPoint);

            return result;
        }

        /// <summary>
        /// Checks if a database exists or if there are pending migrations.
        /// </summary>
        /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
        /// <returns>DatabaseStatus</returns>
        public virtual async Task<DatabaseStatus> GetDatabaseStatus(IEndPointConfiguration endPoint)
        {
            DbContext context = resolver.ResolveMigrationContext(endPoint);

            if (!await ((context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).ExistsAsync()))
                return DatabaseStatus.DoesNotExist;

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                return DatabaseStatus.NotConsistentWithModel;

            return DatabaseStatus.ConsistentWithModel;
        }

        /// <summary>
        /// Applies pending migrations, if any.
        /// </summary>
        /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
        /// <returns>A list of names of migrations that were applied.</returns>
        public virtual async Task<List<string>> ApplyMigrations(IEndPointConfiguration endPoint) 
        {
            DbContext context = resolver.ResolveMigrationContext(endPoint);
            IDatabaseInitializer dataInitializer = resolver.ResolveDatabaseInitializer(endPoint);
            
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

        /// <summary>
        /// Drops database.
        /// </summary>
        /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
        /// <returns></returns>
        public virtual async Task DropDatabase(IEndPointConfiguration endPoint) 
        {
            DbContext context = resolver.ResolveDbContext(endPoint);
            await context.Database.EnsureDeletedAsync();
        }
    }
}
