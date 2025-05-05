using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour {
  public float floatSpeed = 1f;
  public float fadeDuration = 1f;
  public float scaleUpAmount = 1.2f;

  private TextMeshPro text;
  private Color originalColor;
  private float timer = 0f;
  private Vector3 originalScale;
  private ObjectPool pool;
  private Transform followTarget;
  private Vector3 offset = Vector3.up * 1f;

  void Awake() {
    text = GetComponent<TextMeshPro>();
    text.sortingLayerID = SortingLayer.NameToID("In Game UI");
    originalColor = text.color;
    originalScale = transform.localScale;
    transform.forward = Camera.main.transform.forward;
  }

  public void Initialize(ObjectPool pool, Transform reference, int damage) {
    this.pool = pool;
    this.followTarget = reference;
    text.text = damage.ToString();
    timer = 0f;

    transform.position = reference.position + offset;
  }

  void Update() {
    if (pool == null) return;
    timer += Time.deltaTime;

    // Follow the reference (world-space, floating upward over time)
    if (followTarget != null) {
      transform.position = followTarget.position + offset + Vector3.up * (floatSpeed * timer);
    }

    // Fade out
    float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
    text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

    // Scale up slightly
    float scale = Mathf.Lerp(1f, scaleUpAmount, timer / fadeDuration);
    transform.localScale = originalScale * scale;

    if (timer >= fadeDuration) {
      followTarget = null;
      pool.Return(gameObject);
    }
  }
}
