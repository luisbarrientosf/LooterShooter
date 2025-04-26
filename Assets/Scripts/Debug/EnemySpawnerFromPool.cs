using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour {
  public ObjectPool enemyPool;
  public int count = 200;
  public Vector2 spawnArea = new Vector2(30, 30);
  public Tilemap obstacleTilemap;

  void Start() {
    if (enemyPool == null || obstacleTilemap == null) return;

    int spawned = 0;
    int tries = 0;
    int maxTries = count * 10; // Avoid infinite loop

    while (spawned < count && tries < maxTries) {
      tries++;

      Vector3 worldPos = new Vector3(
        Random.Range(-spawnArea.x, spawnArea.x),
        Random.Range(-spawnArea.y, spawnArea.y),
        0
      );

      Vector3Int tilePos = obstacleTilemap.WorldToCell(worldPos);

      // Only spawn if there's NO obstacle tile
      if (!obstacleTilemap.HasTile(tilePos)) {
        GameObject enemyGameObject = enemyPool.Get();
        enemyGameObject.transform.position = worldPos;

        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        enemy.pool = enemyPool;

        spawned++;
      }
    }
  }
}
