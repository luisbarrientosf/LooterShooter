using UnityEngine;

public class UIManager : MonoBehaviour {
  public static UIManager Instance;

  public GameObject inventoryPanel;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this.gameObject);
      return;
    }

    inventoryPanel.SetActive(false);

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.I)) {
      ToggleInventory();
    }
  }

  public void ToggleInventory() {
    if (inventoryPanel != null)
      inventoryPanel.SetActive(!inventoryPanel.activeSelf);
  }
}
