using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

public class ForkliftInputHandler : IStartable, IDisposable
{
    public Vector2 MoveInput { get; private set; }
    public float LiftInput { get; private set; }
    
    private bool _isEngineRunning;
    private bool _isBraking;
    
    public bool IsEngineRunning => _isEngineRunning;
    public bool IsBraking => _isBraking;

    [Inject] private ForkliftInputActions _inputActions;

    public void Start()
    {
        _inputActions.Forklift.Move.performed += Move;
        _inputActions.Forklift.Move.canceled += _ => MoveInput = Vector2.zero;

        _inputActions.Forklift.ForkUp.performed += ctx =>  Lift(ctx,1);
        _inputActions.Forklift.ForkUp.canceled += _ => LiftInput = 0f;
        
        _inputActions.Forklift.ForkDown.performed += ctx => Lift(ctx,-1);
        _inputActions.Forklift.ForkDown.canceled += _ => LiftInput = 0f;

        _inputActions.Forklift.ToggleEngine.performed += _ => _isEngineRunning = true;;

        _inputActions.Forklift.Brake.performed += _ => _isBraking = true;
        _inputActions.Forklift.Brake.canceled += _ => _isBraking = false;

        _inputActions.Enable();
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void Lift(InputAction.CallbackContext ctx, int direction)
    {
        LiftInput = ctx.ReadValue<float>() * direction;
    }

    public void Dispose()
    {
        _inputActions.Disable();
    }

    public void ClearFrameFlags()
    {
        _isEngineRunning = false;
    }
}
