using System;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Common.Cargo
{
    public class CargoDeliveryZone : CargoZone
    {
        [SerializeField] private TriggerBox _triggerBox;

        private Action _onCargoDelivered;

        [Inject]
        public void Initialize(CargoSpawnSettings cargoSpawnSettings)
        {
            _triggerBox.Initialize(OnCargoEnterTrigger, _ => { }, _ => { }, cargoSpawnSettings.CargoTag);
        }

        public void SetCallbacks(Action onCargoDelivered)
        {
            _onCargoDelivered = onCargoDelivered;
        }

        private void OnCargoEnterTrigger(Collider other)
        {
            var cargoObject = other.GetComponent<CargoObject>();
            DeliveryAnimation(cargoObject);
        }

        private void DeliveryAnimation(CargoObject cargoObject)
        {
            cargoObject.ToggleRigidbody(false);

            var cargoTransform = cargoObject.transform;
            var rotationTweenY = cargoTransform
                .DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
            var rotationTweenXZ = cargoTransform
                .DORotate(new Vector3(360f, 0f, 360f), 1f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);

            cargoTransform.DOMove(spawnPoint.position, 5f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    rotationTweenY.Kill();
                    rotationTweenXZ.Kill();
                    Destroy(cargoObject.gameObject);
                    _onCargoDelivered();
                });
        }
    }
}