namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}
