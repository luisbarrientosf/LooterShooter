using UnityEngine;

public class CoinMagnetTrailHandler : MonoBehaviour {
  public GameObject magnetTrailEffect;

  private GameObject activeTrail;

  public void StartTrail(Transform parent) {
    if (magnetTrailEffect != null && activeTrail == null) {
      activeTrail = Instantiate(magnetTrailEffect, parent.position, Quaternion.identity, parent);
      if (activeTrail.TryGetComponent<ParticleSystem>(out var particles)) {
        particles.Play();
      }
    }
  }

  public void UpdateTrailPosition(Transform target) {
    if (activeTrail != null) {
      activeTrail.transform.position = target.position;
    }
  }

  public void StopTrail() {
    if (activeTrail != null) {
      if (activeTrail.TryGetComponent<ParticleSystem>(out var particles)) {
        particles.Stop(true);
      }
      Destroy(activeTrail, 0.5f); // optional fade-out before destruction
      activeTrail = null;
    }
  }

  public void ResetTrail() {
    if (activeTrail != null) {
      Destroy(activeTrail);
      activeTrail = null;
    }
  }
}
