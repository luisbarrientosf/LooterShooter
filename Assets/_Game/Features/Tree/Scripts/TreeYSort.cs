using UnityEditor;
using UnityEngine;

public class TreeYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;


  public float sortingScale = 1000f; // match player

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    Collider2D collider2D = GetComponent<Collider2D>();
    float centerY = collider2D.bounds.min.y;
    if (Mathf.Approximately(centerY, lastY)) return;

    lastY = centerY;
    int baseOrder = YSortUtils.GetBaseSortingOrderTree(collider2D.bounds, sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;
    spriteRenderer.sortingOrder = baseOrder;
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Collider2D collider2D = GetComponent<Collider2D>();

    Gizmos.DrawWireCube(collider2D.bounds.center, collider2D.bounds.size);
    Handles.color = Color.white;
    Vector3 labelPosition = collider2D.bounds.extents.y * Vector3.down + collider2D.bounds.center;
    Handles.Label(labelPosition, lastBaseOrder.ToString());
  }
}