using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour {
  public GameObject roomPrefab;
  public int minRooms = 6;
  public int maxRooms = 20;
  public int gridSize = 15;

  public Color startRoomColor = Color.green;
  public Color bossRoomColor = Color.red;


  private Vector2 roomSize;
  private readonly Dictionary<Vector2Int, GameObject> spawnedRooms = new();
  private readonly Vector2Int[] directions = {
    Vector2Int.up,
    Vector2Int.down,
    Vector2Int.left,
    Vector2Int.right
  };

  void Start() {
    GetMapSize();
    GenerateMap();
  }

  void GetMapSize() {
    GameObject instance = Instantiate(roomPrefab);
    Renderer renderer = instance.GetComponentInChildren<Renderer>();
    if (renderer != null) {
      roomSize = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
    }
  }


  void GenerateMap() {
    spawnedRooms.Clear();

    Vector2Int startPos = Vector2Int.zero;
    SpawnRoom(startPos);

    List<Vector2Int> path = new List<Vector2Int> { startPos };
    Vector2Int currentPos = startPos;
    int roomCount = 1;

    while (roomCount < maxRooms) {
      List<Vector2Int> availableDirs = new List<Vector2Int>();

      foreach (var dir in directions) {
        Vector2Int nextPos = currentPos + dir;
        if (!spawnedRooms.ContainsKey(nextPos)) {
          availableDirs.Add(dir);
        }
      }

      if (availableDirs.Count == 0) {
        if (path.Count > 1) {
          // backtrack to find a new path
          path.RemoveAt(path.Count - 1);
          currentPos = path[path.Count - 1];
          continue;
        }
        else {
          break;
        }
      }

      Vector2Int chosenDir = availableDirs[Random.Range(0, availableDirs.Count)];
      Vector2Int newPos = currentPos + chosenDir;

      SpawnRoom(newPos);
      path.Add(newPos);
      currentPos = newPos;
      roomCount++;
    }

    AssignSpecialRooms();
  }


  void SpawnRoom(Vector2Int position) {
    Vector3 worldPos = new(position.x * roomSize.x, position.y * roomSize.y, 0);
    GameObject room = Instantiate(roomPrefab, worldPos, Quaternion.identity);
    spawnedRooms.Add(position, room);
  }
  void AssignSpecialRooms() {
    if (spawnedRooms.TryGetValue(Vector2Int.zero, out GameObject startRoom)) {
      SetRoomColor(startRoom, startRoomColor);
    }

    Vector2Int bossPosition = spawnedRooms.Keys
      .OrderByDescending(pos => Vector2Int.Distance(Vector2Int.zero, pos))
      .First();

    SetRoomColor(spawnedRooms[bossPosition], bossRoomColor);
  }

  void SetRoomColor(GameObject room, Color color) {
    SpriteRenderer sr = room.GetComponentInChildren<SpriteRenderer>();
    if (sr != null) {
      sr.color = color;
    }
  }
}
