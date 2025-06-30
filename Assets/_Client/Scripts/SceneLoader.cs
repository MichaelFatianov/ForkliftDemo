using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public async UniTask LoadSceneAdditiveAsync(AssetReference sceneReference)
    {
        if (sceneReference == null)
        {
            Debug.LogError("Scene reference is null.");
            return;
        }

        if (!sceneReference.RuntimeKeyIsValid())
        {
            Debug.LogError($"Invalid AssetReference: {sceneReference.RuntimeKey}");
            return;
        }

        AsyncOperationHandle<SceneInstance> handle;

        try
        {
            handle = sceneReference.LoadSceneAsync(LoadSceneMode.Additive, activateOnLoad: true);
            await handle.ToUniTask();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load scene from addressable: {sceneReference.RuntimeKey}\n{ex}");
            return;
        }

        if (!handle.IsDone || handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Scene loading failed: {sceneReference.RuntimeKey}");
            return;
        }

        Debug.Log($"Scene '{sceneReference.RuntimeKey}' loaded additively.");
    }
}
