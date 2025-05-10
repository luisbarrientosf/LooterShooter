using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneLoader : MonoBehaviour {
  void Start() {
    SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Additive);
  }
}
