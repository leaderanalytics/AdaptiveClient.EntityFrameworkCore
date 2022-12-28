namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public class Db_MSSQL : Db, IMigrationContext
{
    public Db_MSSQL(DbContextOptions options) : base(options)
    {
    }
}
