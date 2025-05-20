using UnityEngine;

public class TreeInteractions : MonoBehaviour {
  public float slowMultiplier = 0.5f;
  private float originalSpeed;
  private float originalSprintSpeed;
  private bool isPlayerInside = false;

  void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("OnTriggerEnter2D called");
    Debug.Log("other: " + other.name);
    Debug.Log("other.tag: " + other.tag);
    if (!isPlayerInside && other.name == "FootCollider") {
      isPlayerInside = true;
      PlayerController playerController = other.GetComponentInParent<PlayerController>();
      Debug.Log("playerController: " + playerController);
      if (playerController != null) {
        Debug.Log("playerController.speed: " + playerController.speed);
        originalSpeed = playerController.speed;
        originalSprintSpeed = playerController.sprintSpeed;
        playerController.speed *= slowMultiplier;
        playerController.sprintSpeed *= slowMultiplier;
      }
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    Debug.Log("OnTriggerExit2D called");
    Debug.Log("other: " + other.name);
    if (other.name == "FootCollider") {
      isPlayerInside = false;
      PlayerController playerController = other.GetComponentInParent<PlayerController>();
      if (playerController != null) {
        playerController.speed = originalSpeed;
        playerController.sprintSpeed = originalSprintSpeed;
      }
    }
  }
}
