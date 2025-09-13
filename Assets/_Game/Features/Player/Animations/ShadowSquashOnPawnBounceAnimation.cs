using UnityEngine;

public class ShadowSquashOnPawnBounceAnimation : MonoBehaviour {
  public PawnBounceAnimation pawnBounceAnimation; // Reference to your other script
  public float squashAmount = -1f; // How much to squash/stretch
  public float scaleSpeed = 25f;
  private Vector3 originalScale;

  void Start() {
    originalScale = transform.localScale;
  }

  void Update() {
    if (pawnBounceAnimation == null) return;

    float bounce = Mathf.Abs(Mathf.Sin(pawnBounceAnimation.bounceTimer));
    float scaleX = originalScale.x * (1f + squashAmount * (1f - bounce)); //  high bounce = smaller shadow
    Vector3 targetScale = new Vector3(scaleX, originalScale.y, originalScale.z);

    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
  }
}
