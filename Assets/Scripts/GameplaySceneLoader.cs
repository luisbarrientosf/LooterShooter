using UnityEngine;

public class GameplaySceneLoader : MonoBehaviour {
  void Start() {
    GameManager.Instance.mapGenerator = PerlinMapGenerator.Instance;
    GameManager.Instance.player = PlayerHealth.Instance;
    GameManager.Instance.StartGame();
  }
}
