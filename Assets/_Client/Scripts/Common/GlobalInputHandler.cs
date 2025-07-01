using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Common
{
    public class GlobalInputHandler:IStartable,IDisposable
    {
        [Inject] private ForkliftInputActions _inputActions;
        
        public void Start()
        {
            _inputActions.Forklift.Exit.performed += CloseApplication;
            _inputActions.Forklift.Restart.performed += RestartScene;
            _inputActions.Enable();
        }
        
        private void CloseApplication(InputAction.CallbackContext ctx)
        {
            Application.Quit();
        }

        private void RestartScene(InputAction.CallbackContext ctx)
        {
            var current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
        
        public void Dispose()
        {
            _inputActions.Disable();
        }
    }
}