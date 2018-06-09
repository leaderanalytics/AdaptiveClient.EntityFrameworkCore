using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.StoreFront
{
    public class Order
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
        public Product Product { get; set; }
    }
}
