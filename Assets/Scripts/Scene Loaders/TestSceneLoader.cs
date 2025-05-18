using System.Collections;
using UnityEngine;


public class TestSceneLoader : MonoBehaviour {
  void Awake() {
    GameManager.Instance.player = PlayerHealth.Instance;
    StartCoroutine(LoadGameScenes());
  }

  void Start() {
    GameManager.Instance.StartTestGame();
  }

  IEnumerator LoadGameScenes() {
    yield return SceneLoader.LoadScene(Scenes.HUD);
    yield return SceneLoader.LoadScene(Scenes.Inventory);
    yield return SceneLoader.LoadScene(Scenes.PauseMenu);
    yield return SceneLoader.LoadScene(Scenes.GameOver);
  }
}
