using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL
{
    public class PaymentsService : BaseService, IPaymentsService
    {
        public PaymentsService(Db db, IBackOficeServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        public async Task ApplyPayment(Payment payment)
        {
            db.Entry(payment).State = EntityState.Added;
            await db.SaveChangesAsync();
        }

        public async Task ReversePayment(Payment payment)
        {
            payment.Amount = payment.Amount * -1;
            await ApplyPayment(payment);
        }
    }
}
