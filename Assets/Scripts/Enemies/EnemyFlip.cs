using UnityEngine;

public class EnemyFlip : MonoBehaviour {
  public Transform player;

  void Awake() {
    PlayerController playerController = FindFirstObjectByType<PlayerController>();
    if (playerController != null) {
      player = playerController.transform;
    }
  }

  void FixedUpdate() {
    if (player == null) return;

    Vector3 scale = transform.localScale;
    if (transform.position.x < player.position.x) {
      scale.x = -Mathf.Abs(scale.x);
    }
    else {
      scale.x = Mathf.Abs(scale.x);
    }
    transform.localScale = scale;
  }
}
