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
  public SpriteRenderer leftHandRendererBorder;
  public SpriteRenderer leftHandRenderer;
  public SpriteRenderer rightHandRendererBorder;
  public SpriteRenderer rightHandRenderer;
  public MeshRenderer damageTextMeshRenderer;

  public float sortingScale = 100f;

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    Bounds bounds = new Bounds(transform.position, Vector3.zero);

    if (shadowRenderer) bounds.Encapsulate(shadowRenderer.bounds);
    if (bodyRenderer) bounds.Encapsulate(bodyRenderer.bounds);
    if (bodyBorderRenderer) bounds.Encapsulate(bodyBorderRenderer.bounds);
    if (headRenderer) bounds.Encapsulate(headRenderer.bounds);
    if (headBorderRenderer) bounds.Encapsulate(headBorderRenderer.bounds);
    if (leftEyeRenderer) bounds.Encapsulate(leftEyeRenderer.bounds);
    if (rightEyeRenderer) bounds.Encapsulate(rightEyeRenderer.bounds);
    if (hatRenderer) bounds.Encapsulate(hatRenderer.bounds);
    if (leftHandRenderer) bounds.Encapsulate(leftHandRenderer.bounds);
    if (leftHandRendererBorder) bounds.Encapsulate(leftHandRendererBorder.bounds);
    if (rightHandRenderer) bounds.Encapsulate(rightHandRenderer.bounds);
    if (rightHandRendererBorder) bounds.Encapsulate(rightHandRendererBorder.bounds);
    if (damageTextMeshRenderer) bounds.Encapsulate(damageTextMeshRenderer.bounds);

    float bottomY = bounds.min.y;
    if (Mathf.Approximately(bottomY, lastY)) return;

    lastY = bottomY;
    int baseOrder = -(int)(bottomY * sortingScale);
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
    if (damageTextMeshRenderer) damageTextMeshRenderer.sortingOrder = baseOrder + 7;
    if (leftHandRendererBorder) leftHandRendererBorder.sortingOrder = baseOrder + 8;
    if (leftHandRenderer) leftHandRenderer.sortingOrder = baseOrder + 9;
    if (rightHandRendererBorder) rightHandRendererBorder.sortingOrder = baseOrder + 8;
    if (rightHandRenderer) rightHandRenderer.sortingOrder = baseOrder + 9;
  }
}
