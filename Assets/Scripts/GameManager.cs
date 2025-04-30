using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager Instance;
  public PlayerHealth player;

  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else {
      Destroy(gameObject);
    }
  }
}