namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts;

public class DbContextOptions_MySQL : IDbContextOptions
{
    public DbContextOptions Options { get; set; }

    public DbContextOptions_MySQL(string connectionString)
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        //https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/1246
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")); 
        Options = builder.Options;
    }
}
