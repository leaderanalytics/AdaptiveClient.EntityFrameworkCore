namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

public interface IDatabaseUtilities
{
    Task<DatabaseValidationResult> CreateOrUpdateDatabase(IEndPointConfiguration endPoint);
    Task<DatabaseStatus> GetDatabaseStatus(IEndPointConfiguration endPoint);
    Task<List<string>> ApplyMigrations(IEndPointConfiguration endPoint);
    Task DropDatabase(IEndPointConfiguration endPoint);
}
