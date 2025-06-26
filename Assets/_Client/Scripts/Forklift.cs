using UnityEngine;
using VContainer;

public class Forklift : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform visualModel; 
    [SerializeField] private Transform frontWheelsPivot;
    
    [SerializeField] private float acceleration = 1000f;
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float turnSensitivity = 2f;
    [SerializeField] private float minMoveSpeedToTurn = 0.1f;

    [Inject] private Engine engine;
    [Inject] private FuelSystem fuel;
    [Inject] private ForkliftInputHandler input;

    private float currentSpeed;
    private float steerAngle;

    void FixedUpdate()
    {
        if (!engine.IsRunning || fuel.IsEmpty) return;

        float speedMod = fuel.Fuel < 50f ? 0.5f : 1f;
        Vector2 move = input.MoveInput;

        Vector3 force = transform.forward * (move.y * acceleration * speedMod);
        rb.AddForce(force);

        currentSpeed = rb.linearVelocity.magnitude;
        
        if (Mathf.Abs(move.y) > 0.1f && currentSpeed > minMoveSpeedToTurn)
        {
            steerAngle = move.x * maxSteerAngle;
        }
        else
        {
            steerAngle = 0f;
        }
        
        if (steerAngle != 0f)
        {
            Quaternion deltaRot = Quaternion.Euler(Vector3.up * (steerAngle * move.y * turnSensitivity * Time.fixedDeltaTime));
            rb.MoveRotation(rb.rotation * deltaRot);
        }
        
        if (frontWheelsPivot != null)
        {
            frontWheelsPivot.localRotation = Quaternion.Euler(0, steerAngle, 0);
        }
    }
}
