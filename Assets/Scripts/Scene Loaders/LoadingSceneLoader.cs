using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneLoader : MonoBehaviour {
  public static string targetScene = "Gameplay";

  void Awake() {
    StartCoroutine(LoadGameScenes());
  }

  IEnumerator LoadGameScenes() {
    AsyncOperation mainLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
    while (!mainLoad.isDone) yield return null;

    SceneManager.UnloadSceneAsync(Scenes.Loading);
  }
}