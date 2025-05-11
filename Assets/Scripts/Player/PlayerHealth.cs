using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public static PlayerHealth Instance;
  public int maxHealth = 100;
  private int currentHealth;
  public ObjectPool damageTextPool;
  private GameManager gameManager;

  private void Start() {
    gameManager = GameManager.Instance;
  }

  void Awake() {
    if (Instance == null) {
      Instance = this;
      // DontDestroyOnLoad(gameObject);
    }
    else {
      Destroy(gameObject);
    }
    currentHealth = maxHealth;
  }

  public void TakeDamage(int amount) {

    currentHealth -= amount;
    ShowDamageText(amount);
    if (currentHealth <= 0) {
      currentHealth = 0;
      Die();
    }
  }

  public void Heal(int amount) {
    currentHealth += amount;
    if (currentHealth > maxHealth) {
      currentHealth = maxHealth;
    }
  }

  public void ResetHealth() {
    currentHealth = maxHealth;
  }

  void Die() {
    Debug.Log("Player died!");
    // Add death animation, disable movement, etc.
    gameManager.SetIsGameOver(true);
    gameObject.SetActive(false);
  }

  public int GetCurrentHealth() {
    return currentHealth;
  }

  public bool IsDead() {
    return currentHealth <= 0;
  }

  void ShowDamageText(int amount) {
    GameObject instance = damageTextPool.Get();
    DamageText damageText = instance.GetComponent<DamageText>();
    damageText.Initialize(damageTextPool, transform, amount);
  }
}