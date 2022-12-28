namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public class Db : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public Db(Func<IDbContextOptions> dbContextOptionsFactory) : base(dbContextOptionsFactory().Options)
    {

    }

    public Db(DbContextOptions options) : base(options)
    {
    }
}
