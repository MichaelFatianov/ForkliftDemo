using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Common.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private Camera playerCamera;
        
        private float _currentX;
        private float _currentY;

        [Inject] private PlayerViewSettings _playerViewSettings;
        [Inject] private PlayerViewInputHandler _input;

        private void LateUpdate()
        {
            _currentX += _input.LookDelta.x * _playerViewSettings.Sensitivity;
            _currentY -= _input.LookDelta.y * _playerViewSettings.Sensitivity;
            _currentY = Mathf.Clamp(_currentY, _playerViewSettings.MinX, _playerViewSettings.MaxX);
            _currentX = Mathf.Clamp(_currentX, _playerViewSettings.MinY, _playerViewSettings.MaxY);

            cameraPivot.localRotation = Quaternion.Lerp(cameraPivot.localRotation, Quaternion.Euler(_currentY, _currentX, 0f),0.75f );
        }
    }
}