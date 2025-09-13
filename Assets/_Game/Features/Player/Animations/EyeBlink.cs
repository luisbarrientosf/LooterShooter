using UnityEngine;
using System.Collections;

public class EyeBlink : MonoBehaviour {
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer rightEyeRenderer;
  public Sprite closedLeftEye;
  public Sprite closedRightEye;

  public float blinkIntervalMin = 2f;
  public float blinkIntervalMax = 5f;
  public float blinkDuration = 0.1f;

  void Start() {
    StartCoroutine(BlinkRoutine());
  }

  IEnumerator BlinkRoutine() {
    while (true) {
      float waitTime = Random.Range(blinkIntervalMin, blinkIntervalMax);
      yield return new WaitForSeconds(waitTime);
      // Blink
      Sprite openLeftEye = leftEyeRenderer.sprite;
      Sprite openRightEye = rightEyeRenderer.sprite;
      leftEyeRenderer.sprite = closedLeftEye;
      rightEyeRenderer.sprite = closedRightEye;
      yield return new WaitForSeconds(blinkDuration);
      leftEyeRenderer.sprite = openLeftEye;
      rightEyeRenderer.sprite = openRightEye;
    }
  }
}
