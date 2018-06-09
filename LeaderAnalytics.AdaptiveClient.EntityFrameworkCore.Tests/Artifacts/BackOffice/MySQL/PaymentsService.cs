using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MySQL
{
    public class PaymentsService : LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice.MSSQL.PaymentsService
    {
        public PaymentsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }
        
    }
}
