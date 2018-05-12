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
        public static DbContext ResolveDbContext(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            if (ep == null)
                throw new ArgumentNullException("ep");

            IDbContextOptions options = null;

            try
            {
                options = ResolveDbContextOptions(helper, ep);
            }
            catch (ComponentNotRegisteredException ex)
            {
                throw new ComponentNotRegisteredException($"DbContext could not be resolved. See InnerException for additional detail.", ex);
            }

            DbContext context = helper.cxt.ResolveOptionalKeyed<DbContext>(ep.API_Name, new TypedParameter(typeof(DbContextOptions), options.Options));

            if (context == null)
                throw new ComponentNotRegisteredException($"DbContext could not be resolved for API_Name {ep.API_Name}. Call RegisterDbContext with an API_Name of {ep.API_Name} to register the required component.");

            return context;
        }

        /// <summary>
        /// This overload is primarily for internal use by AdaptiveClient.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IDbContextOptions ResolveDbContextOptions(this ResolutionHelper helper) 
        {
            Func<IEndPointConfiguration> epFactory = helper.cxt.Resolve<Func<IEndPointConfiguration>>();  // Registered by AdaptiveClient.  Returns EndPointContext.CurrentEndPoint 
            IEndPointConfiguration ep = epFactory();

            if (ep == null)
                throw new Exception("EndPointContext.CurrentEndPoint is null.");


            return ResolveDbContextOptions(helper, ep);
        }

        public static IDbContextOptions ResolveDbContextOptions(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            IDbContextOptions options = helper.cxt.ResolveOptionalKeyed<IDbContextOptions>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));

            if (options == null)
                throw new ComponentNotRegisteredException($"IDbContextOptions have not been registered for ProviderName {ep.ProviderName}. Call RegisterDbContextOptions with a ProviderName of {ep.ProviderName} to register the required component.");

            return options;
        }

        /// <summary>
        /// A migration context is a placeholder type that can be registered against a specific API_Name and ProviderName.
        /// One or more migration contexts might derive from the same DBContext.  The type name of each migration context is 
        /// used by AdaptiveClient as a key for resolving supporting objects such as DbContextOptions.  The type name of the
        /// migration context is also used by EF itself for creating migrations and updating the 
        /// database (--context command line option).
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static DbContext ResolveMigrationContext(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            IDbContextOptions options = null;

            try
            {
                options = ResolveDbContextOptions(helper, ep);
            }
            catch (ComponentNotRegisteredException)
            {
                return null;
            }

            return helper.cxt.ResolveOptionalKeyed<IMigrationContext>(ep.API_Name + ep.ProviderName, new TypedParameter(typeof(DbContextOptions), options.Options)) as DbContext;
        }

        public static IDatabaseInitializer ResolveDatabaseInitializer(this ResolutionHelper helper, IEndPointConfiguration ep)
        {
            DbContext context = null;

            try
            {
                context = ResolveDbContext(helper, ep);
            }
            catch (ComponentNotRegisteredException)
            {
                return null;
            }

            return helper.cxt.ResolveOptionalKeyed<IDatabaseInitializer>(ep.API_Name + ep.ProviderName, new TypedParameter(context.GetType(), context));
        }
    }
}
