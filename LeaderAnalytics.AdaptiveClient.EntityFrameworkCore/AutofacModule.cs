namespace LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<DatabaseUtilities>().As<IDatabaseUtilities>();
        builder.Register<Func<IDbContextOptions>>(c => { ILifetimeScope cxt = c.Resolve<ILifetimeScope>(); return () => new ResolutionHelper(cxt).ResolveDbContextOptions(); });
    }
}
