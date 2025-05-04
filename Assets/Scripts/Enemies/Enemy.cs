using UnityEngine;

public class Enemy : MonoBehaviour {
  public ObjectPool pool;
  public ObjectPool coinPool;
  public float maxHealth = 100f;
  private float currentHealth;

  public GameObject healthBar;
  public SpriteRenderer healthBarFill;
  public GameObject damageTextPrefab;

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

    targetHealthPercent = currentHealth / maxHealth;
    ShowDamageText((int)amount);
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

    DropCoins();
    // Reset health and deactivate the enemy
    currentHealth = maxHealth;
    targetHealthPercent = 1f;
    gameObject.SetActive(false);
    pool.Return(gameObject);
  }

  private void DropCoins() {
    int coinAmount = Random.Range(3, 8);
    float coinForce = 1f;
    for (int i = 0; i < coinAmount; i++) {
      GameObject coin = coinPool.Get();
      if (coin == null) continue;

      coin.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

      if (coin.TryGetComponent<CoinSpin>(out var coinSpin)) {
        coinSpin.ResetSpin();
      }

      coin.SetActive(true);

      Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
      if (rb != null) {
        Vector2 forceDir = Random.insideUnitCircle.normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(forceDir * coinForce, ForceMode2D.Impulse);
      }
    }
  }

  void ShowDamageText(int amount) {
    Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0); // above enemy
    GameObject damageText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

    var damageScript = damageText.GetComponent<DamageText>();
    damageScript.SetText(amount.ToString());

    // Face the camera (works with orthographic too)
    damageText.transform.forward = Camera.main.transform.forward;

    // Optional: adjust Z position if you need to sort above other objects
    Vector3 pos = damageText.transform.position;
    pos.z = transform.position.z - 1f; // slightly closer to camera
    damageText.transform.position = pos;
  }
}
