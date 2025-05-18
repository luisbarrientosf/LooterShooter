using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour {
  public static GameOverUIManager Instance;
  public GameObject panel;
  private GameManager gameManager;
  private InventoryUIManager inventoryUIManager;


  void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(gameObject);
    }
    gameManager = GameManager.Instance;
    inventoryUIManager = GameManager.Instance.inventoryUIManager;
    GameManager.Instance.gameOverUIManager = this;
    CheckGameOverUIManager();
  }

  void Start() {
    panel.SetActive(false);
  }

  public void Exit() {
    panel.SetActive(false);
    StartCoroutine(ExitToMainMenu());
  }

  public void Restart() {
    panel.SetActive(false);
    if (gameManager.isTestGame) {
      gameManager.StartTestGame();
    }
    else {
      gameManager.StartGame();
    }
  }

  private IEnumerator ExitToMainMenu() {
    AsyncOperation mainMenuLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu, LoadSceneMode.Additive);
    while (!mainMenuLoad.isDone)
      yield return null;

    string currentScene = gameManager.isTestGame ? Scenes.TestScene : Scenes.Gameplay;
    SceneManager.UnloadSceneAsync(Scenes.PauseMenu);
    SceneManager.UnloadSceneAsync(Scenes.GameOver);
    SceneManager.UnloadSceneAsync(Scenes.HUD);
    SceneManager.UnloadSceneAsync(Scenes.Inventory);
    SceneManager.UnloadSceneAsync(currentScene);
  }

  public void Show() {
    if (!CheckGameOverUIManager()) return;
    inventoryUIManager.HideInventory();

    panel.SetActive(true);
  }

  private bool CheckGameOverUIManager() {
    if (panel == null) {
      Debug.LogError("Game Over UI panel is not assigned.");
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
