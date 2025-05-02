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

  void Awake() {
    text = GetComponent<TextMeshPro>();
    originalColor = text.color;
    originalScale = transform.localScale;
  }

  public void SetText(string damage) {
    text.text = damage;
    timer = 0f;
  }

  void Update() {
    timer += Time.deltaTime;

    // Float upward
    transform.position += Vector3.up * floatSpeed * Time.deltaTime;

    // Fade out
    float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
    text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

    // Scale up slightly
    float scale = Mathf.Lerp(1f, scaleUpAmount, timer / fadeDuration);
    transform.localScale = originalScale * scale;

    // Destroy when done
    // if (timer >= fadeDuration)
    //   Destroy(gameObject);
  }
}
