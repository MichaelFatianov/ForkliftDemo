using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Forklift
{
    public class ForkliftInputHandler : IStartable, IDisposable
    {
        [Inject] private ForkliftInputActions _inputActions;

        public Vector2 MoveInput { get; private set; }
        public float LiftInput { get; private set; }

        public bool IsEngineRunning { get; private set; }

        public bool IsBraking { get; private set; }

        public void Dispose()
        {
            _inputActions.Disable();
        }

        public void Start()
        {
            _inputActions.Forklift.Move.performed += Move;
            _inputActions.Forklift.Move.canceled += _ => MoveInput = Vector2.zero;

            _inputActions.Forklift.ForkUp.performed += ctx => Lift(ctx, 1);
            _inputActions.Forklift.ForkUp.canceled += _ => LiftInput = 0f;

            _inputActions.Forklift.ForkDown.performed += ctx => Lift(ctx, -1);
            _inputActions.Forklift.ForkDown.canceled += _ => LiftInput = 0f;

            _inputActions.Forklift.ToggleEngine.performed += _ => IsEngineRunning = true;
            ;

            _inputActions.Forklift.Brake.performed += _ => IsBraking = true;
            _inputActions.Forklift.Brake.canceled += _ => IsBraking = false;

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

        public void ClearFrameFlags()
        {
            IsEngineRunning = false;
        }
    }
}