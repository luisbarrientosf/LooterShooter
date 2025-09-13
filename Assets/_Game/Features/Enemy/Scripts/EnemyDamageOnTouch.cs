using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour {
  public int damage = 10;
  public float damageCooldown = 1f;
  private float lastHitTime;

  void OnCollisionStay2D(Collision2D collision) {
    if (Time.time - lastHitTime < damageCooldown) return;

    if (collision.gameObject.CompareTag("Player")) {
      PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
      if (playerHealth != null) {
        playerHealth.TakeDamage(damage);
        lastHitTime = Time.time;
      }
    }
  }
}
