using UnityEngine;

public class FuelSystem
{
    private float _fuel;
    private float _maxCapacity = 100f;
    public float Fuel => _fuel;
    public float MaxCapacity => _maxCapacity;
    public float FuelLeft => _fuel / _maxCapacity;
    public bool IsEmpty => _fuel <= 0f;
    
    private readonly EngineData _engineData;

    public FuelSystem(EngineData engineData)
    {
        _engineData = engineData;
        _fuel = _maxCapacity;
    }
    public void UpdateFuelValue()
    {
        _fuel = Mathf.Max(0f, _fuel - _engineData.DrainPerSecond * Time.deltaTime);
    }
}
