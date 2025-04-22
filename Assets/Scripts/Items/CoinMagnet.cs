using System.Collections;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {
  public float magnetRange = 3f;
  public float magnetSpeed = 3f;
  public float slowdownRate = 3.5f;
  public GameObject magnetTrailEffect;

  private Transform player;
  private GameObject activeTrail;
  private bool isMagnetized = false;
  private float currentSpeed = 0f;
  private Vector3 lastDirection = Vector3.zero;

  void Start() {
    PlayerController playerController = FindFirstObjectByType<PlayerController>();
    if (playerController != null) {
      player = playerController.transform;
    }
  }

  void Update() {
    if (player == null) return;

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
