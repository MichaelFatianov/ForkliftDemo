using UnityEngine;

namespace Common.Engine
{
    [CreateAssetMenu(fileName = "NewEngineData", menuName = "Data/EngineData")]
    public class EngineData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private int horsePowers;
        [SerializeField] private float drainPerSecond;
        [SerializeField] [Range(0f, 1f)] private float penaltyThreshold;

        public string Id => id;
        public int HorsePowers => horsePowers;
        public float DrainPerSecond => drainPerSecond;
        public float PenaltyThreshold => penaltyThreshold;
    }
}