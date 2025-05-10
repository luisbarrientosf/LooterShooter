using UnityEngine;

public class PauseMenuUIManager : MonoBehaviour {
  public GameObject panel;
  private GameManager gameManager;

  void Start() {
    if (panel != null) {
      panel.SetActive(false); // Ensure the pause menu is hidden at the start
    }
    gameManager = GameManager.Instance;
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      TogglePauseMenu();
    }
  }

  public void TogglePauseMenu() {
    if (panel == null) return;

    bool isActive = !panel.activeSelf;
    panel.SetActive(isActive);
    gameManager.SetIsGamePaused(isActive);
  }


  public void Exit() {
    Application.Quit();
  }

  public void Continue() {
    if (panel == null) return;

    panel.SetActive(false);
    gameManager.SetIsGamePaused(false);
  }

  public void Options() {
    // Load the options scene
    //SceneManager.LoadScene("OptionsScene");
  }
}
