using _Client.Scripts;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CargoSpawnZone : MonoBehaviour
{
    [Inject] private CargoSpawnSettings _cargoSpawnSettings; 
    [Inject] private IObjectResolver _objectResolver;

    [ContextMenu("SpawnCargo")]
    public void SpawnCargo()
    {
        var cargoObject = _objectResolver.Instantiate(_cargoSpawnSettings.CargoPrefab,
            _cargoSpawnSettings.SpawnPosition, Quaternion.identity);
        
        cargoObject.ToggleRigidbody(false);
        var cargoTransform = cargoObject.transform;

        var rotationTween = cargoTransform
            .DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);

        cargoTransform.DOMove(_cargoSpawnSettings.TargetPosition, 5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                rotationTween.Kill(); 
                cargoTransform.rotation = Quaternion.identity;
                cargoObject.ToggleRigidbody(true);
            });
    }

}
