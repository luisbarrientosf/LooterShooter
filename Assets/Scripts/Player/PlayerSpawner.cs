using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawner : MonoBehaviour {
  public Tilemap backgroundTilemap;
  public Tilemap obstacleTilemap;
  public Transform playerTransform;
  public Vector3Int searchMin = new Vector3Int(-10, -10, 0);
  public Vector3Int searchMax = new Vector3Int(10, 10, 0);

  void Start() {
    // Vector3Int spawnCell = FindValidSpawnPosition(searchMin, searchMax);
    // Vector3 worldPos = backgroundTilemap.CellToWorld(spawnCell) + new Vector3(0.5f, 0.5f, 0f);
    Vector3Int spawnCell = new Vector3Int(0, 0, 0);
    Vector3 worldPos = backgroundTilemap.CellToWorld(spawnCell) + new Vector3(0.5f, 0.5f, 0f);
    playerTransform.position = worldPos;
  }

  Vector3Int FindValidSpawnPosition(Vector3Int min, Vector3Int max) {
    for (int attempts = 0; attempts < 100; attempts++) {
      int x = Random.Range(min.x, max.x);
      int y = Random.Range(min.y, max.y);
      Vector3Int cell = new Vector3Int(x, y, 0);

      bool hasGround = backgroundTilemap.HasTile(cell);
      bool hasObstacle = obstacleTilemap.HasTile(cell);

      if (hasGround && !hasObstacle) {
        return cell;
      }
    }

    Debug.LogWarning("Could not find valid spawn position. Defaulting to searchMin.");
    return min;
  }
}
