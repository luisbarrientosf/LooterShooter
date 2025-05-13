using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedUIManager : MonoBehaviour {
  public static YouDiedUIManager Instance;
  public GameObject panel;
  private GameManager gameManager;


  void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(gameObject);
    }
    gameManager = GameManager.Instance;
  }

  void Start() {
    if (!CheckYouDiedUI()) return;

    panel.SetActive(false);
  }

  public void Exit() {
    if (!CheckYouDiedUI()) return;

    panel.SetActive(false);

    StartCoroutine(ExitToMainMenu());

  }

  public void Restart() {
    if (!CheckYouDiedUI()) return;
    panel.SetActive(false);
    if (gameManager.isTestGame) {
      gameManager.StartTestGame();
    }
    else {
      gameManager.StartGame();
    }
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

  public void Show() {
    if (!CheckYouDiedUI()) return;
    InventoryUIManager.Instance.HideInventory();

    panel.SetActive(true);
  }

  private bool CheckYouDiedUI() {
    if (panel == null) {
      Debug.LogError("You Died UI panel is not assigned.");
      return false;
    }
    if (gameManager == null) {
      Debug.LogError("GameManager instance is null.");
      return false;
    }
    return true;
  }
}
