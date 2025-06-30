using UnityEngine;

public class FuelSystem
{
    public float Fuel { get; private set; }
    
    public bool IsEmpty => Fuel <= 0f;
    
    private readonly EngineData _engineData;

    public FuelSystem(EngineData engineData)
    {
        _engineData = engineData;
        Fuel = 100f;
    }
    public void UpdateFuelValue()
    {
        Fuel = Mathf.Max(0f, Fuel - _engineData.DrainPerSecond * Time.deltaTime);
        // Debug.Log($"Fuel left: {Fuel}");
    }
}
