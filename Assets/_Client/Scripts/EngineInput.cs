using UnityEngine;
using VContainer;

public class EngineInput : MonoBehaviour
{
    [Inject] private ForkliftInputHandler input;
    [Inject] private Engine engine;

    void Update()
    {
        if (input.ToggleEnginePressed)
        {
            engine.Toggle();
            input.ClearFrameFlags();
        }
    }
}
