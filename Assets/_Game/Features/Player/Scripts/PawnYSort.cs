using UnityEngine;

public class PlayerYSort : MonoBehaviour {
  public SpriteRenderer bodyRenderer;
  public SpriteRenderer headRenderer;
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer hatRenderer;
  public SpriteRenderer shadowRenderer;
  public SpriteRenderer leftHandRenderer;
  public SpriteRenderer rightHandRenderer;
  public MeshRenderer damageTextMeshRenderer;

  public float sortingScale = 100f;

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {
    Bounds bounds = new Bounds(transform.position, Vector3.zero);

    if (shadowRenderer) bounds.Encapsulate(shadowRenderer.bounds);
    if (bodyRenderer) bounds.Encapsulate(bodyRenderer.bounds);
    if (headRenderer) bounds.Encapsulate(headRenderer.bounds);
    if (leftEyeRenderer) bounds.Encapsulate(leftEyeRenderer.bounds);
    if (rightEyeRenderer) bounds.Encapsulate(rightEyeRenderer.bounds);
    if (hatRenderer) bounds.Encapsulate(hatRenderer.bounds);
    if (leftHandRenderer) bounds.Encapsulate(leftHandRenderer.bounds);
    if (rightHandRenderer) bounds.Encapsulate(rightHandRenderer.bounds);
    if (damageTextMeshRenderer) bounds.Encapsulate(damageTextMeshRenderer.bounds);

    float bottomY = bounds.min.y;
    if (Mathf.Approximately(bottomY, lastY)) return;

    lastY = bottomY;
    int baseOrder = YSortUtils.GetBaseSortingOrder(bounds, sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;

    if (shadowRenderer) shadowRenderer.sortingOrder = baseOrder;
    if (bodyRenderer) bodyRenderer.sortingOrder = baseOrder;
    if (headRenderer) headRenderer.sortingOrder = baseOrder;
    if (leftEyeRenderer) leftEyeRenderer.sortingOrder = baseOrder;
    if (rightEyeRenderer) rightEyeRenderer.sortingOrder = baseOrder;
    if (hatRenderer) hatRenderer.sortingOrder = baseOrder;
    if (damageTextMeshRenderer) damageTextMeshRenderer.sortingOrder = baseOrder;
    if (leftHandRenderer) leftHandRenderer.sortingOrder = baseOrder;
    if (rightHandRenderer) rightHandRenderer.sortingOrder = baseOrder;
  }
}
