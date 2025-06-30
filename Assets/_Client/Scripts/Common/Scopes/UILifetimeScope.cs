using VContainer;
using VContainer.Unity;

public class UILifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<FadeScreen>();
    }
}
