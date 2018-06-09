using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore
{
    public enum DatabaseStatus
    {
        Unknown,
        DoesNotExist,
        NotConsistentWithModel,
        ConsistentWithModel
    }
}
