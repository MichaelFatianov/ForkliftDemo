using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.Cargo
{
    public class CargoSpawnZone : CargoZone
    {
        [SerializeField] private TriggerBox _triggerBox;
        private CargoSpawnSettings _cargoSpawnSettings;

        private IObjectResolver _objectResolver;
        public bool IsEmpty { get; private set; }

        [Inject]
        private void Initialize(CargoSpawnSettings cargoSpawnSettings, IObjectResolver objectResolver)
        {
            _cargoSpawnSettings = cargoSpawnSettings;
            _objectResolver = objectResolver;
            IsEmpty = true;
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
                    IsEmpty = false;
                });
        }

        private void OnCargoEnterTrigger(Collider other)
        {
            IsEmpty = false;
        }

        private void OnCargoExitTrigger(Collider other)
        {
            IsEmpty = true;
        }
    }
}