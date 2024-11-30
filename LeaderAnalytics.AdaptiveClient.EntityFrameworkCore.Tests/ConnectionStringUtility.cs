﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.Tests
{
    public static class ConnectionstringUtility
    {
        public static string GetConnectionString(string filePath, string apiName, string providerName)
        {
            IEnumerable<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints(filePath, false);
            return endPoints.First(x => x.API_Name == apiName && x.ProviderName == providerName).ConnectionString;
        }

        public static string BuildConnectionString(string connectionString)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("C:\\Users\\sam\\OneDrive\\LeaderAnalytics\\secrets.json");

            IConfigurationRoot config = configBuilder.Build();
            
            connectionString = connectionString.Replace("{MySQL_UserName}", config["MySQL_UserName"]);
            connectionString = connectionString.Replace("{MySQL_Password}", config["MySQL_Password"]);

            //comment above two lines and uncomment two lines below if you wish.... .

            //connectionString = connectionString.Replace("{MySQL_UserName}", "yourUsername");
            //connectionString = connectionString.Replace("{MySQL_Password}", "yourPassword");

            return connectionString;
        }
    }
}
