using UnityEngine;

public class GameplaySceneLoader : MonoBehaviour {
  void Awake() {
    Debug.Log("Gameplay> Loading Map Generator");
    GameManager.Instance.mapGenerator = PerlinMapGenerator.Instance;
    Debug.Log("Gameplay> Loading Player Health");
    GameManager.Instance.player = PlayerHealth.Instance;
  }

  void Start() {
    Debug.Log("Gameplay> START GAME");
    GameManager.Instance.StartGame();
  }
}
