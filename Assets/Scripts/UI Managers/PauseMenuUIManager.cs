using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {
  public GameObject panel;
  private GameManager gameManager;
  private InventoryUIManager inventoryUIManager;

  void Awake() {
    gameManager = GameManager.Instance;
    inventoryUIManager = InventoryUIManager.Instance;
    CheckPauseMenu();
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
    if (isActive) {
      inventoryUIManager.HideInventory();
    }
  }


  public void Exit() {
    if (!CheckPauseMenu()) return;

    panel.SetActive(false);
    StartCoroutine(ExitToMainMenu());
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

  private IEnumerator ExitToMainMenu() {
    AsyncOperation mainMenuLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu, LoadSceneMode.Additive);
    while (!mainMenuLoad.isDone)
      yield return null;

    string currentScene = gameManager.isTestGame ? Scenes.TestScene : Scenes.Gameplay;
    SceneManager.UnloadSceneAsync(currentScene);
    SceneManager.UnloadSceneAsync(Scenes.PauseMenu);
    SceneManager.UnloadSceneAsync(Scenes.GameOver);
    SceneManager.UnloadSceneAsync(Scenes.Inventory);
    SceneManager.UnloadSceneAsync(Scenes.HUD);
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
    if (inventoryUIManager == null) {
      Debug.LogError("InventoryUIManager instance is null.");
      return false;
    }

    return true;
  }
}
