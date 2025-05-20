using UnityEngine;

public class TreeInteractions : MonoBehaviour {
  public float slowMultiplier = 0.5f;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      // animator.SetTrigger("Shake");

      //PlayerMovement player = other.GetComponent<PlayerMovement>();
      // if (player != null) {
      //   player.SetSpeedMultiplier(slowMultiplier);
      // }
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      // PlayerMovement player = other.GetComponent<PlayerMovement>();
      //   if (player != null) {
      //     player.SetSpeedMultiplier(1f);
      //   }
      // }
    }

  }
}
