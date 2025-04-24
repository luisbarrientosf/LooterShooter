using UnityEngine;

public class SpriteYSort : MonoBehaviour {
  public SpriteRenderer spriteRenderer;
  public float sortingScale = 100f;

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    float currentY = transform.position.y;
    if (Mathf.Approximately(currentY, lastY)) return;

    lastY = currentY;
    int baseOrder = -(int)(currentY * sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;

    if (spriteRenderer) spriteRenderer.sortingOrder = baseOrder;

  }
}
