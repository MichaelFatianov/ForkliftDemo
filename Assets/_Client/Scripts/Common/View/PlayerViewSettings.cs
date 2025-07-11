using UnityEngine;

namespace Common.View
{
    [CreateAssetMenu(fileName = "PlayerViewSettings", menuName = "Settings/Player View Settings")]
    public class PlayerViewSettings: ScriptableObject
    {
        [SerializeField] private float _sensitivity = 2f;
        
        [Header ("Horizontal View")]
        [SerializeField] private float _minY = -60f;
        [SerializeField] private float _maxY = 240f;

        [Header ("Vertical View")]
        [SerializeField] private float _minX = -30f;
        [SerializeField] private float _maxX = 60f;
        
        public float Sensitivity => _sensitivity;
        public float MinX => _minX;
        public float MaxX => _maxX;
        public float MinY => _minY;
        public float MaxY => _maxY;
    }
}