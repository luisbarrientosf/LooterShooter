using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour {
  public ObjectPool enemyPool;
  public ObjectPool coinPool;
  public ObjectPool damageTextPool;
  public int count = 200;
  public Vector2 spawnArea = new Vector2(30, 30);
  public Tilemap obstacleTilemap;
  public Tilemap groundTilemap;

  private List<Vector3Int> groundTiles = new List<Vector3Int>();

  public void SpawnEnemies() {
    CacheGroundTiles();
    if (enemyPool == null || obstacleTilemap == null) return;

    int spawned = 0;
    int tries = 0;
    int maxTries = count * 10; // Avoid infinite loop

    while (spawned < count && tries < maxTries) {
      tries++;

      Vector3Int spawnCell = groundTiles[Random.Range(0, groundTiles.Count)];
      Vector3 worldPos = groundTilemap.CellToWorld(spawnCell) + new Vector3(0.5f, 0.5f, 0f);
      Vector3Int tilePos = groundTilemap.WorldToCell(worldPos);

      if (!obstacleTilemap.HasTile(tilePos)) {
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


  void CacheGroundTiles() {
    groundTiles.Clear();
    // Loop through all positions in the ground tilemap's bounds
    BoundsInt bounds = groundTilemap.cellBounds;
    for (int x = bounds.xMin; x < bounds.xMax; x++) {
      for (int y = bounds.yMin; y < bounds.yMax; y++) {
        Vector3Int pos = new Vector3Int(x, y, 0);
        if (groundTilemap.HasTile(pos)) {
          groundTiles.Add(pos);
        }
      }
    }

    if (groundTiles.Count == 0) {
      Debug.LogWarning("No ground tiles found for spawning.");
    }
  }
}
