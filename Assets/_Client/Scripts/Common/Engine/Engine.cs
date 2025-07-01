using System;

[Serializable]
public class Engine
{
    private EngineData _engineData;
    private float _vehicleMass;
    private float _baseAccelerationFactor;
    
    private bool _isRunning;
    
    public bool IsRunning => _isRunning;
    public float PenaltyThreshold => _engineData.PenaltyThreshold;
    
    public Engine(EngineData engineData, float vehicleVehicleMass, float baseAccelerationFactor)
    {
        _engineData = engineData;
        _vehicleMass = vehicleVehicleMass;
        _baseAccelerationFactor = baseAccelerationFactor;
    }

    public void Toggle()
    {
        _isRunning = !_isRunning;
    }

    public float GetForceValue()
    {
        var acceleration = _engineData.HorsePowers * _baseAccelerationFactor / (_vehicleMass / 500f);

        var force = _vehicleMass * acceleration;
        return force;
    }
}
