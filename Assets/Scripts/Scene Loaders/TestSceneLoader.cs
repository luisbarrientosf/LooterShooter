using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneLoader : MonoBehaviour {
  void Start() {
    SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
    SceneManager.LoadScene("You Died", LoadSceneMode.Additive);
  }
}
