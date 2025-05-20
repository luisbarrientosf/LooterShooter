using UnityEngine;

public class SpriteYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;
  public float sortingScale = 1000f; // match player

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    if (!spriteRenderer) return;

    float bottomY = spriteRenderer.bounds.min.y;
    if (Mathf.Approximately(bottomY, lastY)) return;

    lastY = bottomY;
    int baseOrder = YSortUtils.GetBaseSortingOrder(spriteRenderer.bounds, sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;
    spriteRenderer.sortingOrder = baseOrder;
  }
}
