namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public interface IAccountsService
{
    Task<Account> GetAccountByID(int id);
    Task<List<Account>> GetAccounts();
    Task<int> SaveAccount(Account account);
    Task<DateTime?> GetLastPaymentDate(int accountID);
}
