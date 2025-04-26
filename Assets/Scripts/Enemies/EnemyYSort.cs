using UnityEngine;

public class EnemyYSort : MonoBehaviour {
  public SpriteRenderer bodyBorderRenderer;
  public SpriteRenderer bodyRenderer;
  public SpriteRenderer headRenderer;
  public GameObject healthBar;
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

    if (bodyBorderRenderer) bodyBorderRenderer.sortingOrder = baseOrder;
    if (bodyRenderer) bodyRenderer.sortingOrder = baseOrder + 1;
    if (headRenderer) headRenderer.sortingOrder = baseOrder + 2;

    if (healthBar) {
      SpriteRenderer[] healthBarRenderers = healthBar.GetComponentsInChildren<SpriteRenderer>();
      if (healthBarRenderers.Length > 0) {
        foreach (SpriteRenderer healthBarRenderer in healthBarRenderers) {
          if (healthBarRenderer != null) {
            healthBarRenderer.sortingOrder = baseOrder + 3;
          }
        }
      }
    }
  }
}
