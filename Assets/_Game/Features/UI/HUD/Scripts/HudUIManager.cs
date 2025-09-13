using TMPro;
using UnityEngine;

public class HudUIManager : MonoBehaviour {
  public static HudUIManager Instance;
  public TextMeshProUGUI text;
  private PlayerHealth playerHealth;
  private PlayerInventory playerInventory;
  public TextMeshProUGUI coinText;
  public GameObject healthBar;
  public GameObject healthBarFill;
  public GameObject interactKey;
  private Transform itemInteractTransform;

  void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(gameObject);
    }

    playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();
    playerInventory = GameManager.Instance.player.GetComponent<PlayerInventory>();
    CheckHudUIManager();
  }

  void Start() {
    interactKey.SetActive(false);
    text.text = "Health: " + playerHealth.GetCurrentHealth().ToString() + " / Coins: " + playerInventory.GetCoinCount().ToString();
  }

  void Update() {
    if (!CheckHudUIManager()) return;

    text.text = "Health: " + playerHealth.GetCurrentHealth().ToString() + " / Coins: " + playerInventory.GetCoinCount().ToString();
    coinText.text = playerInventory.GetCoinCount().ToString();

    float healthPercent = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
    Vector3 currentScale = healthBarFill.transform.localScale;
    float smoothedX = Mathf.Lerp(currentScale.x, healthPercent, Time.deltaTime * 5f);
    healthBarFill.transform.localScale = new Vector3(smoothedX, 1f, 1f);

    // Update interact key position each frame if it's active
    if (interactKey.activeSelf && itemInteractTransform != null) {
      Vector3 offset = new Vector3(0, itemInteractTransform.GetComponent<Collider2D>().bounds.extents.y / 1.75f, 0);
      Vector3 screenPos = Camera.main.WorldToScreenPoint(itemInteractTransform.position + offset);
      interactKey.transform.position = screenPos;
    }
  }

  public void ShowInteractKey(Transform itemTransform) {
    itemInteractTransform = itemTransform;
    Vector3 offset = new Vector3(0, itemTransform.GetComponent<Collider2D>().bounds.extents.y / 1.75f, 0);
    Vector3 screenPos = Camera.main.WorldToScreenPoint(itemTransform.position + offset);
    interactKey.transform.position = screenPos;
    interactKey.SetActive(true);
  }

  public void HideInteractKey() {
    interactKey.SetActive(false);
    itemInteractTransform = null;
  }

  bool CheckHudUIManager() {
    bool isValid = true;
    if (text == null) {
      Debug.LogError("TextMeshPro component not found.");
      isValid = false;
    }
    if (playerHealth == null) {
      Debug.LogError("PlayerHealth component not found on player.");
      isValid = false;
    }
    if (playerInventory == null) {
      Debug.LogError("PlayerInventory component not found on player.");
      isValid = false;
    }
    if (coinText == null) {
      Debug.LogError("Coin TextMeshPro component not found.");
      isValid = false;
    }
    if (healthBar == null) {
      Debug.LogError("Health Bar GameObject not found.");
      isValid = false;
    }
    if (healthBarFill == null) {
      Debug.LogError("Health Bar Fill GameObject not found.");
      isValid = false;
    }
    if (GameManager.Instance == null) {
      Debug.LogError("GameManager instance is missing.");
      isValid = false;
    }
    return isValid;
  }
}
