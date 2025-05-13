using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour {
  public GameObject panel;
  private GameManager gameManager;

  void Awake() {
    gameManager = GameManager.Instance;
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
      InventoryUIManager.Instance.HideInventory();
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
    AsyncOperation mainMenuLoad = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Additive);
    while (!mainMenuLoad.isDone)
      yield return null;

    string currentScene = gameManager.isTestGame ? "TestScene" : "Gameplay";
    SceneManager.UnloadSceneAsync(currentScene);
    SceneManager.UnloadSceneAsync("Pause Menu");
    SceneManager.UnloadSceneAsync("You Died");
    SceneManager.UnloadSceneAsync("Inventory");
    SceneManager.UnloadSceneAsync("HUD");
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
