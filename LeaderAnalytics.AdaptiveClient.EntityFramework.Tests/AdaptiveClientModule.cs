using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MSSQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice.MySQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MSSQL;
using LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront.MySQL;


namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            // EndPoints
            IEnumerable<IEndPointConfiguration> endpoints = EndPointUtilities.LoadEndPoints("EndPoints.json");
            registrationHelper.RegisterEndPoints(endpoints);

            // --- BackOffice ---
            // MSSQL
            registrationHelper.Register<IAccountssService, Artifacts.BackOffice.MSSQL.AccountsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MSSQL);
            registrationHelper.Register<IPaymentsService, Artifacts.BackOffice.MSSQL.PaymentsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MSSQL);
            // MySQL
            registrationHelper.Register<IAccountssService, Artifacts.BackOffice.MySQL.AccountsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MySQL);
            registrationHelper.Register<IPaymentsService, Artifacts.BackOffice.MySQL.PaymentsService>(EndPointType.InProcess, API_Name.BackOffice, DataBaseProviderName.MySQL);


            // --- StoreFront ---
            // MSSQL
            registrationHelper.Register<IOrdersService, Artifacts.StoreFront.MSSQL.OrdersService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MSSQL);
            registrationHelper.Register<IProductsService, Artifacts.StoreFront.MSSQL.ProductsService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MSSQL);
            // MySQL
            registrationHelper.Register<IOrdersService, Artifacts.StoreFront.MySQL.OrdersService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MySQL);
            registrationHelper.Register<IProductsService, Artifacts.StoreFront.MySQL.ProductsService>(EndPointType.InProcess, API_Name.StoreFront, DataBaseProviderName.MySQL);
        }
    }
}
