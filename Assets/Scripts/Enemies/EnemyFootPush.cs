using UnityEngine;

public class EnemyFootPush : MonoBehaviour {
  public float pushStrength = 0.5f;
  private Transform rootTransform;

  void Awake() {
    rootTransform = transform.root;
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other == null || other == GetComponent<Collider2D>()) return;

    if (other.CompareTag("EnemyFoot")) {
      Transform otherRoot = other.transform.root;

      if (otherRoot != rootTransform) // Evita empujarse a sí mismo
      {
        Vector2 pushDirection = (rootTransform.position - otherRoot.position).normalized;
        rootTransform.position += (Vector3)(pushDirection * pushStrength * Time.deltaTime);
      }
    }
  }
}
