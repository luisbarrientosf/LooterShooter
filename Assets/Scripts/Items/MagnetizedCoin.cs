using UnityEngine;

[RequireComponent(typeof(CoinSpin))]
public class MagnetizedCoin : MonoBehaviour {
  public float magnetRange = 3f;
  public float magnetSpeed = 3f;
  public float slowdownRate = 3.5f;
  public GameObject magnetTrailEffect;

  private Transform player;
  private CoinSpin coinSpin;
  private ObjectPool pool;
  private bool isMagnetized = false;
  private float currentSpeed = 0f;
  private Vector3 lastDirection = Vector3.zero;
  private CoinMagnetTrailHandler trailHandler;

  void OnEnable() {
    if (player == null) player = FindFirstObjectByType<PlayerController>()?.transform;
    if (coinSpin == null) coinSpin = GetComponent<CoinSpin>();
    if (pool == null) pool = FindFirstObjectByType<ObjectPool>();
    if (trailHandler == null) trailHandler = GetComponent<CoinMagnetTrailHandler>();

    isMagnetized = false;
    currentSpeed = 0f;
    lastDirection = Vector3.zero;
    coinSpin?.SetMagnetized(false);
    trailHandler?.ResetTrail();
  }

  void Update() {
    if (player == null) return;

    float distance = Vector2.Distance(transform.position, player.position);

    if (distance < magnetRange) {
      lastDirection = (player.position - transform.position).normalized;
      if (!isMagnetized) {
        currentSpeed = magnetSpeed;
        isMagnetized = true;
        coinSpin?.SetMagnetized(true);
        trailHandler?.StartTrail(transform);
      }
    }
    else if (isMagnetized) {
      currentSpeed = Mathf.Max(0, currentSpeed - slowdownRate * Time.deltaTime);
      if (currentSpeed < 0.1f) {
        isMagnetized = false;
        coinSpin?.SetMagnetized(false);
        trailHandler?.StopTrail();
      }
    }

    if (isMagnetized && currentSpeed > 0f) {
      transform.position += currentSpeed * Time.deltaTime * lastDirection;
    }

    if (distance < 0.1f) {
      trailHandler?.StopTrail();
      player.GetComponent<PlayerInventory>().AddCoin();
      pool.Return(gameObject);
    }

    trailHandler?.UpdateTrailPosition(transform);
  }
}