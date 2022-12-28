using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient;

public static class ResolutionHelperExtensions
{
    /// <summary>
    /// Returns an instance of DbContext keyed to the API_Name of the passed IEndPointConfiguration.
    /// </summary>
    /// <param name="helper">An instance of ResolutionHelper.</param>
    /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
    /// <returns>DbContext</returns>
    public static DbContext ResolveDbContext(this ResolutionHelper helper, IEndPointConfiguration ep)
    {
        if (ep == null)
            throw new ArgumentNullException("ep");

        EntityFrameworkCore.IDbContextOptions options = null;

        try
        {
            options = ResolveDbContextOptions(helper, ep);
        }
        catch (ComponentNotRegisteredException ex)
        {
            throw new ComponentNotRegisteredException($"DbContext could not be resolved. See InnerException for additional detail.", ex);
        }

        DbContext context = helper.scope.ResolveOptionalKeyed<DbContext>(ep.API_Name, new TypedParameter(typeof(DbContextOptions), options.Options));

        if (context == null)
            throw new ComponentNotRegisteredException($"DbContext could not be resolved for API_Name {ep.API_Name}. Call RegisterDbContext with an API_Name of {ep.API_Name} to register the required component.");

        return context;
    }

    /// <summary>
    /// This overload is primarily for internal use by AdaptiveClient.  Resolves an implementation of IDbContextOptions using 
    /// EndPointContext.CurrentEndPoint which is set internally by AdaptiveClient.
    /// </summary>
    /// <param name="helper">An instance of ResolutionHelper.</param>
    /// <returns>An implementation of IDbContextOptions</returns>
    public static EntityFrameworkCore.IDbContextOptions ResolveDbContextOptions(this ResolutionHelper helper) 
    {
        Func<IEndPointConfiguration> epFactory = helper.scope.Resolve<Func<IEndPointConfiguration>>();  // Registered by AdaptiveClient.  Returns EndPointContext.CurrentEndPoint 
        IEndPointConfiguration ep = epFactory();

        if (ep == null)
            throw new Exception("EndPointContext.CurrentEndPoint is null.");


        return ResolveDbContextOptions(helper, ep);
    }

    /// <summary>
    /// Returns an implementation of IDbContextOptions keyed to the ProviderName of the passed IEndPointConfiguration.
    /// </summary>
    /// <param name="helper">An instance of ResolutionHelper.</param>
    /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
    /// <returns>An implementation of IDbContextOptions.</returns>
    public static EntityFrameworkCore.IDbContextOptions ResolveDbContextOptions(this ResolutionHelper helper, IEndPointConfiguration ep)
    {
        EntityFrameworkCore.IDbContextOptions options = helper.scope.ResolveOptionalKeyed<EntityFrameworkCore.IDbContextOptions>(ep.ProviderName, new TypedParameter(typeof(string), ep.ConnectionString));

        if (options == null)
            throw new ComponentNotRegisteredException($"IDbContextOptions have not been registered for ProviderName {ep.ProviderName}. Call RegisterDbContextOptions with a ProviderName of {ep.ProviderName} to register the required component.");

        return options;
    }

    /// <summary>
    /// Returns a MigrationContext which is a placeholder type that derives from DbContext.  The MigrationContext that is
    /// returned is keyed to API_Name and ProviderName of the passed IEndPointConfiguration.
    /// One or more MigrationContexts might derive from the same DBContext.  The type name of each migration context is 
    /// used by AdaptiveClient as a key for resolving supporting objects such as DbContextOptions.  The type name of the
    /// migration context is also used by EF itself for creating migrations and updating the 
    /// database (--context command line option).
    /// </summary>
    /// <param name="helper">An instance of ResolutionHelper.</param>
    /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
    /// <returns>DbContext</returns>
    public static DbContext ResolveMigrationContext(this ResolutionHelper helper, IEndPointConfiguration ep)
    {
        EntityFrameworkCore.IDbContextOptions options = null;

        try
        {
            options = ResolveDbContextOptions(helper, ep);
        }
        catch (ComponentNotRegisteredException)
        {
            return null;
        }

        return helper.scope.ResolveOptionalKeyed<IMigrationContext>(ep.API_Name + ep.ProviderName, new TypedParameter(typeof(DbContextOptions), options.Options)) as DbContext;
    }

    /// <summary>
    /// Returns an instance of IDatabaseInitializer which is used to seed a database after it is created or a migration is applied. The instance
    /// of IDatabaseInitializer that is returned is keyed to the API_Name and ProviderName of the passed IEndPointConfiguration.
    /// </summary>
    /// <param name="helper">An instance of ResolutionHelper.</param>
    /// <param name="ep">The IEndPointConfiguration whose properties will be used as keys.</param>
    /// <returns>IDatabaseInitializer</returns>
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

        return helper.scope.ResolveOptionalKeyed<IDatabaseInitializer>(ep.API_Name + ep.ProviderName, new TypedParameter(context.GetType(), context));
    }
}
