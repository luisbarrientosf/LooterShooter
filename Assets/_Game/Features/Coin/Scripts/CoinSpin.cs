using UnityEngine;

public class CoinSpin : MonoBehaviour {
  public float spinSpeed = 3f;
  public float spinTransitionSpeed = 2f;
  public float magnetizedSpinMultiplier = 2.5f;

  private float spinTimer = 0f;
  private float currentSpinMultiplier = 1f;
  private float normalSpinMultiplier = 1f;
  private bool isMagnetized = false;

  private float flipAmount = 1f;
  private Vector3 baseScale;

  void Awake() {
    baseScale = transform.localScale;
  }

  void Update() {
    Spin();
  }

  void Spin() {
    float targetMultiplier = isMagnetized ? magnetizedSpinMultiplier : normalSpinMultiplier;
    currentSpinMultiplier = Mathf.MoveTowards(currentSpinMultiplier, targetMultiplier, spinTransitionSpeed * Time.deltaTime);

    spinTimer += Time.deltaTime * spinSpeed * currentSpinMultiplier;
    float t = Mathf.PingPong(spinTimer, 1f);
    float flipScaleX = Mathf.Lerp(0f, 1f, t) * flipAmount;

    transform.localScale = new Vector3(baseScale.x * flipScaleX, baseScale.y, baseScale.z);
  }

  public void SetMagnetized(bool state) {
    isMagnetized = state;
  }

  public void SetRandomFlipAmount(float min, float max) {
    flipAmount = Random.Range(min, max); // Like 0.5f to 1.2f
  }

  public void ResetSpin() {
    spinTimer = Random.Range(0f, 1f); // Desync flip phase
  }
}
