using UnityEngine;

public class EnemyYSort : MonoBehaviour {
  public SpriteRenderer bodyBorderRenderer;
  public SpriteRenderer bodyRenderer;
  public SpriteRenderer headRenderer;
  public float sortingScale = 100f;

  void LateUpdate() {
    int baseOrder = -(int)(transform.position.y * sortingScale);

    if (bodyBorderRenderer != null)
      bodyBorderRenderer.sortingOrder = baseOrder;

    if (bodyRenderer != null)
      bodyRenderer.sortingOrder = baseOrder + 1;

    if (headRenderer != null)
      headRenderer.sortingOrder = baseOrder + 2;
  }
}