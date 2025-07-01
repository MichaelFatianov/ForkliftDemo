using Common.Engine.Types;
using UnityEngine;

namespace Forklift
{
    [CreateAssetMenu(fileName = "ForkliftSettings", menuName = "Settings/Forklift Settings")]
    public class ForkliftSettings : ScriptableObject
    {
        [SerializeField] private float _maxSteerAngle = 60f;
        [SerializeField] private float _brakeAcceleration = 30000f;
        [SerializeField] private EngineType _engineType;

        public EngineType EngineType => _engineType;
        public float MaxSteerAngle => _maxSteerAngle;
        public float BrakeAcceleration => _brakeAcceleration;
    }
}