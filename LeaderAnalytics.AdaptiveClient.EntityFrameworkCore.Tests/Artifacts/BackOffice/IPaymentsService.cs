
namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public interface IPaymentsService
{
    Task ApplyPayment(Payment payment);
    Task ReversePayment(Payment payment);
    Task<Payment> GetPaymentByID(int paymentID);
    Task<Account> GetAccountForPaymentID(int paymentID);
    Task<List<Payment>> GetPaymentsForAccount(int accountID);
}
