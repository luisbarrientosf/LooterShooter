using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager Instance;
  public PerlinMapGenerator mapGenerator;
  public PlayerHealth player;
  public InventoryUIManager inventoryUIManager;
  public GameOverUIManager gameOverUIManager;
  public PoolManager poolManager;

  private bool isGamePaused = false;
  private bool isGameOver = false;
  public bool isTestGame = false;

  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else {
      Destroy(gameObject);
    }
  }

  public void StartTestGame() {
    player.ResetHealth();
    player.gameObject.SetActive(true);
    SetIsGameOver(false);
    SetIsGamePaused(false);
  }

  public void StartGame() {
    if (!CheckGameManager()) return;

    mapGenerator.GenerateNewMap();
    player.ResetHealth();
    player.gameObject.SetActive(true);
    SetIsGameOver(false);
    SetIsGamePaused(false);
  }

  public bool IsGamePaused() {
    return isGamePaused;
  }

  public void SetIsGamePaused(bool value) {
    isGamePaused = value;
    Time.timeScale = isGamePaused ? 0 : 1;
  }

  public bool IsGameOver() {
    return isGameOver;
  }

  public void SetIsGameOver(bool value) {
    isGameOver = value;
    if (isGameOver) {
      SetIsGamePaused(true);
      inventoryUIManager.HideInventory();
      gameOverUIManager.Show();
    }
  }

  private bool CheckGameManager() {
    if (Instance == null) {
      Debug.LogError("GameManager instance is null.");
      return false;
    }
    if (mapGenerator == null) {
      Debug.LogError("MapGenerator is not assigned.");
      return false;
    }
    if (player == null) {
      Debug.LogError("Player is not assigned.");
      return false;
    }
    return true;
  }
}