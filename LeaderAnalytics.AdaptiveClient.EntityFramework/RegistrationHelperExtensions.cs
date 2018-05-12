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
        public static RegistrationHelper RegisterDbContext<TContext>(this RegistrationHelper helper, string apiName) where TContext:DbContext
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");

            helper.Builder.RegisterType<TContext>().InstancePerLifetimeScope(); // DbContext is normally injected into a service - no key needed
            helper.Builder.RegisterType<TContext>().Keyed<DbContext>(apiName).UsingConstructor(typeof(DbContextOptions)).InstancePerLifetimeScope(); // For when we have an endPoint and we need to know what DbContext is associated.
            return helper;
        }

        public static RegistrationHelper RegisterDbContextOptions<TOptions>(this RegistrationHelper helper, string providerName) where TOptions:IDbContextOptions
        {
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TOptions>().Keyed<IDbContextOptions>(providerName);
            return helper;
        }

        public static RegistrationHelper RegisterMigrationContext<TContext>(this RegistrationHelper helper, string apiName, string providerName) where TContext : DbContext, IMigrationContext
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TContext>().Keyed<IMigrationContext>(apiName + providerName);
            return helper;
        }

        public static RegistrationHelper RegisterDatabaseInitializer<TInitalizer>(this RegistrationHelper helper, string apiName, string providerName) where TInitalizer : IDatabaseInitializer
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TInitalizer>().Keyed<IDatabaseInitializer>(apiName + providerName);
            return helper;
        }

        public static RegistrationHelper RegisterServiceManifest<TService, TInterface>(this RegistrationHelper helper, string endPointType, string apiName, string providerName) where TService : ServiceManifestFactory
        {
            if (string.IsNullOrEmpty(endPointType))
                throw new ArgumentNullException("endPointType");
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.RegisterPerimeter(typeof(TInterface), apiName);

            helper.Builder.Register<Func<string, string, TInterface>>(c => {
                IComponentContext cxt = c.Resolve<IComponentContext>();
                return (ept, pn) => new ResolutionHelper(cxt).ResolveClient<TInterface>(ept, pn);
            }).PropertiesAutowired();

            helper.Builder.RegisterType<TService>().Keyed<TInterface>(endPointType + providerName).PropertiesAutowired().InstancePerLifetimeScope();
            helper.Builder.RegisterType<TService>().As<TInterface>().PropertiesAutowired().InstancePerLifetimeScope();
            return helper;
        }
    }
}
