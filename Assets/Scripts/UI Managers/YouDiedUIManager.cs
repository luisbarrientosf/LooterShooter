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

  public void Show() {
    if (!CheckYouDiedUI()) return;

    panel.SetActive(true);
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
  }

  public void Restart() {
    if (!CheckYouDiedUI()) return;

    panel.SetActive(false);
    gameManager.StartGame();
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
