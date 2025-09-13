using UnityEngine;

public class CoinPickupOnCollision : MonoBehaviour {
  private Transform player;
  private ObjectPool pool;
  private CoinMagnetTrailHandler trailHandler;

  void OnEnable() {
    if (player == null) player = FindFirstObjectByType<PlayerController>()?.transform;
    if (pool == null) pool = FindFirstObjectByType<ObjectPool>();
    if (trailHandler == null) trailHandler = GetComponent<CoinMagnetTrailHandler>();

    trailHandler?.ResetTrail();
  }

  void OnTriggerEnter2D(Collider2D other) {
    var rb = other.attachedRigidbody;
    if (rb == null) return;

    var root = rb.gameObject;
    if (!root.CompareTag("Player")) return;

    trailHandler?.StopTrail();
    root.GetComponent<PlayerInventory>()?.AddCoin();
    pool.Return(gameObject);
  }
}