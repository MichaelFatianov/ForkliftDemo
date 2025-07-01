using UnityEngine;

namespace Common.Cargo
{
    [CreateAssetMenu(fileName = "CargoSpawnSettings", menuName = "Settings/Cargo Spawn Settings")]
    public class CargoSpawnSettings : ScriptableObject
    {
        [SerializeField] private CargoObject _cargoPrefab;
        [SerializeField] private string _cargoTag;

        public CargoObject CargoPrefab => _cargoPrefab;
        public string CargoTag => _cargoTag;
    }
}