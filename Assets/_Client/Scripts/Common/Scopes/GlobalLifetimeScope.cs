using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace Common.Scopes
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetReference[] _scenes;

        [SerializeField] private ScriptableObject[] _settings;
        [SerializeField] private Database[] _databases;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterClasses(builder);
            RegisterDatabases(builder);
            RegisterSettings(builder);
            _ = LoadScenes();
        }

        private async UniTaskVoid LoadScenes()
        {
            foreach (var sceneAssetReference in _scenes)
            {
                var async = Addressables.LoadSceneAsync(sceneAssetReference.RuntimeKey, LoadSceneMode.Additive, true);
                await async.Task;
            }
            
            await UniTask.WaitUntil(() => Find<UILifetimeScope>());
            
            var uiScope = Find<UILifetimeScope>();
            var screenFader = uiScope.Container.Resolve<FadeScreen>();
            
            screenFader.Fade(5f, FadeType.FadeOut);
        }

        private void RegisterSettings(IContainerBuilder builder)
        {
            foreach (var settingsFile in _settings)
            {
                var type = settingsFile.GetType();
                builder.RegisterInstance(settingsFile).As(type);
            }
        }

        private void RegisterDatabases(IContainerBuilder builder)
        {
            foreach (var database in _databases) builder.RegisterInstance(database).AsSelf();
        }

        private void RegisterClasses(IContainerBuilder builder)
        {
            builder.Register<GlobalInputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(new ForkliftInputActions()).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(_scenes);
        }
    }
}