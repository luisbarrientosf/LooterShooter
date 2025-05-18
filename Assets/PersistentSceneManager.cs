using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneManager : MonoBehaviour {
  void Start() {
    if (GameManager.Instance.isTestGame) {
      // SceneManager.LoadScene("Test Scene", LoadSceneMode.Additive);
    }
    else {
      SceneManager.LoadScene(Scenes.MainMenu, LoadSceneMode.Additive);
    }
  }
}
