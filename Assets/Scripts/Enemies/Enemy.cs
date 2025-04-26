using UnityEngine;

public class Enemy : MonoBehaviour {
  public ObjectPool pool;
  public float maxHealth = 100f;
  private float currentHealth;

  public GameObject healthBar;
  public SpriteRenderer healthBarFill;

  private float visibleTimer = 0f;
  private float visibleDuration = 2f;

  private Vector3 originalScale;

  private float targetHealthPercent;
  private float currentHealthPercent;

  public float smoothSpeed = 5f;

  void Start() {
    currentHealth = maxHealth;
    healthBar.SetActive(false);
    originalScale = healthBarFill.transform.localScale;
    currentHealthPercent = 1f;
    targetHealthPercent = 1f;
  }

  void Update() {
    if (healthBar.activeSelf) {
      visibleTimer -= Time.deltaTime;
      if (visibleTimer <= 0f) {
        healthBar.SetActive(false);
      }
    }

    currentHealthPercent = Mathf.Lerp(currentHealthPercent, targetHealthPercent, Time.deltaTime * smoothSpeed);
    healthBarFill.transform.localScale = new Vector3(currentHealthPercent * originalScale.x, originalScale.y, originalScale.z);
  }

  public void TakeDamage(float amount) {
    currentHealth -= amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    targetHealthPercent = currentHealth / maxHealth; // Set target value

    ShowHealthBarTemporarily();

    if (currentHealth <= 0f) {
      Die();
    }
  }

  private void ShowHealthBarTemporarily() {
    healthBar.SetActive(true);
    visibleTimer = visibleDuration;
  }

  private void Die() {
    currentHealth = maxHealth;
    targetHealthPercent = 1f;
    gameObject.SetActive(false);
    pool.Return(gameObject);
  }
}
