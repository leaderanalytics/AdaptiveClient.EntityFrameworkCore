using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MySQL
{
    public class PaymentsService : LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL.PaymentsService
    {
        public PaymentsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }
        
    }
}
