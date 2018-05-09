using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public class DatabaseValidationResult
    {
        public List<string> AppliedMigrations { get; set; }
        public bool DatabaseWasCreated { get; set; }

        public DatabaseValidationResult()
        {
            AppliedMigrations = new List<string>();
        }
    }
}
