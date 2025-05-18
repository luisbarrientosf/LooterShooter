using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneLoader : MonoBehaviour {
  void Awake() {
    GameManager.Instance.mapGenerator = PerlinMapGenerator.Instance;
    GameManager.Instance.player = PlayerHealth.Instance;
    StartCoroutine(LoadGameScenes());
  }

  void Start() {
    GameManager.Instance.StartGame();
  }

  IEnumerator LoadGameScenes() {
    if (!GameManager.Instance.isTestGame) {
      yield return LoadSceneSafe(Scenes.HUD);
      yield return LoadSceneSafe(Scenes.Inventory);
      yield return LoadSceneSafe(Scenes.PauseMenu);
      yield return LoadSceneSafe(Scenes.YouDied);
    }
  }

  IEnumerator LoadSceneSafe(string sceneName) {
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
