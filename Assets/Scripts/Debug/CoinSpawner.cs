using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinSpawner : MonoBehaviour {
  public ObjectPool coinPool;
  public int count = 200;
  public Vector2 spawnArea = new Vector2(30, 30);
  public Tilemap obstacleTilemap;

  void Start() {
    if (coinPool == null || obstacleTilemap == null) return;

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
        GameObject coin = coinPool.Get();
        coin.transform.position = worldPos; // center coin

        CoinSpin spin = coin.GetComponent<CoinSpin>();
        spin.ResetSpin();

        spawned++;
      }
    }
  }
}
