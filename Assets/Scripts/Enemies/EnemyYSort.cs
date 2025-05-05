using UnityEngine;

public class EnemyYSort : MonoBehaviour {
  public SpriteRenderer bodyBorderRenderer;
  public SpriteRenderer bodyRenderer;
  public SpriteRenderer headRenderer;
  public GameObject healthBar;
  public MeshRenderer damageTextMeshRenderer;
  public float sortingScale = 100f;

  private float lastY;
  private int lastBaseOrder;

  void LateUpdate() {

    Bounds bounds = new Bounds(transform.position, Vector3.zero);
    if (bodyRenderer != null) bounds.Encapsulate(bodyRenderer.bounds);
    if (bodyBorderRenderer != null) bounds.Encapsulate(bodyBorderRenderer.bounds);
    if (headRenderer != null) bounds.Encapsulate(headRenderer.bounds);
    if (damageTextMeshRenderer != null) bounds.Encapsulate(damageTextMeshRenderer.bounds);

    float bottomY = bounds.min.y; // This gives the bottom Y of the object

    if (Mathf.Approximately(bottomY, lastY)) return;

    lastY = bottomY;
    int baseOrder = -(int)(bottomY * sortingScale);
    if (baseOrder == lastBaseOrder) return;

    lastBaseOrder = baseOrder;

    if (bodyBorderRenderer) bodyBorderRenderer.sortingOrder = baseOrder;
    if (bodyRenderer) bodyRenderer.sortingOrder = baseOrder + 1;
    if (headRenderer) headRenderer.sortingOrder = baseOrder + 2;

    if (healthBar) {
      SpriteRenderer[] healthBarRenderers = healthBar.GetComponentsInChildren<SpriteRenderer>();
      foreach (SpriteRenderer healthBarRenderer in healthBarRenderers) {
        if (healthBarRenderer != null) {
          healthBarRenderer.sortingOrder = baseOrder + 3;
        }
      }
    }

    if (damageTextMeshRenderer) damageTextMeshRenderer.sortingOrder = baseOrder + 4;

  }
}
