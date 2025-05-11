using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour {
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void Exit() {
    Application.Quit();
  }

  public void Play() {
    // Load the game scene
    SceneLoader.targetScene = "Gameplay";
    SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    SceneManager.UnloadSceneAsync("Main Menu");
  }

  public void Options() {
    // Load the options scene
    //SceneManager.LoadScene("OptionsScene");
  }
}
