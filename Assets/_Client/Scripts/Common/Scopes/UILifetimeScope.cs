using UI;
using VContainer;
using VContainer.Unity;

namespace Common.Scopes
{
    public class UILifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<FadeScreen>();
        }
    }
}