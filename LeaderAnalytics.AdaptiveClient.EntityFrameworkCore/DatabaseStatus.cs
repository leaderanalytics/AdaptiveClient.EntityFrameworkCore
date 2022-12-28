namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

public enum DatabaseStatus
{
    Unknown,
    DoesNotExist,
    NotConsistentWithModel,
    ConsistentWithModel
}
