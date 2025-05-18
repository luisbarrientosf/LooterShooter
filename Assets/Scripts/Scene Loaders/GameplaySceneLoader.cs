using System.Collections;
using UnityEngine;

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
      yield return SceneLoader.LoadScene(Scenes.HUD);
      yield return SceneLoader.LoadScene(Scenes.Inventory);
      yield return SceneLoader.LoadScene(Scenes.PauseMenu);
      yield return SceneLoader.LoadScene(Scenes.GameOver);
    }
  }

}
