using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.MigrationFactories
{
    public class BackOffice_MSSQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.BackOffice.Db_MSSQL>
    {
        public Tests.Artifacts.BackOffice.Db_MSSQL CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=.\\SQLServer;Initial Catalog=AdaptiveClientEFTest_BackOffice;Integrated Security=True;MultipleActiveResultSets=True";
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
            string connectionString = ConnectionstringUtility.BuildConnectionString(@"Server=localhost;Database=AdaptiveClientEFTest_BackOffice;Uid={MySQL_UserName};Pwd={MySQL_Password};SslMode=none;");
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
            string connectionString = "Data Source=.\\SQLServer;Initial Catalog=AdaptiveClientEFTest_StoreFront;Integrated Security=True;MultipleActiveResultSets=True";
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
            string connectionString = ConnectionstringUtility.BuildConnectionString(@"Server=localhost;Database=AdaptiveClientEFTest_StoreFront;Uid={MySQL_UserName};Pwd={MySQL_Password};SslMode=none;");
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseMySQL(connectionString);
            Tests.Artifacts.StoreFront.Db_MySQL db = new Tests.Artifacts.StoreFront.Db_MySQL(dbOptions.Options);
            return db;
        }
    }

    public static class ConnectionstringUtility
    {
        public static string BuildConnectionString(string connectionString)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("C:\\Users\\sam\\AppData\\Roaming\\Blog\\appsettings.Development.json");
            IConfigurationRoot config = configBuilder.Build();
            connectionString = connectionString.Replace("{MySQL_UserName}", config["Data:MySQLUserName"]);
            connectionString = connectionString.Replace("{MySQL_Password}", config["Data:MySQLPassword"]);

            //comment above two lines and uncomment two lines below if you wish.... don't check in code that contains passwords.

            //connectionString = connectionString.Replace("{MySQL_UserName}", "yourUsername");
            //connectionString = connectionString.Replace("{MySQL_Password}", "yourPassword");
            
            return connectionString;
        }
    }
}
