using _Client.Scripts;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ForkliftInputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<CargoSpawnController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.RegisterComponentInHierarchy<Forklift>();
        builder.RegisterComponentInHierarchy<PlayerView>();
        builder.RegisterComponentInHierarchy<CargoSpawnZone>().AsSelf();
        builder.RegisterComponentInHierarchy<CargoDeliveryZone>().AsSelf();
        builder.RegisterComponentInHierarchy<Dashboard>();
    }
}
