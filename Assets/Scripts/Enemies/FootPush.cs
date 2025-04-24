using UnityEngine;
using System.Collections.Generic;

public class FootPush : MonoBehaviour {
    public float pushRadius = 1.5f;
    public float pushForce = 10f;
    public float minPushDistance = 0.1f; 
    public LayerMask footLayer;

    private Rigidbody2D rb;

    // Reusable list to avoid allocations
    private static readonly List<Collider2D> hitList = new List<Collider2D>(15); // Grow as needed
private float minPushDistanceSqr;
private float pushRadiusSqr;

private static readonly ContactFilter2D contactFilter = new() {
    useLayerMask = true,
    useTriggers = false,
    layerMask = LayerMask.GetMask("EnemyFoot")
};

void Awake() {
    rb = GetComponentInParent<Rigidbody2D>();
    minPushDistanceSqr = minPushDistance * minPushDistance;
    pushRadiusSqr = pushRadius * pushRadius;
}

    void FixedUpdate() {
        // Clear the list before reuse
        hitList.Clear();

        Physics2D.OverlapCircle(transform.position, pushRadius, contactFilter, hitList);

        if (hitList.Count == 0) return;

        for (int i = 0; i < hitList.Count; i++) {
          Collider2D col = hitList[i];

          if (col.attachedRigidbody == null || col.attachedRigidbody == rb)
              continue;

          Vector2 diff = rb.position - col.attachedRigidbody.position;
          float sqrDistance = diff.sqrMagnitude;

          if (sqrDistance < minPushDistanceSqr || sqrDistance > pushRadiusSqr)
              continue;

          float strength = 1f - Mathf.Sqrt(sqrDistance) / pushRadius;
          Vector2 force = diff.normalized * pushForce * Mathf.Clamp01(strength);

          rb.AddForce(force, ForceMode2D.Force);
        }
    }
}