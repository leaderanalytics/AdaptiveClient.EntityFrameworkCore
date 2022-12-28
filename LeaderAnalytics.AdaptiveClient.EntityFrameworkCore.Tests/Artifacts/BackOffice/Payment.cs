namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice;

public class Payment
{
    public int ID { get; set; }
    public DateTime PaymentDate { get; set; }
    public int AccountID { get; set; }
    public decimal Amount { get; set; }
    public virtual Account Account { get; set; }
}
