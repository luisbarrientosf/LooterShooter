using UnityEngine;

public class EnemySpawnerFromPool : MonoBehaviour {
  public ObjectPool enemyPool;
  public int count = 200;
  public Vector2 spawnArea = new Vector2(30, 30);

  void Start() {
    for (int i = 0; i < count; i++) {
      Vector2 position = new Vector2(
          Random.Range(-spawnArea.x, spawnArea.x),
          Random.Range(-spawnArea.y, spawnArea.y)
      );

      GameObject enemy = enemyPool.Get();
      enemy.transform.position = position;
    }
  }
}