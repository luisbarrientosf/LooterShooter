using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  public GameObject enemyPrefab;
  public float spawnInterval = 3f;
  // public Transform[] spawnPoints;

  private float timer;

  void Update() {
    timer += Time.deltaTime;

    if (timer >= spawnInterval) {
      SpawnEnemy();
      timer = 0f;
    }
  }

  void SpawnEnemy() {
    if (enemyPrefab == null) return;

    // Selecciona un punto aleatorio para spawnear

    // int randomIndex = Random.Range(0, spawnPoints.Length);
    // Transform spawnPoint = spawnPoints[randomIndex];
    // Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

    Vector2 randomPos = new Vector2(
        Random.Range(-10f, 10f),
        Random.Range(-5f, 5f)
    );
    Instantiate(enemyPrefab, randomPos, Quaternion.identity);
  }
}