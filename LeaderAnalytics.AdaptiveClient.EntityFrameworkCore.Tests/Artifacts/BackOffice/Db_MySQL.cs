namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public class Db_MySQL : Db, IMigrationContext
{
    public Db_MySQL(DbContextOptions options) : base(options)
    {
    }
}
