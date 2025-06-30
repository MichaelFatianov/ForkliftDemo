using System;

[Serializable]
public class Engine
{
    private EngineData _engineData;
    private float _vehicleMass;
    private float _baseAccelerationFactor;
    
    public Engine(EngineData engineData, float vehicleVehicleMass, float baseAccelerationFactor)
    {
        _engineData = engineData;
        _vehicleMass = vehicleVehicleMass;
        _baseAccelerationFactor = baseAccelerationFactor;
    }
    
    public bool IsRunning { get; private set; }

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
