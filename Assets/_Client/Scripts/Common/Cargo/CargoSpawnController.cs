using VContainer.Unity;

namespace Common.Cargo
{
    public class CargoSpawnController : IStartable
    {
        private readonly CargoDeliveryZone _cargoDeliveryZone;
        private readonly CargoSpawnZone _cargoSpawnZone;

        public CargoSpawnController(CargoSpawnZone cargoSpawnZone, CargoDeliveryZone cargoDeliveryZone)
        {
            _cargoSpawnZone = cargoSpawnZone;
            _cargoDeliveryZone = cargoDeliveryZone;
        }

        public void Start()
        {
            _cargoDeliveryZone.SetCallbacks(OnCargoDelivered);
            SpawnCargo();
        }

        private void SpawnCargo()
        {
            if (_cargoSpawnZone.IsEmpty)
                _cargoSpawnZone.SpawnCargo();
        }

        private void OnCargoDelivered()
        {
            SpawnCargo();
        }
    }
}