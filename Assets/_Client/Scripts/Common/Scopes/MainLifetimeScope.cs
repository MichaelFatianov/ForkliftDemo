using Common.Cargo;
using Common.View;
using Forklift;
using UI;
using VContainer;
using VContainer.Unity;

namespace Common.Scopes
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ForkliftInputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PlayerViewInputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CargoSpawnController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<Forklift.Forklift>();
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.RegisterComponentInHierarchy<CargoSpawnZone>().AsSelf();
            builder.RegisterComponentInHierarchy<CargoDeliveryZone>().AsSelf();
            builder.RegisterComponentInHierarchy<Dashboard>();
        }
    }
}