using UnityEngine;

public class CoinSpinAnimation : MonoBehaviour {
  public float spinSpeed = 3f;
  private float maxScaleX;

  void Start() {
    maxScaleX = transform.localScale.x;
  }

  void Update() {
    float t = Mathf.PingPong(Time.time * spinSpeed, 1f);
    float scaleX = Mathf.Lerp(0, maxScaleX, t);

    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    GetComponent<SpriteRenderer>().flipX = scaleX < 0;
  }
}
