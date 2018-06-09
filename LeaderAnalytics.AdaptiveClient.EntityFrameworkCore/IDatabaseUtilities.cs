using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore
{
    public interface IDatabaseUtilities
    {
        Task<DatabaseValidationResult> CreateOrUpdateDatabase(IEndPointConfiguration endPoint);
        Task<DatabaseStatus> GetDatabaseStatus(IEndPointConfiguration endPoint);
        Task<List<string>> ApplyMigrations(IEndPointConfiguration endPoint);
        Task DropDatabase(IEndPointConfiguration endPoint);
    }
}
