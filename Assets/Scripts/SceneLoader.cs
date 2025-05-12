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

    AsyncOperation hudLoad = SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
    while (!hudLoad.isDone)
      yield return null;

    AsyncOperation inventoryLoad = SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Additive);
    while (!inventoryLoad.isDone)
      yield return null;

    AsyncOperation pauseMenuLoad = SceneManager.LoadSceneAsync("Pause Menu", LoadSceneMode.Additive);
    while (!pauseMenuLoad.isDone)
      yield return null;

    AsyncOperation youDiedMenuLoad = SceneManager.LoadSceneAsync("You Died", LoadSceneMode.Additive);
    while (!youDiedMenuLoad.isDone)
      yield return null;

    SceneManager.UnloadSceneAsync("Loading");
  }
}
