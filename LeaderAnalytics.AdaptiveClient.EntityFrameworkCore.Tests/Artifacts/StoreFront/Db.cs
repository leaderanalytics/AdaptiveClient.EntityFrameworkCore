﻿namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public class Db : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public Db(Func<IDbContextOptions> dbContextOptionsFactory) : base(dbContextOptionsFactory().Options)
    {

    }

    public Db(DbContextOptions options) : base(options)
    {
    }
}
