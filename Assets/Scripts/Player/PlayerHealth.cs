using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int maxHealth = 100;
  private int currentHealth;
  public GameObject damageTextPrefab;

  void Awake() {
    currentHealth = maxHealth;
  }

  public void TakeDamage(int amount) {

    ShowDamageText(amount);
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
