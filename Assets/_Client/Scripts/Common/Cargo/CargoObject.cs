using UnityEngine;

public class CargoObject : MonoBehaviour
{
   [SerializeField] private Rigidbody cargoRigidbody;
   
   public void ToggleRigidbody(bool toggle)
   {
      cargoRigidbody.useGravity = toggle;
      cargoRigidbody.isKinematic = !toggle;
   }
}
