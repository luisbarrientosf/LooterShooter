using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour {
  public void Exit() {
    Application.Quit();
  }

  public void Play() {
    StartCoroutine(LoadScenes());
  }

  public void Options() {
    //SceneManager.LoadScene("OptionsScene");
  }

  IEnumerator LoadScenes() {
    string targetScene = GameManager.Instance.isTestGame ? "TestScene" : "Gameplay";
    SceneLoader.targetScene = targetScene;
    AsyncOperation loadingLoad = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
    while (!loadingLoad.isDone)
      yield return null;
    SceneManager.UnloadSceneAsync("Main Menu");
    SceneManager.UnloadSceneAsync("Loading");
  }
}
