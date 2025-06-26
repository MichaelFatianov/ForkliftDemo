using UnityEngine;

public class Engine
{
    public bool IsRunning { get; private set; }

    public void Toggle()
    {
        IsRunning = !IsRunning;
    }
}
