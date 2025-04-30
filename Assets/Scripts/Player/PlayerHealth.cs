using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int maxHealth = 100;
  private int currentHealth;

  void Awake() {
    currentHealth = maxHealth;
  }

  public void TakeDamage(int amount) {
    currentHealth -= amount;
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

  void Die() {
    Debug.Log("Player died!");
    // Add death animation, disable movement, etc.
    gameObject.SetActive(false);
  }

  public int GetCurrentHealth() {
    return currentHealth;
  }

  public bool IsDead() {
    return currentHealth <= 0;
  }
}
