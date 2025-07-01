using Common;
using Common.Engine;
using Common.Wheels;
using UI;
using UnityEngine;
using VContainer;

namespace Forklift
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        [Header("Fork")] [SerializeField] private GameObject _fork;

        [SerializeField] private float _forkBottomPosition;
        [SerializeField] private float _forkTopPosition;

        [Header("Wheels")] [SerializeField] private Wheel[] _wheels;

        private Dashboard _dashboard;
        private Engine _engine;

        private ForkliftSettings _forkliftSettings;
        private FuelSystem _fuelSystem;
        private ForkliftInputHandler _input;

        private void Update()
        {
            if (_input.IsEngineRunning)
            {
                _engine.Toggle();
                _dashboard.SetEngineStatus(_engine.IsRunning);
                _input.ClearFrameFlags();
            }

            if (_fuelSystem.IsEmpty || !_engine.IsRunning) return;
            _dashboard.SetFuelValue(_fuelSystem.FuelLeft);
            _fuelSystem.UpdateFuelValue();
        }

        private void FixedUpdate()
        {
            if (!_engine.IsRunning || _fuelSystem.IsEmpty) return;

            var speedMod = _fuelSystem.FuelLeft < _engine.PenaltyThreshold ? 0.5f : 1f;
            var moveInput = _input.MoveInput;
            var acceleration = _engine.GetForceValue();

            Brake(_input.IsBraking);
            Steer(moveInput.x);

            if (!_engine.IsRunning) return;
            MoveFork();
            Move(moveInput.y, acceleration, speedMod);
        }

        [Inject]
        public void Initialize(EngineDatabase engineDatabase, ForkliftInputHandler input, ForkliftSettings forkliftSettings,
            Dashboard dashboard)
        {
            _forkliftSettings = forkliftSettings;
            var engineData = engineDatabase.MappedEnginesData[forkliftSettings.EngineType.ToString()];
            _engine = new Engine(engineData, _rb.mass, 1f);
            _fuelSystem = new FuelSystem(engineData);
            _dashboard = dashboard;
            _input = input;

            UpdateDashboard();
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
                if (wheel.WheelAxis != WheelAxis.RearAxis) continue;
                var torque = input * acceleration * speedModifier * Time.fixedDeltaTime;
                wheel.WheelCollider.motorTorque = torque;
            }
        }

        private void Steer(float input)
        {
            foreach (var wheel in _wheels)
            {
                if (wheel.WheelAxis != WheelAxis.RearAxis) continue;
                var steerAngle = -input * _forkliftSettings.MaxSteerAngle;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, steerAngle, 0.5f);
            }
        }

        private void Brake(bool isBraking)
        {
            var brakingForce = isBraking ? _forkliftSettings.BrakeAcceleration * Time.fixedDeltaTime : 0f;
            foreach (var wheel in _wheels) wheel.WheelCollider.brakeTorque = brakingForce;
        }

        private void UpdateDashboard()
        {
            _dashboard.SetEngineStatus(_engine.IsRunning);
            _dashboard.SetFuelValue(_fuelSystem.FuelLeft);
        }
    }
}