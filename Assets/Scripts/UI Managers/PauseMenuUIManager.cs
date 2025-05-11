using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {
  public GameObject panel;
  private GameManager gameManager;
  //private InventoryUIManager inventoryUIManager;

  void Awake() {
    gameManager = GameManager.Instance;
    //inventoryUIManager = InventoryUIManager.Instance;
  }

  void Start() {
    if (!CheckPauseMenu()) return;

    panel.SetActive(false);
  }

  void Update() {
    if (!CheckPauseMenu()) return;
    if (gameManager.IsGameOver()) return;

    if (Input.GetKeyDown(KeyCode.Escape)) {
      TogglePauseMenu();
    }
  }

  public void TogglePauseMenu() {
    if (!CheckPauseMenu()) return;

    bool isActive = !panel.activeSelf;
    panel.SetActive(isActive);
    gameManager.SetIsGamePaused(isActive);
  }


  public void Exit() {
    if (!CheckPauseMenu()) return;


    panel.SetActive(false);
    // inventoryUIManager.HideInventory();
    SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    SceneManager.UnloadSceneAsync("Gameplay");
    SceneManager.UnloadSceneAsync("Pause Menu");
    SceneManager.UnloadSceneAsync("You Died");
  }

  public void Continue() {
    if (!CheckPauseMenu()) return;

    panel.SetActive(false);
    gameManager.SetIsGamePaused(false);
  }

  public void Options() {
    // Load the options scene
    //SceneManager.LoadScene("OptionsScene");
  }

  private bool CheckPauseMenu() {
    if (panel == null) {
      Debug.LogError("Pause menu panel is not assigned.");
      return false;
    }
    if (gameManager == null) {
      Debug.LogError("GameManager instance is null.");
      return false;
    }

    return true;
  }
}
