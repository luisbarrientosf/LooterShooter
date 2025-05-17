using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
  public static string targetScene = "Gameplay";

  void Start() {
    StartCoroutine(LoadGameScenes());
  }

  IEnumerator LoadGameScenes() {
    Debug.Log("Scene Loader> Loading Main Scene");
    AsyncOperation mainLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
    while (!mainLoad.isDone)
      yield return null;

    if (!GameManager.Instance.isTestGame) {

      Debug.Log("Scene Loader> Loading Inventory");
      AsyncOperation inventoryLoad = SceneManager.LoadSceneAsync(Scenes.Inventory, LoadSceneMode.Additive);
      while (!inventoryLoad.isDone)
        yield return null;

      Debug.Log("Scene Loader> Loading Pause Menu");
      AsyncOperation pauseMenuLoad = SceneManager.LoadSceneAsync(Scenes.PauseMenu, LoadSceneMode.Additive);
      while (!pauseMenuLoad.isDone)
        yield return null;

      Debug.Log("Scene Loader> Loading You Died Menu");
      AsyncOperation youDiedMenuLoad = SceneManager.LoadSceneAsync(Scenes.YouDied, LoadSceneMode.Additive);
      while (!youDiedMenuLoad.isDone)
        yield return null;

      Debug.Log("Scene Loader> Loading HUD");
      AsyncOperation hudLoad = SceneManager.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive);
      while (!hudLoad.isDone)
        yield return null;
    }

    SceneManager.UnloadSceneAsync(Scenes.Loading);
  }
}
