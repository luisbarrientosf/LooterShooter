using UnityEngine;
using UnityEngine.SceneManagement;


public class TestSceneLoader : MonoBehaviour {
  void Start() {
    GameManager.Instance.player = PlayerHealth.Instance;
    SceneManager.LoadScene(Scenes.HUD, LoadSceneMode.Additive);
    SceneManager.LoadScene(Scenes.Inventory, LoadSceneMode.Additive);
    SceneManager.LoadScene(Scenes.PauseMenu, LoadSceneMode.Additive);
    SceneManager.LoadScene(Scenes.YouDied, LoadSceneMode.Additive);
    GameManager.Instance.StartTestGame();
  }
}
