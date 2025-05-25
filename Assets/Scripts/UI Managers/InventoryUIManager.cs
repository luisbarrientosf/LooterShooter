using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {
  public static InventoryUIManager Instance;

  public GameObject inventoryPanel;
  public GameObject slotPrefab;
  public GameObject itemPrefab;
  public Transform slotsGridParent;
  public GameObject title;

  private PlayerInventory inventory;

  void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    inventory = GameManager.Instance.player.GetComponent<PlayerInventory>();
    GameManager.Instance.inventoryUIManager = this;
    CheckInventoryUI();
  }

  void Start() {
    if (!CheckInventoryUI()) return;
    inventoryPanel.SetActive(false);
    title.SetActive(false);
  }

  public void ShowInventory() {
    if (!CheckInventoryUI()) return;

    RefreshUI();
    inventoryPanel.SetActive(true);
    title.SetActive(true);
  }

  public void HideInventory() {
    if (!CheckInventoryUI()) return;

    inventoryPanel.SetActive(false);
    title.SetActive(false);
  }

  void Update() {
    if (GameManager.Instance.IsGameOver()) return;
    if (GameManager.Instance.IsGamePaused()) return;

    if (Input.GetKeyDown(KeyCode.I)) {
      ToggleInventory();
    }
  }

  public void ToggleInventory() {
    if (!CheckInventoryUI()) return;

    bool isActive = !inventoryPanel.activeSelf;
    inventoryPanel.SetActive(isActive);
    title.SetActive(isActive);

    if (isActive) {
      RefreshUI();
    }
  }

  public void RefreshUI() {
    // Clear existing slots
    foreach (Transform child in slotsGridParent) {
      Destroy(child.gameObject);
    }

    // Create one slot per item
    foreach (Item item in inventory.items) {
      GameObject itemImage = Instantiate(slotPrefab, slotsGridParent);
      Image[] images = itemImage.GetComponentsInChildren<Image>();
      foreach (Image image in images) {
        if (image.gameObject != itemImage) {
          image.sprite = item.sprite;
        }
      }
    }
  }

  private bool CheckInventoryUI() {
    bool isValid = true;
    if (inventoryPanel == null) {
      Debug.LogError("Inventory panel is not assigned.");
      isValid = false;
    }
    if (slotPrefab == null) {
      Debug.LogError("Slot prefab is not assigned.");
      isValid = false;
    }
    if (slotsGridParent == null) {
      Debug.LogError("Slots Grid parent is not assigned.");

    }
    if (inventory == null) {
      Debug.LogError("PlayerInventory component is not assigned.");
      isValid = false;
    }
    return isValid;
  }
}
