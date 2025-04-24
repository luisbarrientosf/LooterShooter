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

  void LateUpdate() {
    int baseOrder = -(int)(transform.position.y * sortingScale);

    if (shadowRenderer != null)
      shadowRenderer.sortingOrder = baseOrder;

    if (bodyBorderRenderer != null)
      bodyBorderRenderer.sortingOrder = baseOrder + 1;

    if (bodyRenderer != null)
      bodyRenderer.sortingOrder = baseOrder + 2;

    if (headBorderRenderer != null)
      headBorderRenderer.sortingOrder = baseOrder + 3;

    if (headRenderer != null)
      headRenderer.sortingOrder = baseOrder + 4;

    if (leftEyeRenderer != null)
      leftEyeRenderer.sortingOrder = baseOrder + 5;

    if (rightEyeRenderer != null)
      rightEyeRenderer.sortingOrder = baseOrder + 5;

    if (hatRenderer != null)
      hatRenderer.sortingOrder = baseOrder + 6;
  }
}