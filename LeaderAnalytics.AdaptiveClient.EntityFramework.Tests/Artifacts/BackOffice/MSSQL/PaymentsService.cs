using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL
{
    public class PaymentsService : BaseService, IPaymentsService
    {
        public PaymentsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {
        }

        public async Task ApplyPayment(Payment payment)
        {
            db.Entry(payment).State = EntityState.Added;
            await db.SaveChangesAsync();
        }

        public async Task<Account> GetAccountForPaymentID(int paymentID)
        {
            Payment payment = await GetPaymentByID(paymentID);

            if (payment == null)
                return null;

            return await ServiceManifest.AccountsService.GetAccountByID(payment.AccountID);
        }

        public async Task<List<Payment>> GetPaymentsForAccount(int accountID)
        {
            return await db.Payments.Where(x => x.AccountID == accountID).ToListAsync();
        }

        public async Task<Payment> GetPaymentByID(int paymentID)
        {
            return await db.Payments.FirstOrDefaultAsync(x => x.ID == paymentID);
        }

        public async Task ReversePayment(Payment payment)
        {
            payment.Amount = payment.Amount * -1;
            await ApplyPayment(payment);
        }
    }
}
