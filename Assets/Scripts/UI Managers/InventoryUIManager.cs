using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {
  public static InventoryUIManager Instance;

  [Header("UI References")]
  public GameObject inventoryPanel;
  public GameObject slotPrefab;
  public Transform gridParent;
  public GameObject title;

  private PlayerInventory inventory;

  void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
    DontDestroyOnLoad(inventoryPanel);
    DontDestroyOnLoad(slotPrefab);
    DontDestroyOnLoad(gridParent);
    DontDestroyOnLoad(title);
    inventoryPanel.SetActive(false);
    if (title != null) {
      title.SetActive(false);
    }
  }

  void Start() {
    inventory = GameManager.Instance.player.GetComponent<PlayerInventory>();
    if (inventory == null) {
      Debug.LogError("PlayerInventory component not found on player.");
      return;
    }
    if (inventoryPanel == null) {
      Debug.LogError("Inventory panel not assigned.");
      return;
    }
    if (slotPrefab == null) {
      Debug.LogError("Slot prefab not assigned.");
      return;
    }
    if (gridParent == null) {
      Debug.LogError("Grid parent not assigned.");
      return;
    }
  }

  public void ShowInventory() {
    if (inventoryPanel == null) return;

    inventoryPanel.SetActive(true);
    if (title != null) {
      title.SetActive(true);
    }
    RefreshUI();
  }

  public void HideInventory() {
    if (inventoryPanel == null) return;

    inventoryPanel.SetActive(false);
    if (title != null) {
      title.SetActive(false);
    }
  }

  void Update() {
    if (GameManager.Instance.IsGameOver()) return;

    if (Input.GetKeyDown(KeyCode.I)) {
      ToggleInventory();
    }
  }

  public void ToggleInventory() {
    if (inventoryPanel == null) return;

    bool isActive = !inventoryPanel.activeSelf;
    inventoryPanel.SetActive(isActive);
    if (title != null) {
      title.SetActive(isActive);
    }

    if (isActive)
      RefreshUI();
  }

  public void RefreshUI() {
    if (inventory == null) return;

    // Clear existing slots
    foreach (Transform child in gridParent) {
      Destroy(child.gameObject);
    }

    // Create one slot per item
    foreach (Item item in inventory.items) {
      GameObject slot = Instantiate(slotPrefab, gridParent);
      Image icon = slot.GetComponent<Image>();
      if (icon != null)
        icon.sprite = item.icon;
    }
  }
}
