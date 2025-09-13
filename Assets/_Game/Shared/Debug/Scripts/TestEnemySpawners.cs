using UnityEngine;


public class TestEnemySpawner : MonoBehaviour {
  public ObjectPool enemyPool;
  public ObjectPool coinPool;
  public ObjectPool damageTextPool;
  public int count = 5;
  public Vector2 spawnArea = new Vector2(0, 20);

  void Start() {
    SpawnEnemies();
  }

  public void SpawnEnemies() {
    if (enemyPool == null) return;

    int spawned = 0;
    int tries = 0;
    int maxTries = count * 10; // Avoid infinite loop

    while (spawned < count && tries < maxTries) {
      tries++;


      // Randomly select a position within the spawn area
      Vector3 worldPos = new Vector3(
        Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
        Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
        0
      );

      GameObject enemyGameObject = enemyPool.Get();
      enemyGameObject.transform.position = worldPos;

      Enemy enemy = enemyGameObject.GetComponent<Enemy>();
      enemy.SetPool(enemyPool);
      enemy.SetCoinPool(coinPool);
      enemy.SetDamageTextPool(damageTextPool);
      spawned++;

    }
  }


}
