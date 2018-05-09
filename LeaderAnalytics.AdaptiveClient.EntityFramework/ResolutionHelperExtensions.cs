using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public static class ResolutionHelperExtensions
    {
        public static IDbContextOptions<TContext> ResolveDbContextOptions<TContext>(this ResolutionHelper helper) where TContext : DbContext
        {
            Func<IEndPointConfiguration> epFactory = helper.cxt.Resolve<Func<IEndPointConfiguration>>();  // Registered by AdaptiveClient
            IEndPointConfiguration ep = epFactory();
            IDbContextOptions<TContext> options = helper.cxt.ResolveKeyed<IDbContextOptions<TContext>>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));
            return options;
        }

        public static IDbContextOptions ResolveDbContextOptions(this ResolutionHelper helper) 
        {
            Func<IEndPointConfiguration> epFactory = helper.cxt.Resolve<Func<IEndPointConfiguration>>();  // Registered by AdaptiveClient
            IEndPointConfiguration ep = epFactory();
            IDbContextOptions options = helper.cxt.ResolveKeyed<IDbContextOptions>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));
            return options;
        }

        public static DbContext ResolveContext(this ResolutionHelper helper, IEndPointConfiguration ep) 
        {
            IDbContextOptions options = helper.cxt.ResolveKeyed<IDbContextOptions>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));
            //DbContext context = cxt.ResolveKeyed<DbContext>(ep.API_Name + ep.ProviderName, new TypedParameter(typeof(DbContextOptions), options.Options));
            Func<IDbContextOptions, DbContext> contextFunc = helper.cxt.ResolveKeyed<Func<IDbContextOptions, DbContext>>(ep.API_Name + ep.ProviderName, new TypedParameter(typeof(IDbContextOptions), options.Options));
            DbContext context = contextFunc(options);
            return context;
        }

        /// <summary>
        /// A migration context is a placeholder type that can be registered against a specific API_Name and ProviderName.
        /// One or more migration contexts might derive from the same DBContext.  The type name of each migration context is 
        /// used by AdaptiveClient as a key for resolving supporting objects such as DbContextOptions.  The type name of the
        /// migration context is also used by EF itself for creating migrations and updating the 
        /// database (--context command line option).
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static DbContext ResolveMigrationContext(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            IDbContextOptions options = helper.cxt.ResolveKeyed<IDbContextOptions>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));
            DbContext context = helper.cxt.ResolveKeyed<IMigrationContext>(ep.API_Name + ep.ProviderName, new TypedParameter(typeof(DbContextOptions), options.Options)) as DbContext;
            return context;
        }

        public static IDataInitializer ResolveDataInitializer(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            object migrator = null;
            DbContext context = ResolveContext(helper, ep);

            try
            {
                migrator = helper.cxt.ResolveKeyed(ep.API_Name + ep.ProviderName, typeof(IDataInitializer), new TypedParameter(context.GetType(), context));
            }
            catch (Exception)
            {
                // swallow it
            }

            if (migrator != null)
                return migrator as IDataInitializer;

            return null;
        }
    }
}
