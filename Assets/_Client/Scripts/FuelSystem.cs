using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public float Fuel { get; private set; } = 100f;
    public bool IsEmpty => Fuel <= 0f;
    private const float drainPerSecond = 5f;

    public void Update()
    {
        Fuel = Mathf.Max(0f, Fuel - drainPerSecond * Time.deltaTime);
    }
}
