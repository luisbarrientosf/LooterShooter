using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneManager : MonoBehaviour {
  void Start() {
    if (GameManager.Instance.isTestGame) {

      // SceneManager.LoadScene(Scenes.TestScene, LoadSceneMode.Additive);
    }
    else {
      SceneManager.LoadScene(Scenes.MainMenu, LoadSceneMode.Additive);
    }
  }
}
