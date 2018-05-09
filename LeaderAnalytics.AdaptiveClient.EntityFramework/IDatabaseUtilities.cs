using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public interface IDatabaseUtilities
    {
        Task<DatabaseValidationResult> VerifyDatabase(IEndPointConfiguration endPoint);
        Task<DatabaseStatus> GetDatabaseStatus(IEndPointConfiguration endPoint);
        Task<List<string>> ApplyMigrations(IEndPointConfiguration endPoint);
        Task DropDatabase(IEndPointConfiguration endPoint);
    }
}
