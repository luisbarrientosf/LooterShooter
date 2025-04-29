using UnityEngine;

public class UIManager : MonoBehaviour {
  public static UIManager Instance;

  public GameObject inventoryPanel;
  public GameObject hudPanel;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(this.gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject); // optional if you want persistence
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
