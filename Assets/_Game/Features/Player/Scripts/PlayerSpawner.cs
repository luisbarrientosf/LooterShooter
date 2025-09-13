using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {
  public Tilemap groundTilemap;
  public Tilemap obstacleTilemap;
  public Transform playerTransform;

  private List<Vector3Int> groundTiles = new List<Vector3Int>();

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

  public void SpawnPlayer() {
    CacheGroundTiles();
    if (groundTiles.Count == 0) return;

    Vector3Int spawnCell = groundTiles[Random.Range(0, groundTiles.Count)];
    Vector3 worldPos = groundTilemap.CellToWorld(spawnCell) + new Vector3(0.5f, 0.5f, 0f);
    playerTransform.position = worldPos;

    PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
    playerHealth.ResetHealth();
    playerTransform.gameObject.SetActive(true);
  }


}
