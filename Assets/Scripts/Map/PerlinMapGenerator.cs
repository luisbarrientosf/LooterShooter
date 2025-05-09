using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinMapGenerator : MonoBehaviour {
  public int width = 100;
  public int height = 100;
  public float scale = 10f;

  public TileBase waterTile;
  public TileBase grassTile;
  public TileBase mountainTile;

  public float waterThreshold = 0.4f;
  public float mountainThreshold = 0.75f;

  public Vector2 noiseOffset;

  public Tilemap waterTilemap;
  public Tilemap groundTilemap;
  public Tilemap obstacleTilemap;

  public PlayerSpawner playerSpawner;
  public EnemySpawner enemySpawner;



  [System.Serializable]
  public class IslandSettings {
    public Vector2 center;
    public float radius = 20f;
  }

  public List<IslandSettings> islands;

  public int islandCount = 3;
  public float minIslandRadius = 15f;
  public float maxIslandRadius = 30f;

  void Start() {
    GenerateIslands();
    GenerateMap();
    playerSpawner.SpawnPlayer();
    enemySpawner.SpawnEnemies();
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.R)) {
      groundTilemap.ClearAllTiles();
      waterTilemap.ClearAllTiles();
      obstacleTilemap.ClearAllTiles();
      GenerateIslands();
      GenerateMap();
      playerSpawner.SpawnPlayer();
      enemySpawner.SpawnEnemies();
    }
  }

  void GenerateIslands() {
    islands.Clear();

    for (int i = 0; i < islandCount; i++) {
      Vector2 center = new Vector2(
          Random.Range(5f, width - 40f),
          Random.Range(5f, height - 40f)
      );

      float radius = Random.Range(minIslandRadius, maxIslandRadius);

      islands.Add(new IslandSettings {
        center = center,
        radius = radius
      });
    }
  }

  void GenerateMap() {
    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        float xCoord = (x + noiseOffset.x) / scale;
        float yCoord = (y + noiseOffset.y) / scale;
        float noise = Mathf.PerlinNoise(xCoord, yCoord);

        // Combine all islands' falloffs
        float islandFactor = 0f;
        foreach (var island in islands) {
          float dx = (x - island.center.x) / island.radius;
          float dy = (y - island.center.y) / island.radius;
          float dist = Mathf.Sqrt(dx * dx + dy * dy);

          // Add some randomness to break circular shape
          float noiseDistortion = Mathf.PerlinNoise((x + noiseOffset.x + 1000f) / (scale * 0.5f),
                                                     (y + noiseOffset.y + 1000f) / (scale * 0.5f)) * 0.5f;

          float falloff = Mathf.Clamp01(dist + noiseDistortion);
          float islandShape = Mathf.Clamp01(1f - Mathf.Pow(falloff, 3f)); // Less sharp falloff
          islandFactor = Mathf.Max(islandFactor, islandShape); // Combine islands
        }

        float finalValue = noise * islandFactor;
        Vector3Int pos = new Vector3Int(x - width / 2, y - height / 2, 0);

        if (finalValue < waterThreshold) {
          waterTilemap.SetTile(pos, waterTile);
          obstacleTilemap.SetTile(pos, waterTile);
        }
        else if (finalValue > mountainThreshold) {
          groundTilemap.SetTile(pos, grassTile);
          obstacleTilemap.SetTile(pos, mountainTile);
        }
        else {
          waterTilemap.SetTile(pos, waterTile);
          groundTilemap.SetTile(pos, grassTile);
        }
      }
    }
  }


}
