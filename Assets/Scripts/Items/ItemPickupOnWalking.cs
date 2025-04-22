using UnityEngine;

public class ItemPickupOnWaling : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    PlayerInventory inventory = collider.GetComponent<PlayerInventory>();

    if (inventory != null) {
      inventory.AddCoin();
      Destroy(gameObject);
    }
  }
}
