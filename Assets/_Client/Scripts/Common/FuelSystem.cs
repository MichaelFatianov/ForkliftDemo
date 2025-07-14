using Common.Engine;
using UnityEngine;

namespace Common
{
    public class FuelSystem
    {
        private readonly EngineData _engineData;

        public FuelSystem(EngineData engineData)
        {
            _engineData = engineData;
            Fuel = MaxCapacity;
        }

        public float Fuel { get; private set; }
        public float MaxCapacity => 100f;

        public float FuelLeft => Fuel / MaxCapacity;
        public bool IsEmpty => Fuel <= 0f;

        public void UpdateFuelValue()
        {
            Fuel = Mathf.Max(0f, Fuel - _engineData.DrainPerSecond * Time.deltaTime);
        }
    }
}