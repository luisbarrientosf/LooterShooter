using System.Collections;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {
  public float magnetRange = 3f;
  public float magnetSpeed = 3f;
  public float slowdownRate = 3.5f;
  public float spinSpeed = 3f;
  public GameObject magnetTrailEffect;

  private Transform player;
  private GameObject activeTrail;
  private bool isMagnetized = false;
  private float currentSpeed = 0f;
  private Vector3 lastDirection = Vector3.zero;

  private float maxScaleX;
  private float currentSpinMultiplier = 1f;
  private float normalSpinMultiplier = 1f;
  private float magnetizedSpinMultiplier = 2.2f;
  public float spinTransitionSpeed = 2f;
  private float spinTimer = 0f;

  void Start() {
    PlayerController playerController = FindFirstObjectByType<PlayerController>();
    if (playerController != null) {
      player = playerController.transform;
    }
    maxScaleX = transform.localScale.x;

  }

  void Update() {
    if (player == null) return;

    Spin();
    float distance = Vector2.Distance(transform.position, player.position);

    if (distance < magnetRange) {
      isMagnetized = true;
      lastDirection = (player.position - transform.position).normalized;
      currentSpeed = magnetSpeed;

      if (magnetTrailEffect != null && activeTrail == null) {
        AddParticlesWhenMoving();
      }
    }
    else if (isMagnetized) {
      currentSpeed = Mathf.Max(0, currentSpeed - slowdownRate * Time.deltaTime);
      if (currentSpeed < 0.1f && activeTrail != null) {
        activeTrail.GetComponent<ParticleSystem>().Stop(true);
        isMagnetized = false;
      }
    }

    if (isMagnetized && currentSpeed > 0f) {
      transform.position += currentSpeed * Time.deltaTime * lastDirection;
    }

    // Pick up
    if (distance < 0.2f) {
      if (activeTrail != null) {
        StartCoroutine(FadeOutAndDestroyTrail(activeTrail.GetComponent<ParticleSystem>(), 0.5f));
      }
      player.GetComponent<PlayerInventory>().AddCoin();
      Destroy(gameObject);
    }
  }

  void Spin() {
    float targetMultiplier = isMagnetized ? magnetizedSpinMultiplier : normalSpinMultiplier;
    currentSpinMultiplier = Mathf.MoveTowards(currentSpinMultiplier, targetMultiplier, spinTransitionSpeed * Time.deltaTime);
    spinTimer += Time.deltaTime * spinSpeed * currentSpinMultiplier;

    float t = Mathf.PingPong(spinTimer, 1f);
    float scaleX = Mathf.Lerp(0f, maxScaleX, t);
    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
  }

  void AddParticlesWhenMoving() {
    activeTrail = Instantiate(magnetTrailEffect, transform.position, Quaternion.identity, transform);
    if (activeTrail.TryGetComponent<ParticleSystem>(out var particles)) {
      particles.Play();
    }
  }

  IEnumerator FadeOutAndDestroyTrail(ParticleSystem particles, float delay) {
    if (particles == null) yield break;
    particles.Stop();
    yield return new WaitForSeconds(delay);
    Destroy(particles.gameObject);
  }
}
