using UnityEngine;

public class ItemPickup : MonoBehaviour {
  public Item item;
  private bool playerInRange = false;
  private bool collected = false;
  private PlayerInventory playerInventory;
  private HudUIManager hudManager;

  void Update() {
    if (playerInRange && !collected && Input.GetKeyDown(KeyCode.E)) {
      CollectItem();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (collected) return;

    playerInventory = other.GetComponentInParent<PlayerInventory>();
    if (playerInventory != null) {
      playerInRange = true;

      hudManager = HudUIManager.Instance;
      hudManager.ShowInteractKey(transform);
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.GetComponentInParent<PlayerInventory>() != null) {
      playerInRange = false;

      hudManager = HudUIManager.Instance;
      hudManager.HideInteractKey();
    }
  }

  private void CollectItem() {
    collected = true;
    playerInventory.AddItem(item);
    hudManager.HideInteractKey();
    Destroy(gameObject);
  }

  void OnDrawGizmos() {
    // draw red line to show the pickup range
    Collider2D collider = GetComponent<Collider2D>();
    if (collider != null) {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(collider.bounds.center, collider.bounds.extents.y);
      Gizmos.color = Color.cyan;
      Gizmos.DrawWireSphere(collider.bounds.center, collider.bounds.extents.y / 2 / 2);
    }
  }
}
