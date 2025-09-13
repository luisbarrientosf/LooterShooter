using UnityEngine;

public class TestSceneEventSystem : MonoBehaviour {
  public static TestSceneEventSystem Instance;

  void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
}
