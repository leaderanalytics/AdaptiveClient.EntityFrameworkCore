using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests.Artifacts.BackOffice
{
    public class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
