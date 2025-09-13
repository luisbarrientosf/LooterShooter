using UnityEditor;
using UnityEngine;

public class SpriteYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;


  public float sortingScale = 1000f; // match player

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {

    float centerY = spriteRenderer.bounds.min.y;
    if (Mathf.Approximately(centerY, lastY)) return;

    lastY = centerY;
    int baseOrder = YSortUtils.GetBaseSortingOrder(spriteRenderer.bounds, sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;
    spriteRenderer.sortingOrder = baseOrder;
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;

    Gizmos.DrawWireCube(spriteRenderer.bounds.center, spriteRenderer.bounds.size);
    Handles.color = Color.white;
    Vector3 labelPosition = spriteRenderer.bounds.extents.y * Vector3.down + spriteRenderer.bounds.center;
    Handles.Label(labelPosition, lastBaseOrder.ToString());
  }
}