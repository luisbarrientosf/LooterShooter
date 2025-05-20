using UnityEngine;

public class SpriteYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;


  public float sortingScale = 1000f; // match player

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    if (!spriteRenderer) return;
    Collider2D collider2D = GetComponent<Collider2D>();
    if (!collider2D) {
      float bottomY = spriteRenderer.bounds.min.y;
      if (Mathf.Approximately(bottomY, lastY)) return;

      lastY = bottomY;
      int baseOrder = YSortUtils.GetBaseSortingOrder(spriteRenderer.bounds, sortingScale);
      if (baseOrder == lastBaseOrder) return;

      lastBaseOrder = baseOrder;
      spriteRenderer.sortingOrder = baseOrder;
    }
    else {
      // use collider2D center
      float centerY = collider2D.bounds.min.y;
      if (Mathf.Approximately(centerY, lastY)) return;

      lastY = centerY;
      int baseOrder = YSortUtils.GetBaseSortingOrder(collider2D.bounds, sortingScale);
      if (baseOrder == lastBaseOrder) return;

      lastBaseOrder = baseOrder;
      spriteRenderer.sortingOrder = baseOrder;
    }
  }
}