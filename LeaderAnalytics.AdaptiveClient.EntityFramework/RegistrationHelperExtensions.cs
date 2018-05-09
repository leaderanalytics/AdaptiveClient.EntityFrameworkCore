using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Autofac;
using LeaderAnalytics.AdaptiveClient;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public static class RegistrationHelperExtensions  
    {
        public static RegistrationHelper RegisterContext<TContext>(this RegistrationHelper helper) where TContext:DbContext
        {
            helper.Builder.RegisterType<TContext>();
            return helper;
        }

        public static RegistrationHelper RegisterMigrationContext<TContext>(this RegistrationHelper helper, string apiName, string providerName) where TContext : DbContext, IMigrationContext
        {
            helper.Builder.RegisterType<TContext>().Keyed<IMigrationContext>(apiName + providerName);
            return helper;
        }

        public static RegistrationHelper RegisterServiceManifest<TService, TInterface>(this RegistrationHelper helper, string endPointType, string api_name, string providerName) where TService : ServiceManifestFactory
        {
            helper.RegisterPerimeter(typeof(TInterface), api_name);
            helper.Builder.RegisterType<TService>().Keyed<TInterface>(endPointType + providerName).InstancePerLifetimeScope().PropertiesAutowired();
            helper.Builder.RegisterType<TService>().As<TInterface>().InstancePerLifetimeScope();
            return helper;
        }
    }
}
