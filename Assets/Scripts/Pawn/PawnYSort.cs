using UnityEngine;

public class PlayerYSort : MonoBehaviour {
  public SpriteRenderer bodyBorderRenderer;
  public SpriteRenderer bodyRenderer;
  public SpriteRenderer headBorderRenderer;
  public SpriteRenderer headRenderer;
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer hatRenderer;
  public SpriteRenderer shadowRenderer;

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

    if (shadowRenderer) shadowRenderer.sortingOrder = baseOrder;
    if (bodyBorderRenderer) bodyBorderRenderer.sortingOrder = baseOrder + 1;
    if (bodyRenderer) bodyRenderer.sortingOrder = baseOrder + 2;
    if (headBorderRenderer) headBorderRenderer.sortingOrder = baseOrder + 3;
    if (headRenderer) headRenderer.sortingOrder = baseOrder + 4;
    if (leftEyeRenderer) leftEyeRenderer.sortingOrder = baseOrder + 5;
    if (rightEyeRenderer) rightEyeRenderer.sortingOrder = baseOrder + 5;
    if (hatRenderer) hatRenderer.sortingOrder = baseOrder + 6;
  }
}