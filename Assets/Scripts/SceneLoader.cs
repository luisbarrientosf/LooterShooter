using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
  void Start() {
    SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Additive);
  }
}
