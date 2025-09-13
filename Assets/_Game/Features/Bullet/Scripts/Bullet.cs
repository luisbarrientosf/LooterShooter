using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed = 10f;
  public float lifetime = 2f;

  private Vector2 direction;
  private float lifeTimer;
  private ObjectPool pool;
  private bool hasHit = false;

  public void Init(Vector2 shootDir, ObjectPool bulletPool) {
    direction = shootDir.normalized;
    lifeTimer = lifetime;
    pool = bulletPool;
    // gameObject.SetActive(true);
  }

  void Update() {
    transform.Translate(direction * speed * Time.deltaTime);

    lifeTimer -= Time.deltaTime;
    if (lifeTimer <= 0f) {
      ReturnToPool();
      // Debug.Log("Bullet returned to pool due to timeout.");
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (!gameObject.activeInHierarchy) return;
    if (hasHit) return;

    if (other.CompareTag("Enemy")) {
      hasHit = true;
      var enemy = other.GetComponentInParent<Enemy>();
      if (enemy != null) {
        enemy.TakeDamage(3); // or any damage value
      }
      else {
        // Debug.LogWarning("Enemy script not found on the object.");
      }

      ReturnToPool();
      // Debug.Log("Bullet hit an enemy.");
    }
    else if (other.CompareTag("Wall")) {
      hasHit = true;
      ReturnToPool();
      // Debug.Log("Bullet hit a wall.");
    }
  }

  void ReturnToPool() {
    gameObject.SetActive(false);
    hasHit = false;
    lifeTimer = lifetime;
    pool.Return(gameObject);
    // Debug.Log("Bullet returned to pool.");
  }
}