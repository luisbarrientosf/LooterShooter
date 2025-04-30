using UnityEngine;

public class SpriteYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;
  public float sortingScale = 100f;

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    if (!spriteRenderer) return;

    float bottomY = spriteRenderer.bounds.min.y;
    if (Mathf.Approximately(bottomY, lastY)) return;

    lastY = bottomY;
    int baseOrder = -(int)(bottomY * sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;
    spriteRenderer.sortingOrder = baseOrder;
  }
}
