using UnityEngine;

public class CargoObject : MonoBehaviour
{
   [SerializeField] private Rigidbody _cargoRigidbody;
   [SerializeField] private Collider[] _cargoColliders;
    
   public void ToggleRigidbody(bool toggle)
   {
      _cargoRigidbody.useGravity = toggle;
      _cargoRigidbody.isKinematic = !toggle;
      foreach (var cargoCollider in _cargoColliders)
      {
         cargoCollider.enabled = toggle;
      }
   }
}
