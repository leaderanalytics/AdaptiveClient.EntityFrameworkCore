using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Autofac;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

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

            // For when we have an endPoint and we need to know what DbContext is associated.
            // We do not want InstancePerLifetimeScope here - when looping through a collection of EndPoints
            // we want a new instance for each endpoint.
            helper.Builder.RegisterType<TContext>().Keyed<DbContext>(apiName).UsingConstructor(typeof(DbContextOptions)); 
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
    }
}
