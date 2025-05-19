using UnityEngine;

public class ScreenShake : MonoBehaviour {
  public static ScreenShake Instance;

  private Vector3 originalPosition;
  private float shakeDuration = 0f;
  private float shakeMagnitude = 0.1f;
  private float dampingSpeed = 1.0f;

  void Awake() {
    if (Instance == null) Instance = this;
    else Destroy(gameObject);

    originalPosition = transform.localPosition;
  }

  void Update() {
    if (shakeDuration > 0) {
      transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
      transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, originalPosition.z);
      shakeDuration -= Time.deltaTime * dampingSpeed;
    }
    else {
      shakeDuration = 0f;
      transform.localPosition = originalPosition;
    }
  }

  public void Shake(float duration, float magnitude) {
    shakeDuration = duration;
    shakeMagnitude = magnitude;
  }
}
