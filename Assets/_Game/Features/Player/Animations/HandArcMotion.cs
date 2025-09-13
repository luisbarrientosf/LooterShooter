using UnityEngine;
using System.Collections;

public class HandArcMotion : MonoBehaviour {
  public PawnBounceAnimation pawnBounceAnimation;
  public Transform followTarget;

  public float arcRadius = 0.5f;
  public float arcDuration = 1f;
  public float returnDuration = 0.5f;

  private float arcTime = 0f;
  private bool isReturning = false;

  void Update() {
    if (pawnBounceAnimation == null || followTarget == null) return;

    bool isWalking = pawnBounceAnimation.IsWalking();
    Vector3 basePosition = followTarget.position;

    if (isWalking) {
      if (isReturning) {
        StopAllCoroutines();
        isReturning = false;
      }

      // Ping-pong time from 0 to 1, then back
      arcTime += Time.deltaTime;
      float pingPong = Mathf.PingPong(arcTime / arcDuration, 1f);
      float angle = Mathf.PI + pingPong * Mathf.PI; // From π to 2π (bottom semi-circle)

      float x = arcRadius * Mathf.Cos(angle);
      float y = arcRadius * Mathf.Sin(angle);

      transform.position = basePosition + new Vector3(x, y, 0f);
    }
    else {
      if (!isReturning) {
        StartCoroutine(ReturnToBase());
      }
    }
  }

  IEnumerator ReturnToBase() {
    isReturning = true;
    Vector3 startPos = transform.position;
    float t = 0f;

    while (t < 1f) {
      t += Time.deltaTime / returnDuration;
      Vector3 updatedBase = followTarget != null ? followTarget.position : startPos;
      transform.position = Vector3.Lerp(startPos, updatedBase, t);
      yield return null;
    }

    if (followTarget != null)
      transform.position = followTarget.position;

    arcTime = 0f;
    isReturning = false;
  }
}
