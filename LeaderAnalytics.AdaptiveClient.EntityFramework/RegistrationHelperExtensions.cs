using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Autofac;
using LeaderAnalytics.AdaptiveClient.EntityFramework;

namespace LeaderAnalytics.AdaptiveClient
{
    public static class RegistrationHelperExtensions  
    {
        /// <summary>
        /// Registers a class that derives from DbContext using the supplied apiName as a key.   
        /// </summary>
        /// <typeparam name="TContext">Type that derives from DbContext.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="apiName">An API_Name to use as a key.  Must match the API_Name of one or more IEndPointConfiguration objects. 
        /// API_Name is an arbitrary name given to the collection of services exposed by an API.  
        /// The name of a database or a domain name are examples of names that might also be used as an API_Name.</param>
        /// <returns>RegistrationHelper</returns>
        public static RegistrationHelper RegisterDbContext<TContext>(this RegistrationHelper helper, string apiName) where TContext:DbContext
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");

            helper.Builder.RegisterType<TContext>().InstancePerLifetimeScope(); // DbContext is normally injected into a service - no key needed
            helper.Builder.RegisterType<TContext>().Keyed<DbContext>(apiName).UsingConstructor(typeof(DbContextOptions)).InstancePerLifetimeScope(); // For when we have an endPoint and we need to know what DbContext is associated.
            return helper;
        }

        /// <summary>
        /// Registers an implementation of IDbContextOptions keyed to the supplied providerName. 
        /// </summary>
        /// <typeparam name="TOptions">Type that implements IDbContextOptions.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="providerName">ProviderName typically represents some implementation of technology such as a DBMS platform.
        /// Examples might be: MSSQL, MySQL, SQLite, etc.</param>
        /// <returns>RegistrationHelper</returns>
        public static RegistrationHelper RegisterDbContextOptions<TOptions>(this RegistrationHelper helper, string providerName) where TOptions:IDbContextOptions
        {
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TOptions>().Keyed<IDbContextOptions>(providerName);
            return helper;
        }

        /// <summary>
        /// Registers an implementation of IMigrationContext.  A MigrationContext is a placeholder type that allows AdaptiveClient to associate an API_Name 
        /// and ProviderName with specific implementations of DbContext and DbContextOptions.
        /// </summary>
        /// <typeparam name="TContext">Type that derives from DbContext and implements IMigratinContext.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="apiName">An API_Name to use as a key.  Must match the API_Name of one or more IEndPointConfiguration objects. 
        /// API_Name is an arbitrary name given to the collection of services exposed by an API.  
        /// The name of a database or a domain name are examples of names that might also be used as an API_Name. </param>
        /// <param name="providerName">ProviderName typically represents some implementation of technology such as a DBMS platform.
        /// Examples might be: MSSQL, MySQL, SQLite, etc.</param>
        /// <returns>RegistrationHelper</returns>
        public static RegistrationHelper RegisterMigrationContext<TContext>(this RegistrationHelper helper, string apiName, string providerName) where TContext : DbContext, IMigrationContext
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TContext>().Keyed<IMigrationContext>(apiName + providerName);
            return helper;
        }

        /// <summary>
        /// Registers an implementation of IDatabaseinitializer, an object that is used to initialize or seed a database after it is created or migrations are applied.
        /// </summary>
        /// <typeparam name="TInitalizer">Type that implements IDatabaseInitializer.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="apiName">An API_Name to use as a key.  Must match the API_Name of one or more IEndPointConfiguration objects. 
        /// API_Name is an arbitrary name given to the collection of services exposed by an API.  
        /// The name of a database or a domain name are examples of names that might also be used as an API_Name. </param>
        /// <param name="providerName">ProviderName typically represents some implementation of technology such as a DBMS platform.
        /// Examples might be: MSSQL, MySQL, SQLite, etc.</param>
        /// <returns>RegistrationHelper</returns>
        public static RegistrationHelper RegisterDatabaseInitializer<TInitalizer>(this RegistrationHelper helper, string apiName, string providerName) where TInitalizer : IDatabaseInitializer
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException("providerName");

            helper.Builder.RegisterType<TInitalizer>().Keyed<IDatabaseInitializer>(apiName + providerName);
            return helper;
        }

        /// <summary>
        /// Registers a ServiceManifest, a class that exposes a property for each type of service associated with an API.  A ServiceManifest is keyed to an EndPointType, an API_Name, and a ProviderName.
        /// </summary>
        /// <typeparam name="TService">Type of service manifest. Must derive from ServiceManifestFactory.</typeparam>
        /// <typeparam name="TInterface">Interface that describes TService.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="endPointType">Describes the technology or protocol used by an IEndPointConfiguration.</param>
        /// <param name="apiName">An API_Name to use as a key.  Must match the API_Name of one or more IEndPointConfiguration objects. 
        /// API_Name is an arbitrary name given to the collection of services exposed by an API.  
        /// The name of a database or a domain name are examples of names that might also be used as an API_Name. </param>
        /// <param name="providerName">ProviderName typically represents some implementation of technology such as a DBMS platform.
        /// Examples might be: MSSQL, MySQL, SQLite, etc.</param>
        /// <returns></returns>
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
