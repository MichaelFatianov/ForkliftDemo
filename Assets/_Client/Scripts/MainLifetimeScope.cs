using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ForkliftInputHandler>();
        builder.Register<Forklift>(Lifetime.Singleton);
        builder.Register<Engine>(Lifetime.Singleton);
        builder.Register<FuelSystem>(Lifetime.Singleton);
        builder.Register<LiftMechanism>(Lifetime.Singleton);
        builder.RegisterEntryPoint<EngineInput>();
    }
}
