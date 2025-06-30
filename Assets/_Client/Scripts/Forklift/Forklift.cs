using _Client.Scripts.Engine.Types.Enum;
using UnityEngine;
using VContainer;

public class Forklift : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private GameObject _fork;

    [SerializeField] private float _forkBottomPosition;
    [SerializeField] private float _forkTopPosition;
    
    [SerializeField] private float _maxSteerAngle = 30f;
    [SerializeField] private float _brakeAcceleration = 30000f;

    [SerializeField] private EngineType _engineType;
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private Vector3 _centerOfMass;
    
    private Engine _engine;
    private FuelSystem _fuelSystem;
    private ForkliftInputHandler _input;
    
    [Inject]
    public void Initialize(EngineDatabase engineDatabase, ForkliftInputHandler input)
    {
        var engineData = engineDatabase.MappedEnginesData[_engineType.ToString()];
        _engine = new Engine(engineData, _rb.mass, 1f);
        _fuelSystem = new FuelSystem(engineData);
        _input = input;
    }

    void Update()
    {
        if (_input.IsEngineRunning)
        {
            _engine.Toggle();
            _input.ClearFrameFlags();
        }
        
        if (_fuelSystem.IsEmpty || !_engine.IsRunning) return;
        _fuelSystem.UpdateFuelValue();
    }
    
    void FixedUpdate()
    {
        if (!_engine.IsRunning || _fuelSystem.IsEmpty) return;
        
        var speedMod = _fuelSystem.Fuel < 50f ? 0.5f : 1f;
        var moveInput = _input.MoveInput;
        var acceleration = _engine.GetForceValue();
        
        Brake(_input.IsBraking);
        Steer(moveInput.x);
        
        if (!_engine.IsRunning) return;
        MoveFork();
        Move(moveInput.y, acceleration, speedMod);
    }

    private void MoveFork()
    {
        if (_input.LiftInput == 0) return;
        var forkPositionY = _fork.transform.localPosition.y;
        if (forkPositionY <= _forkBottomPosition && _input.LiftInput < 0) return;
        if (forkPositionY >= _forkTopPosition && _input.LiftInput > 0) return;
        _fork.transform.localPosition += Vector3.up * (1f * _input.LiftInput * Time.deltaTime);
    }

    private void Move(float input, float acceleration, float speedModifier)
    {
        foreach (var wheel in _wheels)
        {
            switch (wheel.WheelAxis)
            {
                case WheelAxis.FrontAxis:
                    break;
                case WheelAxis.RearAxis:
                    var torque = input * acceleration * speedModifier * Time.fixedDeltaTime;
                    wheel.WheelCollider.motorTorque = torque;
                    break;
            }
        }
    }
    
    private void Steer(float input)
    {
        foreach (var wheel in _wheels)
        {
            switch (wheel.WheelAxis)
            {
                case WheelAxis.FrontAxis:
                    break;
                case WheelAxis.RearAxis:
                    var steerAngle = -input * _maxSteerAngle;
                    wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, steerAngle, 0.5f);
                    break;
            }
        }
    }

    private void Brake(bool isBraking)
    {
        Debug.Log($"Braking?: {isBraking} with force {_brakeAcceleration}");
        var brakingForce = isBraking ? _brakeAcceleration * Time.fixedDeltaTime : 0f;
        foreach (var wheel in _wheels)
        {
            wheel.WheelCollider.brakeTorque = brakingForce;
        }
    }
}
