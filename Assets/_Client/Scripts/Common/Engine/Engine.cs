using System;

namespace Common.Engine
{
    [Serializable]
    public class Engine
    {
        private float _baseAccelerationFactor;
        private EngineData _engineData;

        private float _vehicleMass;

        public Engine(EngineData engineData, float vehicleVehicleMass, float baseAccelerationFactor)
        {
            _engineData = engineData;
            _vehicleMass = vehicleVehicleMass;
            _baseAccelerationFactor = baseAccelerationFactor;
        }

        public bool IsRunning { get; private set; }

        public float PenaltyThreshold => _engineData.PenaltyThreshold;

        public void Toggle()
        {
            IsRunning = !IsRunning;
        }

        public float GetForceValue()
        {
            var acceleration = _engineData.HorsePowers * _baseAccelerationFactor / (_vehicleMass / 500f);

            var force = _vehicleMass * acceleration;
            return force;
        }
    }
}