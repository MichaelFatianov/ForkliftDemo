using VContainer.Unity;

namespace _Client.Scripts
{
    public class CargoSpawnController: IStartable
    {
        private CargoSpawnZone _cargoSpawnZone;
        private CargoDeliveryZone _cargoDeliveryZone;
        
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
            if(_cargoSpawnZone.IsEmpty)
                _cargoSpawnZone.SpawnCargo();
        }

        private void OnCargoDelivered()
        {
            SpawnCargo();
        }
    }
}