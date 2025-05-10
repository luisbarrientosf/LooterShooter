using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager Instance;
  public PlayerHealth player;
  private bool isGamePaused = false;

  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else {
      Destroy(gameObject);
    }
  }

  public bool IsGamePaused() {
    return isGamePaused;
  }

  public void SetIsGamePaused(bool value) {
    isGamePaused = value;
    Time.timeScale = isGamePaused ? 0 : 1;
  }
}