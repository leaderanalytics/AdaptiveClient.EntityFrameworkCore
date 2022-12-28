namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

public class DatabaseValidationResult
{
    public List<string> AppliedMigrations { get; set; }
    public bool DatabaseWasCreated { get; set; }

    public DatabaseValidationResult()
    {
        AppliedMigrations = new List<string>();
    }
}
