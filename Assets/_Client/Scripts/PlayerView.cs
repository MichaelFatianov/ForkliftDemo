using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot; // точка вращения (обычно пустышка в кабине)
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minY = -60f, maxY = 240f;
    [SerializeField] private float minX = -30f, maxX = 60f;

    private ForkliftInputActions inputActions;
    private Vector2 lookDelta;
    private float currentX, currentY;

    private void Awake()
    {
        inputActions = new ForkliftInputActions(); // Ижектить
        inputActions.Forklift.Look.performed += context => lookDelta = context.ReadValue<Vector2>();
        inputActions.Forklift.Look.canceled += _ => lookDelta = Vector2.zero;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void LateUpdate()
    {
        currentX += lookDelta.x * sensitivity;
        currentY -= lookDelta.y * sensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);
        currentX = Mathf.Clamp(currentX, minX, maxX);

        cameraPivot.localRotation = Quaternion.Euler(currentY, currentX, 0f);
    }
}