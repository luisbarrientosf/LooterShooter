using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour {
  public TextMeshProUGUI text;
  private PlayerHealth playerHealth;
  private PlayerInventory playerInventory;
  public TextMeshProUGUI coinText;
  public GameObject healthBar;
  public GameObject healthBarFill;

  void Start() {
    if (text == null) Debug.LogError("TextMeshPro component not found.");

    playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();
    if (playerHealth == null) Debug.LogError("PlayerHealth component not found on player.");

    playerInventory = GameManager.Instance.player.GetComponent<PlayerInventory>();
    if (playerInventory == null) Debug.LogError("PlayerInventory component not found on player.");

    text.text = "Health: " + playerHealth.GetCurrentHealth().ToString() + " / Coins: " + playerInventory.GetCoinCount().ToString();
  }

  void Update() {
    if (text == null || playerHealth == null || playerInventory == null) return;
    if (healthBar == null || healthBarFill == null) return;
    text.text = "Health: " + playerHealth.GetCurrentHealth().ToString() + " / Coins: " + playerInventory.GetCoinCount().ToString();
    coinText.text = playerInventory.GetCoinCount().ToString();

    float healthPercent = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
    Vector3 currentScale = healthBarFill.transform.localScale;
    float smoothedX = Mathf.Lerp(currentScale.x, healthPercent, Time.deltaTime * 5f);
    healthBarFill.transform.localScale = new Vector3(smoothedX, 1f, 1f);
  }
}
