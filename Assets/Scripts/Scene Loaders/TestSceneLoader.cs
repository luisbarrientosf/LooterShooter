using System.Collections;
using UnityEngine;


public class TestSceneLoader : MonoBehaviour {
  void Awake() {
    StartCoroutine(LoadGameScenes());
    Debug.Log("TestSceneLoader Awake");

  }

  IEnumerator Start() {
    Debug.Log("TestSceneLoader Start");
    yield return StartCoroutine(GameManager.Instance.poolManager.InitializeAllPools());
    Debug.Log("TestSceneLoader Start Coroutine");
    GameManager.Instance.StartTestGame();
    Debug.Log("TestSceneLoader Start Test Game");
  }

  IEnumerator LoadGameScenes() {
    yield return SceneLoader.LoadScene(Scenes.HUD);
    yield return SceneLoader.LoadScene(Scenes.Inventory);
    yield return SceneLoader.LoadScene(Scenes.PauseMenu);
    yield return SceneLoader.LoadScene(Scenes.GameOver);
  }
}
