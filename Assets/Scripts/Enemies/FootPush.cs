using UnityEngine;

public class FootPush : MonoBehaviour {
  [Header("Push Settings")]
  public float pushRadius = 0.5f;
  public float pushForce = 3f;
  public LayerMask footLayer; // Set to "Foot" layer in Inspector

  private Rigidbody2D rb;

  void Awake() {
    // Get the root Rigidbody2D (enemy's main body)
    rb = GetComponentInParent<Rigidbody2D>();
  }

  void FixedUpdate() {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pushRadius, footLayer);

    foreach (Collider2D col in hits) {
      // Skip self
      if (col.attachedRigidbody == null || col.attachedRigidbody == rb)
        continue;

      Vector2 diff = (Vector2)rb.position - (Vector2)col.attachedRigidbody.position;
      float distance = diff.magnitude;

      if (distance == 0f)
        continue;

      float strength = Mathf.Clamp01(1f - (distance / pushRadius));
      Vector2 force = diff.normalized * pushForce * strength;

      rb.AddForce(force, ForceMode2D.Force);

      // Debug (optional)
      Debug.DrawLine(transform.position, rb.position + force, Color.green);
      Debug.Log($"Pushing {col.name} with force {force}");
    }
  }
}
