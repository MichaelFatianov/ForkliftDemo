using _Client.Scripts;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CargoSpawnZone : CargoZone
{
    [SerializeField] private TriggerBox _triggerBox;
    private CargoSpawnSettings _cargoSpawnSettings; 
    private IObjectResolver _objectResolver;
    
    private bool _isEmpty;
    public bool IsEmpty => _isEmpty;

    [Inject]
    private void Initialize(CargoSpawnSettings cargoSpawnSettings, IObjectResolver objectResolver)
    {
        _cargoSpawnSettings = cargoSpawnSettings;
        _objectResolver = objectResolver;
        _isEmpty = true;
        _triggerBox.Initialize(OnCargoEnterTrigger, OnCargoExitTrigger, _ => { }, cargoSpawnSettings.CargoTag);
    }

    [ContextMenu("SpawnCargo")]
    public void SpawnCargo()
    {
        var cargoObject = _objectResolver.Instantiate(_cargoSpawnSettings.CargoPrefab,
            spawnPoint.position, Quaternion.identity);
        
        cargoObject.ToggleRigidbody(false);
        var cargoTransform = cargoObject.transform;

        var rotationTween = cargoTransform
            .DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);

        cargoTransform.DOMove(targetPoint.position, 5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                rotationTween.Kill(); 
                cargoTransform.rotation = Quaternion.identity;
                cargoObject.ToggleRigidbody(true);
                _isEmpty = false;
            });
    }

    void OnCargoEnterTrigger(Collider other)
    {
        _isEmpty = false;
    }

    void OnCargoExitTrigger(Collider other)
    {
        _isEmpty = true;
    }

}
