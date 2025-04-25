using UnityEngine;

public class CoinSpin : MonoBehaviour {
  public float spinSpeed = 3f;
  public float spinTransitionSpeed = 2f;
  public float magnetizedSpinMultiplier = 2.5f;

  private float maxScaleX;
  private float spinTimer = 0f;
  private float currentSpinMultiplier = 1f;
  private float normalSpinMultiplier = 1f;
  private bool isMagnetized = false;

  void Start() {
    maxScaleX = transform.localScale.x;
  }

  void Update() {
    Spin();
  }

  public void SetMagnetized(bool state) {
    isMagnetized = state;
  }

  void Spin() {
    float targetMultiplier = isMagnetized ? magnetizedSpinMultiplier : normalSpinMultiplier;
    currentSpinMultiplier = Mathf.MoveTowards(currentSpinMultiplier, targetMultiplier, spinTransitionSpeed * Time.deltaTime);
    spinTimer += Time.deltaTime * spinSpeed * currentSpinMultiplier;

    float t = Mathf.PingPong(spinTimer, 1f);
    float scaleX = Mathf.Lerp(0f, maxScaleX, t);
    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
  }
}