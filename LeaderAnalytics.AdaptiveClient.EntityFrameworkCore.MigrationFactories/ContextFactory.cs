using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.MigrationFactories;
public static class Consts
{
    public const string endPointsFile = "bin\\debug\\netcoreapp2.0\\EndPoints.json";
}

public class BackOffice_MSSQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.BackOffice.Db_MSSQL>
{
    public Tests.Artifacts.BackOffice.Db_MSSQL CreateDbContext(string[] args)
    {
        string connectionString = EndPointUtilities.LoadEndPoints(Consts.endPointsFile, false).First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MSSQL).ConnectionString;
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
        string connectionString = EndPointUtilities.LoadEndPoints(Consts.endPointsFile, false).First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString;
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseMySql(ServerVersion.AutoDetect(connectionString));
        Tests.Artifacts.BackOffice.Db_MySQL db = new Tests.Artifacts.BackOffice.Db_MySQL(dbOptions.Options);
        return db;
    }
}

public class StoreFront_MSSQLContextFactory : IDesignTimeDbContextFactory<Tests.Artifacts.StoreFront.Db_MSSQL>
{
    public Tests.Artifacts.StoreFront.Db_MSSQL CreateDbContext(string[] args)
    {
        string connectionString = EndPointUtilities.LoadEndPoints(Consts.endPointsFile, false).First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MSSQL).ConnectionString;
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
        string connectionString = EndPointUtilities.LoadEndPoints(Consts.endPointsFile, false).First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString;
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseMySql(ServerVersion.AutoDetect(connectionString));
        Tests.Artifacts.StoreFront.Db_MySQL db = new Tests.Artifacts.StoreFront.Db_MySQL(dbOptions.Options);
        return db;
    }
}
