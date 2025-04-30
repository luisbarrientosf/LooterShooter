using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour {
  public TextMeshProUGUI text;
  private PlayerHealth playerHealth;
  private PlayerInventory playerInventory;

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
    text.text = "Health: " + playerHealth.GetCurrentHealth().ToString() + " / Coins: " + playerInventory.GetCoinCount().ToString();
  }
}
