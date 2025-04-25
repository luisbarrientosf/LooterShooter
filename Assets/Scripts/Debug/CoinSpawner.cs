using UnityEngine;

public class CoinSpawner : MonoBehaviour {
  public ObjectPool coinPool;
  public int count = 200;
  public Vector2 spawnArea = new Vector2(30, 30);

  void Start() {
    if (coinPool == null) return;

    for (int i = 0; i < count; i++) {
      Vector2 position = new Vector2(
          Random.Range(-spawnArea.x, spawnArea.x),
          Random.Range(-spawnArea.y, spawnArea.y)
      );

      GameObject coin = coinPool.Get();
      coin.transform.position = position;
    }
  }
}
