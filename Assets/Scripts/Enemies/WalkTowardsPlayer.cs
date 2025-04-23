using UnityEngine;

public class WalkTowardsPlayer : MonoBehaviour {
  public float speed = 2f;
  private Transform player;

  void Start() {
    PlayerController playerController = FindFirstObjectByType<PlayerController>();
    if (playerController != null) {
      player = playerController.transform;
    }
    else {
      Debug.LogWarning("Player not found. Make sure the PlayerController is in the scene.");
    }
  }

  void Update() {
    if (player == null) return;

    Vector2 direction = (player.position - transform.position).normalized;
    transform.position += (Vector3)(direction * speed * Time.deltaTime);
  }
}