using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public class Payment
    {
        int ID { get; set; }
        DateTime PaymentDate { get; set; }
        public int ClientID { get; set; }
        public decimal Amount { get; set; }
        public virtual Account Account { get; set; }
    }
}
