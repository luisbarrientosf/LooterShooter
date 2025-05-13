using UnityEngine;
using UnityEngine.SceneManagement;


public class TestSceneLoader : MonoBehaviour {
  void Start() {
    GameManager.Instance.player = PlayerHealth.Instance;
    SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
    SceneManager.LoadScene("You Died", LoadSceneMode.Additive);
    GameManager.Instance.StartTestGame();
  }
}
