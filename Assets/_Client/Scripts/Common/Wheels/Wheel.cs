using System;
using UnityEngine;

[Serializable]
public struct Wheel
{
    [SerializeField] private WheelAxis _wheelAxis;
    [SerializeField] private GameObject _wheelMesh;
    [SerializeField] private WheelCollider _wheelCollider;
    
    public WheelAxis WheelAxis => _wheelAxis;
    public WheelCollider WheelCollider => _wheelCollider;
    public GameObject WheelMesh => _wheelMesh;
}
