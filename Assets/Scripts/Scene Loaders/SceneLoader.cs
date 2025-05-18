using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

  public static IEnumerator LoadScene(string sceneName) {
    Debug.Log("Attempting to load scene: " + sceneName);

    if (!Application.CanStreamedLevelBeLoaded(sceneName)) {
      Debug.LogError($"SceneLoader > Scene '{sceneName}' not found in Build Settings!");
      yield break;
    }

    if (SceneManager.GetSceneByName(sceneName).isLoaded) {
      Debug.LogWarning($"SceneLoader > Scene '{sceneName}' is already loaded.");
      yield break;
    }

    AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    if (loadOp == null) {
      Debug.LogError($"SceneLoader > LoadSceneAsync returned null for '{sceneName}'");
      yield break;
    }

    while (!loadOp.isDone) {
      yield return null;
    }

    Debug.Log("SceneLoader > Successfully loaded scene: " + sceneName);
  }
}