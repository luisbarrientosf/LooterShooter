using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyFollow : MonoBehaviour {
  public float speed = 2f;
  private Transform player;
  private Rigidbody2D rb;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    PlayerController playerController = FindFirstObjectByType<PlayerController>();
    if (playerController != null) {
      player = playerController.transform;
    }
  }

  void FixedUpdate() {
    if (player == null) return;

    Vector2 direction = (player.position - transform.position).normalized;
    rb.linearVelocity = direction * speed;
  }
}
