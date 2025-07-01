using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.View
{
    public class PlayerViewInputHandler: IStartable, IDisposable
    {
        [Inject] private ForkliftInputActions _inputActions;

        public Vector2 LookDelta { get; private set; }

        public void Dispose()
        {
            _inputActions.Disable();
        }

        public void Start()
        {
            _inputActions.Forklift.Look.performed += context => LookDelta = context.ReadValue<Vector2>();
            _inputActions.Forklift.Look.canceled += _ => LookDelta = Vector2.zero;
            _inputActions.Enable();
        }
    }
}