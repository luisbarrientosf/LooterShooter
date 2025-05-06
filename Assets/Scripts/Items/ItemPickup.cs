using UnityEngine;

public class ItemPickup : MonoBehaviour {
  public Item item;
  private bool collected = false;

  private void OnTriggerEnter2D(Collider2D other) {
    if (collected) return;

    PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>();
    if (playerInventory != null) {
      collected = true;
      playerInventory.AddItem(item);
      Destroy(gameObject);
    }
  }
}
