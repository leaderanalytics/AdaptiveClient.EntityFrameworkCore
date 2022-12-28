namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

public interface IDatabaseInitializer
{
    Task Seed(string migrationName);
}
