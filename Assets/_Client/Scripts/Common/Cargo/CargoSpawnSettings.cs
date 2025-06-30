using UnityEngine;

namespace _Client.Scripts
{
    [CreateAssetMenu(fileName = "CargoSpawnSettings", menuName = "Settings/Cargo Spawn Settings")]
    public class CargoSpawnSettings : ScriptableObject
    {
        [SerializeField] private Vector3 spawnPosition;   
        [SerializeField] private Vector3 targetPosition;
        
        [SerializeField] private CargoObject cargoPrefab;
        
        public Vector3 SpawnPosition => spawnPosition;
        public Vector3 TargetPosition => targetPosition;
        public CargoObject CargoPrefab => cargoPrefab;
    }
}