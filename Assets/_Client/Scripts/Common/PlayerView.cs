using UnityEngine;
using VContainer;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minY = -60f, maxY = 240f;
    [SerializeField] private float minX = -30f, maxX = 60f;

    [Inject] private ForkliftInputActions _inputActions;
    
    private Vector2 _lookDelta;
    private float _currentX;
    private float _currentY;

    private void Awake()
    { 
        _inputActions.Forklift.Look.performed += context => _lookDelta = context.ReadValue<Vector2>();
        _inputActions.Forklift.Look.canceled += _ => _lookDelta = Vector2.zero;
    }

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    private void LateUpdate()
    {
        _currentX += _lookDelta.x * sensitivity;
        _currentY -= _lookDelta.y * sensitivity;
        _currentY = Mathf.Clamp(_currentY, minY, maxY);
        _currentX = Mathf.Clamp(_currentX, minX, maxX);

        cameraPivot.localRotation = Quaternion.Euler(_currentY, _currentX, 0f);
    }
}