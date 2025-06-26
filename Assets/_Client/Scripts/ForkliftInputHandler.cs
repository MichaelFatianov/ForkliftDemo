using System;
using UnityEngine;
using VContainer.Unity;

public class ForkliftInputHandler : IStartable, IDisposable
{
    public Vector2 MoveInput { get; private set; }
    public float LiftInput { get; private set; }
    public bool ToggleEnginePressed { get; private set; }

    private ForkliftInputActions inputActions;

    public void Start()
    {
        inputActions = new ForkliftInputActions();
        inputActions.Forklift.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        inputActions.Forklift.Move.canceled += _ => MoveInput = Vector2.zero;

        inputActions.Forklift.ForkUp.performed += context => LiftInput = context.ReadValue<float>();
        inputActions.Forklift.ForkUp.canceled += _ => LiftInput = 0f;
        
        inputActions.Forklift.ForkDown.performed += context => LiftInput = -context.ReadValue<float>();
        inputActions.Forklift.ForkDown.canceled += _ => LiftInput = 0f;

        inputActions.Forklift.ToggleEngine.performed += _ => ToggleEnginePressed = !ToggleEnginePressed;

        inputActions.Enable();
    }

    public void Dispose()
    {
        inputActions.Disable();
    }

    public void ClearFrameFlags()
    {
        ToggleEnginePressed = false;
    }
}
