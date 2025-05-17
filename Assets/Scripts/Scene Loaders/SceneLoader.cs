using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
  public static string targetScene = "Gameplay";

  void Start() {
    StartCoroutine(LoadGameScenes());
  }

  IEnumerator LoadGameScenes() {
    AsyncOperation mainLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
    while (!mainLoad.isDone)
      yield return null;

    if (!GameManager.Instance.isTestGame) {
      AsyncOperation hudLoad = SceneManager.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive);
      while (!hudLoad.isDone)
        yield return null;

      AsyncOperation inventoryLoad = SceneManager.LoadSceneAsync(Scenes.Inventory, LoadSceneMode.Additive);
      while (!inventoryLoad.isDone)
        yield return null;

      AsyncOperation pauseMenuLoad = SceneManager.LoadSceneAsync(Scenes.PauseMenu, LoadSceneMode.Additive);
      while (!pauseMenuLoad.isDone)
        yield return null;

      AsyncOperation youDiedMenuLoad = SceneManager.LoadSceneAsync(Scenes.YouDied, LoadSceneMode.Additive);
      while (!youDiedMenuLoad.isDone)
        yield return null;
    }

    SceneManager.UnloadSceneAsync(Scenes.Loading);
  }
}
