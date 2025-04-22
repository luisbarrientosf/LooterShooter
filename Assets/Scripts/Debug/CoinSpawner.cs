using UnityEngine;

public class CoinSpawner : MonoBehaviour {
  public GameObject coinPrefab;
  public int coinCount = 10;
  public Vector2 spawnAreaMin = new Vector2(-5f, -5f);
  public Vector2 spawnAreaMax = new Vector2(5f, 5f);

  void Start() {
    SpawnCoins();
  }

  void SpawnCoins() {
    for (int i = 0; i < coinCount; i++) {
      Vector2 randomPos = new Vector2(
          Random.Range(spawnAreaMin.x, spawnAreaMax.x),
          Random.Range(spawnAreaMin.y, spawnAreaMax.y)
      );

      Instantiate(coinPrefab, randomPos, Quaternion.identity);
    }
  }
}
