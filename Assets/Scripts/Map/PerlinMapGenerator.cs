using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinMapGenerator : MonoBehaviour {
  public int width = 100;
  public int height = 100;
  public float scale = 10f;

  public Tilemap groundTilemap;
  public TileBase waterTile;
  public TileBase grassTile;
  public TileBase mountainTile;

  public float waterThreshold = 0.4f;
  public float mountainThreshold = 0.75f;

  public Vector2 noiseOffset;
  public Tilemap obstacleTilemap;

  void Start() {
    GenerateMap();
  }

  void GenerateMap() {
    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        float xCoord = (x + noiseOffset.x) / scale;
        float yCoord = (y + noiseOffset.y) / scale;
        float noise = Mathf.PerlinNoise(xCoord, yCoord);

        Vector3Int pos = new Vector3Int(x - width / 2, y - height / 2, 0);

        if (noise < waterThreshold) {
          obstacleTilemap.SetTile(pos, waterTile);
        }
        else if (noise > mountainThreshold) {
          obstacleTilemap.SetTile(pos, mountainTile);
        }
        else {
          groundTilemap.SetTile(pos, grassTile);
        }
      }
    }
  }
}
