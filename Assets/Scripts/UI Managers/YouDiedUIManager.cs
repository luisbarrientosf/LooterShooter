using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedUIManager : MonoBehaviour {
  public static YouDiedUIManager Instance;
  public GameObject panel;
  private GameManager gameManager;


  void Awake() {
    if (Instance == null) {
      Instance = this;
      //DontDestroyOnLoad(gameObject);
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
    SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    SceneManager.UnloadSceneAsync("Gameplay");
    SceneManager.UnloadSceneAsync("Pause Menu");
    SceneManager.UnloadSceneAsync("You Died");
    SceneManager.UnloadSceneAsync("HUD");
    SceneManager.UnloadSceneAsync("Inventory");
  }

  public void Restart() {
    if (!CheckYouDiedUI()) return;
    panel.SetActive(false);
    gameManager.StartGame();
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
