using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.MigrationFactories
{
    public class BackOffice_MSSQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.BackOffice.Db_MSSQL>
    {
        public Tests.Artifacts.BackOffice.Db_MSSQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.BackOffice, DataBaseProviderName.MSSQL);
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseSqlServer(connectionString);
            Tests.Artifacts.BackOffice.Db_MSSQL db = new Tests.Artifacts.BackOffice.Db_MSSQL(dbOptions.Options);
            return db;
        }
    }

    public class BackOffice_MySQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.BackOffice.Db_MySQL>
    {
        public Tests.Artifacts.BackOffice.Db_MySQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.BackOffice, DataBaseProviderName.MySQL));
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseMySQL(connectionString);
            Tests.Artifacts.BackOffice.Db_MySQL db = new Tests.Artifacts.BackOffice.Db_MySQL(dbOptions.Options);
            return db;
        }
    }

    public class StoreFront_MSSQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.StoreFront.Db_MSSQL>
    {
        public Tests.Artifacts.StoreFront.Db_MSSQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.StoreFront, DataBaseProviderName.MSSQL);
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseSqlServer(connectionString);
            Tests.Artifacts.StoreFront.Db_MSSQL db = new Tests.Artifacts.StoreFront.Db_MSSQL(dbOptions.Options);
            return db;
        }
    }

    public class StoreFront_MySQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.StoreFront.Db_MySQL>
    {
        public Tests.Artifacts.StoreFront.Db_MySQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.StoreFront, DataBaseProviderName.MySQL));
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseMySQL(connectionString);
            Tests.Artifacts.StoreFront.Db_MySQL db = new Tests.Artifacts.StoreFront.Db_MySQL(dbOptions.Options);
            return db;
        }
    }
}
